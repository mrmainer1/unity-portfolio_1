using System.Collections.Generic;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Tests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Child.Tests.Resources
{
    public class EETestCacheComponentChild : EEBehaviourTestCacheComponent
    {
        public IEnumerator<float> TestGetFirstChildTransform()
        {
            yield return Timing.WaitForOneFrame;
            var ts = MiddleTestGameObject.GetChild<Transform>();
            Assert.AreEqual(ts.name, "Child");
        }

        public IEnumerator<float> TestGetAllChildTransforms()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllChild<Transform>();
            Assert.AreEqual(array.Length, 4);
        }

        public IEnumerator<float> TestGetCollectionFloat()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetChild<EECollectionFloat>();
            Assert.AreEqual(coll, null);
        }

        public IEnumerator<float> TestGetCollectionInt()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetChild<EECollectionInt>();
            Assert.AreNotEqual(coll, null);
        }
    }
}