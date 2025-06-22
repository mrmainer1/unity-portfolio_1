using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.Assertions;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Tests;

namespace Project.EntenEller.Base.Scripts.Advanced.Serializations.Tests.Resources
{
    public class EEJSONTest : EEBehaviourTestCacheComponent
    {
        public IEnumerator<float> TestJSONMissingFields()
        {
            yield return Timing.WaitForOneFrame;
            
            var json = "{\"Name\":\"John Doe\"}";
            var deserializedData = EEJSON.Deserialize<SampleData>(json);

            Assert.AreEqual(deserializedData.Name, "John Doe");
            Assert.AreEqual(deserializedData.Age, 0);
        }

        public IEnumerator<float> TestJSONVectorSerialization()
        {
            yield return Timing.WaitForOneFrame;
            var vectorToSerialize = new Vector3(1.5f, 2.0f, -3.5f);

            var json = EEJSON.Serialize(vectorToSerialize);
            var deserializedVector = EEJSON.Deserialize<Vector3>(json);
            
            Assert.AreEqual(vectorToSerialize.x, deserializedVector.x);
            Assert.AreEqual(vectorToSerialize.y, deserializedVector.y);
            Assert.AreEqual(vectorToSerialize.z, deserializedVector.z);
        }

        public IEnumerator<float> TestJSONVectorArraySerialization()
        {
            yield return Timing.WaitForOneFrame;
            var vectorArrayToSerialize = new Vector2[]
            {
                new Vector2(1.0f, 2.0f),
                new Vector2(-3.0f, 0.5f)
            };

            var json = EEJSON.Serialize(vectorArrayToSerialize);
            var deserializedVectorArray = EEJSON.Deserialize<Vector2[]>(json);
            
            for (var i = 0; i < vectorArrayToSerialize.Length; i++)
            {
                Assert.AreEqual(vectorArrayToSerialize[i], deserializedVectorArray[i]);
            }
        }
        
        public IEnumerator<float> TestListSerialization()
        {
            yield return Timing.WaitForOneFrame;

            var intArrayToSerialize = new List<int>{ 1, 2, 3, 4, 5 };

            var json = EEJSON.Serialize(intArrayToSerialize);
            var deserializedIntArray = EEJSON.Deserialize<List<int>>(json);

            Assert.AreEqual(true, intArrayToSerialize.IsFullyEqual(deserializedIntArray));
        }
        
        public IEnumerator<float> TestArraySerialization()
        {
            yield return Timing.WaitForOneFrame;
            
            var intArrayToSerialize = new int[5];
            for (var i = 0; i < intArrayToSerialize.Length; i++)
            {
                intArrayToSerialize[i] = i;
            }
            
            var json = EEJSON.Serialize(intArrayToSerialize);
            var deserializedIntArray = EEJSON.Deserialize<int[]>(json);

            for (var i = 0; i < intArrayToSerialize.Length; i++)
            {
                Assert.AreEqual(true, deserializedIntArray[i] == intArrayToSerialize[1]);
            }

            
        }
        
        public IEnumerator<float> TestPolymorphicSerialization()
        {
            yield return Timing.WaitForOneFrame;

            var objectsToSerialize = new BaseClass[]
            {
                new DerivedClassA { Value = 10 },
                new DerivedClassB { Value = 20 }
            };

            var json = EEJSON.Serialize(objectsToSerialize);
            var deserializedObjects = EEJSON.Deserialize<BaseClass[]>(json);
            
            Assert.AreEqual(10, ((DerivedClassA)deserializedObjects[0]).Value);
            Assert.AreEqual(20, ((DerivedClassB)deserializedObjects[1]).Value);
        }
        
        public IEnumerator<float> TestEnumSerialization()
        {
            yield return Timing.WaitForOneFrame;

            var enumValueToSerialize = TestEnum.OptionB;

            var json = EEJSON.Serialize(enumValueToSerialize);
            var deserializedEnum = EEJSON.Deserialize<TestEnum>(json);

            Assert.AreEqual(enumValueToSerialize, deserializedEnum);
        }

        private enum TestEnum
        {
            OptionA,
            OptionB,
            OptionC
        }

        public IEnumerator<float> TestJSONSimpleObject()
        {
            yield return Timing.WaitForOneFrame;
            var dataToSerialize = new SampleData
            {
                Name = "John Doe",
                Age = 30
            };

            var json = EEJSON.Serialize(dataToSerialize);
            var deserializedData = EEJSON.Deserialize<SampleData>(json);
            Assert.AreEqual(dataToSerialize.Name, deserializedData.Name);
            Assert.AreEqual(dataToSerialize.Age, deserializedData.Age);
        }

        public IEnumerator<float> TestJSONDictionary()
        {
            yield return Timing.WaitForOneFrame;
            var dictionaryToSerialize = new Dictionary<string, int>
            {
                { "One", 1 },
                { "Two", 2 },
                { "Three", 3 }
            };

            var json = EEJSON.Serialize(dictionaryToSerialize);
            var deserializedDictionary = EEJSON.Deserialize<Dictionary<string, int>>(json);
            
            foreach (var kvp in dictionaryToSerialize)
            {
                Assert.IsTrue(deserializedDictionary.ContainsKey(kvp.Key));
                Assert.AreEqual(kvp.Value, deserializedDictionary[kvp.Key]);
            }
        }

        public IEnumerator<float> TestJSONNestedClasses()
        {
            yield return Timing.WaitForOneFrame;
            var outerClass = new OuterClass
            {
                OuterProperty = "Outer",
                Inner = new InnerClass
                {
                    InnerProperty = "Inner"
                }
            };

            var json = EEJSON.Serialize(outerClass);
            var deserializedOuterClass = EEJSON.Deserialize<OuterClass>(json);
            
            Assert.AreEqual(outerClass.OuterProperty, deserializedOuterClass.OuterProperty);
            Assert.AreEqual(outerClass.Inner.InnerProperty, deserializedOuterClass.Inner.InnerProperty);
        }
        
        public IEnumerator<float> TestJSONQuaternionSerialization()
        {
            yield return Timing.WaitForOneFrame;
            var quaternionToSerialize = new Quaternion(0.5f, 0.3f, -0.2f, 1.0f);

            var json = EEJSON.Serialize(quaternionToSerialize);
            var deserializedQuaternion = EEJSON.Deserialize<Quaternion>(json);
            
            Assert.AreEqual(quaternionToSerialize.x, deserializedQuaternion.x);
            Assert.AreEqual(quaternionToSerialize.y, deserializedQuaternion.y);
            Assert.AreEqual(quaternionToSerialize.z, deserializedQuaternion.z);
            Assert.AreEqual(quaternionToSerialize.w, deserializedQuaternion.w);
        }

        public IEnumerator<float> TestJSONAnonymousType()
        {
            yield return Timing.WaitForOneFrame;
            var anonymousData = new { Name = "John Doe", Age = 30, IsStudent = true };

            var json = EEJSON.Serialize(anonymousData);
            var deserializedData = EEJSON.Deserialize<object>(json);

            Assert.AreEqual(json, EEJSON.Serialize(deserializedData));
        }
        
        public IEnumerator<float> TestJSONObjectFloat()
        {
            yield return Timing.WaitForOneFrame;
            
            const float f1 = 1f;
            var json = EEJSON.Serialize((object) f1);
            var f2 = (float) EEJSON.Deserialize<object>(json);

            Assert.AreEqual(f1, f2);
        }
        
        [Serializable]
        private class SampleData
        {
            public string Name;
            public int Age;
        }

        [Serializable]
        private class OuterClass
        {
            public string OuterProperty;
            public InnerClass Inner;
        }

        [Serializable]
        private class InnerClass
        {
            public string InnerProperty;
        }
        
        private class BaseClass
        {
            public int Value;
        }

        private class DerivedClassA : BaseClass { }
        private class DerivedClassB : BaseClass { }
    }
}
