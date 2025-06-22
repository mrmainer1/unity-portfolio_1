using System;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UnitTests
{
    public class EEUnitTestingTestObjectSpawner : EEBehaviour
    {
        public static Action<EEBehaviourTest> CreatedUnitTestGameObjectEvent;

        public void Call()
        {
            var testScripts = Resources.LoadAll<EEBehaviourTest>(string.Empty);
            foreach (var testScript in testScripts)
            {
                CreatedUnitTestGameObjectEvent.Call(testScript);
            }
        }
    }
}
