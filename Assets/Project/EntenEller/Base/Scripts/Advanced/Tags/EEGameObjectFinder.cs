using System;
using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Tags
{
    [Serializable]
    public class EEGameObjectFinder 
    {
        public GameObjectType Type;
        private bool isInitialized;
        
        [Sirenix.OdinInspector.ShowIf("Type", GameObjectType.EETag)]
        [SerializeField] private string eeTag = string.Empty;
        
        [Sirenix.OdinInspector.ShowIf("Type", GameObjectType.EEGameObject)]
        [SerializeField] private EEGameObject eeGameObject = null;
        
        [SerializeField] [ReadOnly] private List<EEGameObject> foundGameObjects;

        public EEGameObject EEGameObject => GetSingle();
        
#if DEBUG
        [SerializeField] private bool isWarningOnNotFound = true;
#endif   
        
        public void SetGameObject(EEGameObject obj)
        {
            eeGameObject = obj;
        }
        
        public void Restart()
        {
            isInitialized = false;
        }
        
        public EEGameObject GetSingle(MonoBehaviour self = null)
        {
            if (!isInitialized) Initialize(self);
            return foundGameObjects[0];
        }
        
        public List<EEGameObject> GetAll(MonoBehaviour self = null)
        {
            if (!isInitialized) Initialize(self);
            return foundGameObjects;
        }
        
        public EEGameObject GetByIndex(int index, MonoBehaviour self = null)
        {
            if (!isInitialized) Initialize(self);
            return foundGameObjects[index];
        }

        private void Initialize(MonoBehaviour self)
        {
            isInitialized = true;
            switch (Type)
            {
                case GameObjectType.EEGameObject:
                    foundGameObjects = new List<EEGameObject> {eeGameObject};
                    break;
                case GameObjectType.EETag:
                    var tags = EETagUtils.TryFindEETagsInScenes(eeTag);
                    if (tags == null)
                    {
#if DEBUG
                        if (isWarningOnNotFound)
                        {
                            EEDebug.Log("Cannot find EETag " + eeTag, EEDebug.LogType.Warning);
                            EEDebug.ShowProblemObject(self.gameObject, EEDebug.LogType.Warning);
                        }
#endif
                        return;
                    }
                    foundGameObjects = tags.Select(a => a.GetEEGameObject()).ToList();
                    break;
            }
        }
        
        public enum GameObjectType
        {
            EEGameObject,
            EETag
        }
    }
}
