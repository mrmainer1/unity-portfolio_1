using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Actions
{
    public class EEActionSynced : EEBehaviour
    {
        [SerializeField] private EEGameObjectFinder objectFinder;
        [SerializeField] private bool isOnlyForActiveGameObjects;
        public EEAction Action;
        
        public void CallSynced()
        {
            var list = objectFinder.GetAll(this);
            if (isOnlyForActiveGameObjects) list = list.Where(a => a.IsActive).ToList();
            list.ForEach(a => a.GetSelf<EEActionSynced>().Action.Call());
        }
    }
}
