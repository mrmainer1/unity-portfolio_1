using System.Collections.Generic;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Tests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Self.Tests.Resources
{
    public class EETestCacheComponentSelf : EEBehaviourTestCacheComponent
    {
        public IEnumerator<float> TestGetSelfTransform()
        {
            yield return Timing.WaitForOneFrame;
            var ts = MiddleTestGameObject.GetSelf<Transform>();
            Assert.AreEqual(ts.name, "Middle (1)");
        }

        public IEnumerator<float> TestGetAllSelfTransforms()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllSelf<Transform>();
            Assert.AreEqual(array.Length, 1);
        }
        
        public IEnumerator<float> TestGetCollectionFloat()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllSelf<EECollectionFloat>();
            Assert.AreEqual(array.Length, 3);
        }
        
        public IEnumerator<float> TestGetCollectionString()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetSelf<EECollectionString>();
            Assert.AreEqual(coll, null);
        }
    }
}