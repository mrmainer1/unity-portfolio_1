using System;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Transforms;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.GameObjects
{
    public class EEGameObjectParentController : EEBehaviour
    {
        public Action ChangedParentEvent;

        public void SetParentResetRPS(EEGameObject newParent)
        {
            SetParent(newParent);
        }
        
        public void SetParentSaveRPS(EEGameObject newParent)
        {
            SetParent(newParent, false);
        }

        public void SetParent(EEGameObject newParent, bool isResetRPS = true)
        {
            if (isResetRPS)
            {
                transform.SetParentResetPRS(newParent.transform);
            }
            else
            {
                transform.SetParentSavePRS(newParent.transform);
            }
            ChangedParentEvent.Call();
        }
        
        public void UnsetParent()
        {
            GetSelf<Transform>().SetParentSavePRS(null);
            ChangedParentEvent.Call();
        }
    }
}
