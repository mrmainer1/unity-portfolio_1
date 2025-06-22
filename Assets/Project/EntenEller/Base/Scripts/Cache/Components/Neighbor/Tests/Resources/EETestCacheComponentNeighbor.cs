using System.Collections.Generic;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Tests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Neighbor.Tests.Resources
{
    public class EETestCacheComponentNeighbor : EEBehaviourTestCacheComponent
    {
        public IEnumerator<float> TestGetCollectionInt()
        {
            yield return Timing.WaitForOneFrame;
            var ts = MiddleTestGameObject.GetNeighbor<EECollectionInt>();
            Assert.AreEqual(ts.name, "Middle");
        }
        
        public IEnumerator<float> TestGetAllNeighborTransforms()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllNeighbor<Transform>();
            Assert.AreEqual(array.Length, 2);
        }
        
        public IEnumerator<float> TestGetCollectionString()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetNeighbor<EECollectionString>();
            Assert.AreEqual(coll, null);
        }
        
        public IEnumerator<float> TestGetAllCollectionsFloat()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllNeighbor<EECollectionFloat>();
            Assert.AreEqual(array.Length, 6);
        }
    }
}