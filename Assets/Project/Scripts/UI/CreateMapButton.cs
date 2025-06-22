using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.InputField;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.UINewMap
{
    public class CreateMapButton : EEBehaviour
    {
        [SerializeField] private EEInputField inputField;
        public EENotifier HasNameNotifier, NotHasNameNotifier;
        protected override void EEAwake()
        {
            base.EEAwake();
            inputField.UpdateNotifier.Event += ChangeName;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            inputField.UpdateNotifier.Event -= ChangeName;
        }

        public void ChangeName()
        {
            if(!inputField.Data.IsNullOrWhitespace())
                HasNameNotifier.Notify();
            else
                NotHasNameNotifier.Notify();
         
      
        }
    }
}
