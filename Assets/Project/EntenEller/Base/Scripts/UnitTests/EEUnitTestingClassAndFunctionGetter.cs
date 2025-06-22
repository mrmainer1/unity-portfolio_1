using System;
using System.Reflection;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.UnitTests
{
    public class EEUnitTestingClassAndFunctionGetter : EEBehaviour
    {
        public static Action<EEBehaviourTest, Type, MethodInfo> GotDataEvent;
        protected override void EEAwake()
        {
            base.EEAwake();
            EEUnitTestingTestObjectSpawner.CreatedUnitTestGameObjectEvent += testObject =>
            {
                var type = testObject.GetType();
                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    if (!method.Name.Contains("Test")) continue;
                    GotDataEvent.Call(testObject, type, method);
                }
            };
        }
    }
}