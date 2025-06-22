using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.Button;
using Project.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Mobile
{
    public class EEMobileKeyboardKey : EEBehaviour
    {
        [SerializeField] private string symbol;

        protected override void EEAwake()
        {
            base.EEAwake();
            GetChild<EEText>().SetData(symbol);
            GetSelf<EEButton>().ClickEvent += OnClick;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEButton>().ClickEvent -= OnClick;
        }

        private void OnClick(EEButton eeButton)
        {
            GetParent<EEMobileKeyboard>().Call(symbol);
        }
    }
}
