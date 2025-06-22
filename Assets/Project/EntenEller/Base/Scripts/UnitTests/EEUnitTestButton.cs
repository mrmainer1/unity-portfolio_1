using System;
using System.Collections.Generic;
using System.Reflection;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using MPUIKIT;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;

namespace Project.EntenEller.Base.Scripts.UnitTests
{
    public class EEUnitTestButton : EESpawnerEEGameObject
    {
        public static Action<EEUnitTestButton> CreatedEvent;
        
        private EEBehaviourTest UnitTest;
        private Type Type;
        private MethodInfo Method;
        
        public bool IsFail;
        
        public static Color ColorFail = new Color(1f, 0.33f, 0.17f);
        public static Color ColorSuccess = new Color(0.27f, 0.58f, 0.15f);
        public static Color ColorInProgress = new Color(0.8f, 0.8f, 0.15f);
        
        public EENotifier TestComplete;
        private IEnumerator<float> coroutine;
        private EEBehaviourTest unitTestSpawned;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            CreatedEvent.Call(this);
            GetSelf<Button>().onClick.AddListener(() =>
            {
                if (coroutine != null) return;
                
                DespawnAll();
                
                Prefab = UnitTest.GetEEGameObject();
                unitTestSpawned = Spawn().GetSelf<EEBehaviourTest>();

                IsFail = false;
                coroutine = (IEnumerator<float>) Method.Invoke(unitTestSpawned, null);
                
                Timing.RunCoroutine(CheckIfEnded(Timing.RunCoroutine(coroutine)));

                IEnumerator<float> CheckIfEnded(CoroutineHandle runCoroutine)
                {
                    GetChild<MPImage>().color = ColorInProgress;
                    while (true)
                    {
                        yield return Timing.WaitForOneFrame;
                        if (!runCoroutine.IsRunning) break;
                    }
                    yield return Timing.WaitForOneFrame;
                    GetChild<MPImage>().color = IsFail ? ColorFail : ColorSuccess;
                    if (!IsFail) DespawnAll();
                    TestComplete.Notify();
                    coroutine = null;
                }
            });
        }

        public void Setup(EEBehaviourTest unitTest, Type type, MethodInfo method)
        {
            UnitTest = unitTest;
            Type = type;
            Method = method;
            GetChild<EETextSimple>().SetData(type.Name + " " + method.Name);
        }

        public void CheckExceptionMessage(string message)
        {
            if (message.Contains(Type.Name) && message.Contains(Method.Name)) IsFail = true;
        }
    }
}
