using Project.EntenEller.Base.Scripts.Advanced.Sprites;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.Advanced.Textures
{
    public class EETextureChanger : EEBehaviour
    {
        public void Change(Texture texture)
        {
            GetSelf<Renderer>().material.mainTexture = texture;
        }
        
        public void Change(Texture2D texture)
        {
            if (GetSelf<Image>())
            {
                GetSelf<Image>().sprite = EESpriteUtils.Texture2DToSprite(texture);
                return;
            }
            GetSelf<Renderer>().material.mainTexture = texture;
        }
    }
}
