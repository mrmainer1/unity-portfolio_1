using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms
{
    public class EEChildShuffle : EEBehaviour
    {
        public List<Transform> StartOrder = new List<Transform>();

        public void Call()
        {
            if (StartOrder.Count == 0)
            {
                for (var i = 0; i < GetSelf<Transform>().childCount; i++)
                {
                    StartOrder.Add(GetSelf<Transform>().GetChild(i));
                }
            }
            StartOrder.Where(a => a.gameObject.activeInHierarchy).OrderBy(a => Random.Range(0f, 1f)).ToList().ForEach(b => b.transform.SetAsFirstSibling());
        }
    }
}
