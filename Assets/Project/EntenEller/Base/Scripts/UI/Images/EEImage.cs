using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.UI.Images
{
    public class EEImage : EEBehaviourUpdate
    {
        [SerializeField] private float speed;
        private Color targetColor;
        private Image image;

        protected override void EEAwake()
        {
            base.EEAwake();
            image = GetComponent<Image>();
            targetColor = image.color;
        }

        public void SetColor(Color color)
        {
            targetColor = color;
        }
        
        public void SetColor(string colorHex)
        {
            ColorUtility.TryParseHtmlString(colorHex, out targetColor);
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            image.color = Color.Lerp(image.color, targetColor, speed * Time.deltaTime);
        }
    }
}
