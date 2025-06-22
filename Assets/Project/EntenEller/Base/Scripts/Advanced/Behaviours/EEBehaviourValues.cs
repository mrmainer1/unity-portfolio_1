using Project.EntenEller.Base.Scripts.Advanced.Randoms;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Behaviours
{
    public class EEBehaviourValues : MonoBehaviour
    {
        private static int idGlobal;
        private int _ComponentID;

        [PropertyOrder(1000)][SerializeField]
        private bool isHavingConstantID;
        [ShowIf("isHavingConstantID")] [PropertyOrder(1001)] [Tooltip("ConstantID is an unique ID that component can use to save its data between game sessions.")]
        public string ConstantID;
        private bool isInitComponentID;

        public int ComponentID
        {
            get
            {
                if (isInitComponentID) return _ComponentID;
                isInitComponentID = true;
                idGlobal++;
                _ComponentID = idGlobal;
                return _ComponentID;
            }
        }
        
        public void SetConstantID(string constantID)
        {
            ConstantID = constantID;
        }

        [ShowIf("isHavingConstantID")] [PropertyOrder(1002)] [Button]
        public void GenerateConstantID()
        {
            ConstantID = EERandomUtils.RandomTimeBasedString();
        }
    }
}