using System;
using DG.Tweening;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.Timers;
using Project.EntenEller.Base.Scripts.Translation;
using Project.EntenEller.Base.Scripts.UI.Menu;
using Project.EntenEller.Base.Scripts.UI.SizeController;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Project.EntenEller.Base.Scripts.UI.Text
{
    public abstract class EEText : EEBehaviourEEMenu
    {
        [MultiLineProperty] public string Prefix;
        [MultiLineProperty] public string Postfix;
        [MultiLineProperty] public string Data;
        private TextMeshProUGUI _textMeshProUGUI;
        private EERectTransformSizeController rectTransformSizeController;
        [SerializeField] private float colorChangeDuration = 0;
        [SerializeField] private bool isSupportNewLine;
        [SerializeField] private string newLineString;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (Data == string.Empty) Data = textMeshProUGUI.text;
            rectTransformSizeController = GetComponent<EERectTransformSizeController>();
        }

        private TextMeshProUGUI textMeshProUGUI
        {
            get
            {
                if (_textMeshProUGUI == null) 
                {
                    _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
                }
                return _textMeshProUGUI;
            }
        }

        public void SetPrefix(string prefix)
        {
            Prefix = prefix;
            Change();
        }
        
        public void SetPrefix(object prefix)
        {
            Prefix = prefix.ToString();
            Change();
        }

        public void SetPostfix(string postfix)
        {
            Postfix = postfix;
            Change();
        }
        
        public void SetPostfix(object postfix)
        {
            Postfix = postfix.ToString();
            Change();
        }

        public void SetData(string data)
        {
            Data = data;
            Change();
        }
        
        public void SetData(object data)
        {
            Data = data.ToString();
            Change();
        }

        protected override void MenuStartShow()
        {
            base.MenuStartShow();
            EESingleton.Get<EELanguageManager>().SwitchedEvent += SetLanguage;
            Change();
        }
        
        protected override void MenuStartHide()
        {
            base.MenuStartHide();
            EESingleton.Get<EELanguageManager>().SwitchedEvent -= SetLanguage;
        }
        
        private void SetLanguage(int index)
        {
            Change();
        }

        protected abstract void Change();

        public void SetColor(string hex)
        {
            if (ColorUtility.TryParseHtmlString("#" + hex, out var color))
            {
                SetColor(color);
            }
        }
        
        public void SetColor(Color color)
        {
            textMeshProUGUI.DOKill();
            textMeshProUGUI.DOColor(color, colorChangeDuration);
        }
        
        protected void Set(string text)
        {
            if (this == null) return;
            if (isSupportNewLine)
            {
                text = text.Replace(newLineString, Environment.NewLine);
            }
            textMeshProUGUI.text = text;
            if (rectTransformSizeController.IsNotNull()) rectTransformSizeController.Recount();
        }
        
        protected string GetTranslated()
        {
            var prefixTranslation = EESingleton.Get<EELanguageManager>().GetTranslation(Prefix);
            var dataTranslation = EESingleton.Get<EELanguageManager>().GetTranslation(Data);
            var postfixTranslation = EESingleton.Get<EELanguageManager>().GetTranslation(Postfix);
            return prefixTranslation + dataTranslation + postfixTranslation;
        }
    }
}