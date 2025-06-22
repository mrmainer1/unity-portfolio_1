using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine.EventSystems;

namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers
{
    [ExecuteBefore(typeof(EEPointerManager))]
    public class EEPointerUI : EEPointer, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            UpNotifier.Event += OnUp;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            UpNotifier.Event -= OnUp;
        }

        private void OnUp()
        {
            if (EEPointerUtils.IsTouchDevice()) Exit();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!EEPointerUtils.IsTouchDevice()) Enter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!EEPointerUtils.IsTouchDevice()) Exit();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (EEPointerUtils.IsTouchDevice()) Enter();
        }
    }
}
