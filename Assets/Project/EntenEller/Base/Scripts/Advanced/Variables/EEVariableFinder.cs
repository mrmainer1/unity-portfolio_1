using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.Editors;
using Project.EntenEller.Base.Scripts.Advanced.ForEditor;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Scenes;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    public class EEVariableFinder : EEBehaviourUpdate
    {
        [SerializeField] private EEGameObjectFinder target;
        public List<VariableInfo> VariablesInfo = new List<VariableInfo>();
        [SerializeField] protected bool IsActiveUpdating = false;
        [SerializeField] private bool isWarningOnNotFound = true;
#if DEBUG
        [ReadOnly] public EEEditorTimePoint TimePointVariableFind = new();
#endif
        
        protected override void EEAwake()
        {
            base.EEAwake();
            EESpawnUtils.SpawnDoneEvent += Research;
            EESceneResource.ScenesFinishedChangesEvent += Research;
            Research();
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            EESpawnUtils.SpawnDoneEvent -= Research;
            EESceneResource.ScenesFinishedChangesEvent -= Research;
        }
        

        private void Research()
        {
            if (target.Type != EEGameObjectFinder.GameObjectType.EETag) return;
            target.Restart();
            foreach (var variable in VariablesInfo)
            {
                variable.Variables.Clear();
                foreach (var obj in target.GetAll(this))
                {
                    foreach (var comp in obj.GetComponents(typeof(MonoBehaviour)))
                    {
                        var type = comp.GetType();
                        while (type != null)
                        {
                            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                            MemberInfo memberInfo = type.GetField(variable.VariableName, bindingFlags);
                            if (memberInfo == null) memberInfo = type.GetProperty(variable.VariableName, bindingFlags);
                            if (memberInfo == null)
                            {
                                type = type.BaseType;
                                continue;
                            }
                            var data = new VariableData
                            {
                                Component = comp,
                                MemberInfo = memberInfo,
                            };
                            data.Value = data.MemberInfo.GetMemberValue(comp);
                            variable.Variables.Add(data);
                            break;
                        }
                    }
                }
            }
#if DEBUG
            if (!VariablesInfo.First().Variables.Any() && isWarningOnNotFound)
            {
                var str = VariablesInfo.Aggregate(string.Empty, (current, variable) => current + " " + variable.VariableName);
                EEDebug.Log("Cannot find variable" + str, EEDebug.LogType.Warning);
                EEDebug.ShowProblemObject(gameObject, EEDebug.LogType.Warning);
            }
            TimePointVariableFind.Refresh();
#endif
        }
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            if (!IsActiveUpdating) return;
            var wasChanged = false;
            foreach (var variableInfo in VariablesInfo)
            {
                foreach (var variable in variableInfo.Variables)
                {
                    variable.Value = variable.MemberInfo.GetMemberValue(variable.Component);
                    if (variable.Value == variable.ValueOld) continue;
                    variable.ValueOld = variable.ValueOld;
                    wasChanged = true;
                }
            }
            if (wasChanged) Change();
        }
        
        protected virtual void Change() {}
        
        [Serializable]
        public class VariableInfo 
        {
            public string VariableName;
            [ReadOnly] public List<VariableData> Variables = new List<VariableData>();
        }
        
        [Serializable]
        public class VariableData
        {
            [ReadOnly] public Component Component;
            public MemberInfo MemberInfo;
            public object Value;
            public object ValueOld;
        }
    }
}
