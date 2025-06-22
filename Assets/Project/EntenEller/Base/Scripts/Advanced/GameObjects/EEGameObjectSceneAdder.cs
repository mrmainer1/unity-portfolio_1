using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.GameObjects
{
    public class EEGameObjectSceneAdder : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            var a = EEGameObjectUtils.EEGameObjects;
        }
    }
}
