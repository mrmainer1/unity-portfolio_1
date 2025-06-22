using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Randoms;
using Project.EntenEller.Base.Scripts.Dialogs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.ScriptableObjects
{
    public class EEResource : ScriptableObject
    {
        private bool isHavingConstantID;
        public string ConstantID;
        
        [ShowIf("isHavingConstantID")] [Button]
        public void GenerateConstantID()
        {
            ConstantID = EERandomUtils.RandomTimeBasedString();
        }
    }
}
