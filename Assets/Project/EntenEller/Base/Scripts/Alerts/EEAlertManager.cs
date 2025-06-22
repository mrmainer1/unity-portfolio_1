using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Alerts
{
    public class EEAlertManager : EEBehaviour
    {
        [SerializeField] private float duration = 5;
        
        public void ShowError()
        {
            ShowCustom("error");
        }
        
        public void ShowWarning()
        {
            ShowCustom("warning");
        }
        
        public void ShowSuccess()
        {
            ShowCustom("success");
        }
        
        public void ShowMessage()
        {
            ShowCustom("message");
        }
        
        public void ShowCustom(string eeTag)
        {
            
        }
        
        public void Hide()
        {
            
        }
    }
}
