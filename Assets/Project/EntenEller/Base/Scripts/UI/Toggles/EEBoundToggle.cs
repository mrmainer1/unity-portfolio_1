using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Toggles
{
    public class EEBoundToggle : EEBehaviour
    {
        public string specialTag;
        private static int frame;
        private static List<string> specialTags = new List<string>();

        private void ValueChanged(bool isOn)
        {
            if (frame != Time.frameCount)
            {
                frame = Time.frameCount;
                specialTags.Clear();
            }
            if (specialTags.Contains(specialTag)) return;
            specialTags.Add(specialTag);
            var boundToggles = EEComponentUtils.FindAll<EEBoundToggle>().Where(a => a.specialTag == specialTag).ToList();
            foreach (var boundToggle in boundToggles)
            {
                boundToggle.GetSelf<EEToggle>().SetState(isOn);
            }
        }

        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEToggle>().ValueChangedEvent += ValueChanged;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEToggle>().ValueChangedEvent -= ValueChanged;
        }
    }
}
