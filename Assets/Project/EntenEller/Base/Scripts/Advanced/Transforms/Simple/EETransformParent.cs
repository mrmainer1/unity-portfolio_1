using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformParent : EEBehaviour
    {
        [SerializeField] private EEGameObjectFinder parentToStick;

        public void Set()
        {
            GetSelf<Transform>().SetParent(parentToStick.GetSingle(this).transform);
        }
    }
}