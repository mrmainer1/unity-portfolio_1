using System;
using System.Text.RegularExpressions;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.InputField
{
    public class EEInputFieldValidation : EEBehaviour
    {
        [SerializeField] private ValidationType validationType;
        [ShowIf("validationType", ValidationType.Text)][SerializeField] private string validText;
        [ShowIf("validationType", ValidationType.Regex)][SerializeField] private string mask;
        public bool IsValid;
        public EENotifier ValidNotifier, InvalidNotifier;
            
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEInputField>().UpdateNotifier.Event += Check;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEInputField>().UpdateNotifier.Event -= Check;
        }

        public void Check()
        {
            var isValidOld = IsValid;
            switch (validationType)
            {
                case ValidationType.Text:
                    IsValid = GetSelf<EEInputField>().Data == validText;
                    break;
                case ValidationType.Regex:
                    IsValid = Regex.IsMatch(GetSelf<EEInputField>().Data, mask);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (isValidOld == IsValid) return;
            if (IsValid) ValidNotifier.Notify();
            else InvalidNotifier.Notify();
        }
    }

    public enum ValidationType
    {
        Text,
        Regex
    }
}
