using System.Collections.Generic;
using MPUIKIT;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.UnitTests
{
    public class EEUnitTestButtonTestAll : EEBehaviour
    {
        private readonly List<EEUnitTestButton> list = new List<EEUnitTestButton>();
        private bool isFail;

        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<CanvasGroup>().alpha = 0;
            EEUnitTestButton.CreatedEvent += Create;
            GetSelf<Button>().onClick.AddListener(Click);
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            EEUnitTestButton.CreatedEvent -= Create; 
            GetSelf<Button>().onClick.RemoveListener(Click);
        }

        private void Create(EEUnitTestButton button)
        {
            list.Add(button);
            GetSelf<CanvasGroup>().alpha = 1;
        }

        private void Click()
        {
            list.ForEach(a => a.GetSelf<Button>().onClick.Invoke());
            var i = 0;
            list.ForEach(a => a.TestComplete.Event += () =>
            {
                GetChild<MPImage>().color = EEUnitTestButton.ColorInProgress;
                i++;
                if (isFail) return;
                if (a.IsFail)
                {
                    isFail = true;
                    GetChild<MPImage>().color = EEUnitTestButton.ColorFail;
                }
                else if (i == list.Count)
                {
                    GetChild<MPImage>().color = EEUnitTestButton.ColorSuccess;
                }
            });
        }
    }
}
