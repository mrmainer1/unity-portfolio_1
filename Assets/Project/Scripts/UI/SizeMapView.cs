using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UINewMap
{
    public class SizeMapView : EEBehaviour
    {
        [SerializeField] private SizeMap sizeMap;
        [SerializeField] private Slider sliderHeight;
        [SerializeField] private Slider sliderWidth;
        [SerializeField] private EEText titleHeight;
        [SerializeField] private EEText titleWidth;
        protected override void EEAwake()
        {
            base.EEAwake();
            sizeMap.SizeChangedNotifier.Event += ViewSize;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            sizeMap.SizeChangedNotifier.Event -= ViewSize;
        }

        private void ViewSize()
        {
            titleHeight.SetData(sliderHeight.value);
            titleWidth.SetData(sliderWidth.value);
        }
    }

}
