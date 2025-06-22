using System.Globalization;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.UI.Menu;
using Project.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.UI.Sliders
{
    public class EESliderTextValue : EEBehaviourEEMenu
    {
        [SerializeField] private EEGameObjectFinder slider;
        [SerializeField] private bool isShowMaxValue;
        [SerializeField] private bool isInteger;
        private float valueOld = float.NegativeInfinity;

        protected override void MenuLoop()
        {
            base.MenuLoop();
            var s = slider.GetSingle().GetSelf<Slider>();
            if (s.value.IsAlmostEqual(valueOld)) return;
            valueOld = s.value;
            var result = isInteger ? ((int) (valueOld + 0.01f)).ToString() : valueOld.ToString(CultureInfo.InvariantCulture);
            if (isShowMaxValue)
            {
                GetSelf<EEText>().SetPrefix(result);
                GetSelf<EEText>().SetPostfix(s.maxValue.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                GetSelf<EEText>().SetData(result);
            }
        }
    }
}
