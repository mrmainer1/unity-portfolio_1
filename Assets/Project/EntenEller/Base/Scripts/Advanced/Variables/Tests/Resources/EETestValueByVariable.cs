using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using UnityEngine.Assertions;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables.Tests.Resources
{
    public class EETestValueByVariable : EETestValueByVariableBase
    {
        private int privateFieldForChange = -1;
        private int privateField = 1;
        protected int ProtectedField = 2;
        [NonSerialized] public int PublicField = 3;

        private int privateProperty
        {
            get => privateField;
            set => privateField = value;
        }
        
        protected int ProtectedProperty
        {
            get => ProtectedField;
            set => ProtectedField = value;
        }
        
        public int PublicProperty
        {
            get => PublicField;
            set => PublicField = value;
        }
        
        /// <summary>
        /// Fields
        /// </summary>
        /// <returns></returns>

        public IEnumerator<float> TestPrivateField()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(privateField, GetChildData("privateField"));
        }
        
        public IEnumerator<float> TestProtectedField()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(ProtectedField, GetChildData("ProtectedField"));
        }
        
        public IEnumerator<float> TestPublicField()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(PublicField, GetChildData("PublicField"));
        }
        
        public IEnumerator<float> TestPrivateFieldBase()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(GetPrivateField(), GetChildData("privateFieldBase"));
        }
        
        public IEnumerator<float> TestProtectedFieldBase()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(ProtectedFieldBase, GetChildData("ProtectedFieldBase"));
        }
        
        public IEnumerator<float> TestPublicFieldBase()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(PublicFieldBase, GetChildData("PublicFieldBase"));
        }

        /// <summary>
        /// Properties
        /// </summary>
        /// <returns></returns>
        
        public IEnumerator<float> TestPrivateProperty()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(privateProperty, GetChildData("privateProperty"));
        }
        
        public IEnumerator<float> TestProtectedProperty()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(ProtectedProperty, GetChildData("ProtectedProperty"));
        }
        
        public IEnumerator<float> TestPublicProperty()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(PublicProperty, GetChildData("PublicProperty"));
        }
        
        public IEnumerator<float> TestPrivatePropertyBase()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(GetPrivateProperty(), GetChildData("privatePropertyBase"));
        }
        
        public IEnumerator<float> TestProtectedPropertyBase()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(ProtectedPropertyBase, GetChildData("ProtectedPropertyBase"));
        }
        
        public IEnumerator<float> TestPublicPropertyBase()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(PublicPropertyBase, GetChildData("PublicPropertyBase"));
        }
        
        private int GetChildData(string variableName)
        {
            var variable = GetAllChild<EEVariableFinder>().First(a => a.VariablesInfo.First().VariableName == variableName).VariablesInfo.First();
            return (int) variable.Variables.First().Value;
        }

        /// <summary>
        /// Other
        /// </summary>
        /// <returns></returns>
      
        public IEnumerator<float> TestValueChange()
        {
            privateFieldForChange = -1;
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(privateFieldForChange, GetChildData("privateFieldForChange"));
            privateFieldForChange = -2;
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(privateFieldForChange, GetChildData("privateFieldForChange"));
        }
        
        public IEnumerator<float> TestValueByIndex()
        {
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(1f, (float) EETagUtils.FindEETagInChildren(this, "ee_timer0").GetSelf<EEVariableFinder>().VariablesInfo.First().Variables.First().Value);
            Assert.AreEqual(4f, (float) EETagUtils.FindEETagInChildren(this, "ee_timer1").GetSelf<EEVariableFinder>().VariablesInfo.First().Variables[1].Value);
        }
    }
}
