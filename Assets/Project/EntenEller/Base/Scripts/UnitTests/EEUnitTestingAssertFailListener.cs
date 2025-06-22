using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UnitTests
{
    public class EEUnitTestingAssertFailListener : EEBehaviour
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            Application.logMessageReceived += OnLogCallback;
        }
        
        protected override void EEDestroy()
        {
            base.EEDestroy();
            Application.logMessageReceived -= OnLogCallback;
        }
        
        private static void OnLogCallback(string condition, string trace, LogType type)
        {
            if (type != LogType.Exception && type != LogType.Error) return;
            if (!Application.isPlaying) return;
            var buttons = EEComponentUtils.FindAll<EEUnitTestButton>();
            buttons.ForEach(a => { a.CheckExceptionMessage(trace); });
        }
    }
}
