using System;
using Project.EntenEller.Base.Scripts.UnitTests;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables.Tests.Resources
{
    public class EETestValueByVariableBase : EEBehaviourTest
    {
        private int privateFieldBase = 4;
        protected int ProtectedFieldBase = 16;
        [NonSerialized] public int PublicFieldBase = 256;

        private int privatePropertyBase
        {
            get => privateFieldBase;
            set => privateFieldBase = value;
        }
        
        protected int ProtectedPropertyBase
        {
            get => ProtectedFieldBase;
            set => ProtectedFieldBase = value;
        }
        
        public int PublicPropertyBase
        {
            get => PublicFieldBase;
            set => PublicFieldBase = value;
        }

        public int GetPrivateField()
        {
            return privateFieldBase;
        }
        
        public int GetPrivateProperty()
        {
            return privatePropertyBase;
        }
    }
}
