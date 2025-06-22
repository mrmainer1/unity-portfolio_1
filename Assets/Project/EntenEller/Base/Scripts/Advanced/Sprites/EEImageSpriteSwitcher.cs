using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.Advanced.Sprites
{
    public class EEImageSpriteSwitcher : EEBehaviour
    {
        [SerializeField] private List<Sprite> sprites;
        private Image image;

        protected override void EEAwake()
        {
            base.EEAwake();
            image = GetComponent<Image>();
        }

        public void Switch(int i)
        {
            image.sprite = sprites[i];
        }
    }
}
