using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Actions
{
    public class EEActionAlternation : EEBehaviour
    {
        [SerializeField] private List<EEAction> actions;
        [SerializeField] private int index;
        [SerializeField] private bool isMovingToNext = true; 
        
        public void Call()
        {
            if (!enabled) return;
            var currentIndex = index;
            if (isMovingToNext)
            {
                index++;
                if (index >= actions.Count) index = 0;
            }
            actions[currentIndex].Call();
        }

        public void Pause()
        {
            isMovingToNext = false;
        }

        public void Unpause()
        {
            isMovingToNext = true;
        }

        public void SetIndex(int i)
        {
            index = i;
        }
        
        public void IncreaseIndex(int i)
        {
            index += i;
        }
        
        public void DecreaseIndex(int i)
        {
            index -= i;
        }
    }
}
