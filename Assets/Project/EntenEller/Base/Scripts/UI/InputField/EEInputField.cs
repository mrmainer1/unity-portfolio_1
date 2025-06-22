using System;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Save;
using Project.EntenEller.Base.Scripts.UI.Menu;
using TMPro;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.InputField
{
    public class EEInputField : EEBehaviourEEMenu
    {
        [SerializeField] private bool isDefaultOnMenuShow;
        public EENotifier UpdateNotifier, EmptyDataNotifier, GetDataNotifier;
        public event Action<string> UpdateEvent;
        public string Data;
        private TMP_InputField tmpInputField;

        protected override void EEAwake()
        {
            base.EEAwake();
            tmpInputField = GetSelf<TMP_InputField>();
            tmpInputField.onValueChanged.AddListener(OnValueChanged);
            CheckIfEmpty(tmpInputField.text);
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            tmpInputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(string data)
        {
            Data = data;
            CheckIfEmpty(Data);
            if (data.IsEmpty()) EmptyDataNotifier.Notify();
            UpdateNotifier.Notify();
            UpdateEvent.Call(data);
        }

        public void Set(string data)
        {
            Data = data;
            CheckIfEmpty(Data);
            tmpInputField.text = data;
        }

        private void CheckIfEmpty(string data)
        {
            if (data.IsEmpty()) EmptyDataNotifier.Notify();
            else GetDataNotifier.Notify();
        }
    }
}