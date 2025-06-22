using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Project.EntenEller.Base.Scripts.UnitTests;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Tests
{
    public class EEBehaviourTestCacheComponent : EEBehaviourTest
    {
        private EEGameObject _MiddleTestGameObject;
        protected EEGameObject MiddleTestGameObject
        {
            get
            {
                if (_MiddleTestGameObject) return _MiddleTestGameObject;
                _MiddleTestGameObject = gameObject.GetComponentsInChildren<EECollectionFloat>(true).
                    First(a => a.gameObject.name == "Middle (1)").GetComponent<EEGameObject>();
                return _MiddleTestGameObject;
            }
        }
    }
}
