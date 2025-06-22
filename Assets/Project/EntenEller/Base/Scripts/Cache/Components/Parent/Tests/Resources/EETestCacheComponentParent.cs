using System.Collections.Generic;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Tests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Parent.Tests.Resources
{
    public class EETestCacheComponentParent : EEBehaviourTestCacheComponent
    {
        public IEnumerator<float> TestGetFirstParentTransform()
        {
            yield return Timing.WaitForOneFrame;
            var ts = MiddleTestGameObject.GetParent<Transform>();
            Assert.AreEqual(ts.name, "CollectionString");
        }
        
        public IEnumerator<float> TestGetAllParentTransforms()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllParent<Transform>();
            Assert.AreEqual(array.Length, 2);
        }
        
        public IEnumerator<float> TestGetCollectionString()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetParent<EECollectionString>();
            Assert.AreNotEqual(coll, null);
        }
        
        public IEnumerator<float> TestGetCollectionInt()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetParent<EECollectionInt>();
            Assert.AreEqual(coll, null);
        }
    }
}