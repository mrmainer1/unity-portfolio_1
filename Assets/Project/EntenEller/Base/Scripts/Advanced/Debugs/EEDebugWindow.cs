using System;
using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using Project.EntenEller.Base.Scripts.UI.Text;
using Project.EntenEller.Base.Scripts.UI.Toggles;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Debugs
{
    public class EEDebugWindow : EESpawnerEEGameObject
    {
        private static Dictionary<EEDebugTag, bool> _debugValues;
        private static Dictionary<EEDebugTag, bool> debugValues
        {
            get
            {
                if (_debugValues != null) return _debugValues;
                _debugValues = new Dictionary<EEDebugTag, bool>();
                foreach (var debugTag in Enum.GetValues(typeof(EEDebugTag)).Cast<EEDebugTag>())
                {
                    _debugValues.Add(debugTag, Load(debugTag) == 1);
                }
                return _debugValues;
            }
        }
        
        protected override void EEAwake()
        {
            base.EEAwake();
            foreach (var debugTag in Enum.GetValues(typeof(EEDebugTag)).Cast<EEDebugTag>())
            {
                var obj = Spawn();
                obj.GetChild<EEToggle>().ValueChangedEvent += isOn =>
                {
                    Save(debugTag, isOn);
                    debugValues[debugTag] = isOn;
                };
                obj.GetChild<EETextSimple>().SetData(debugTag.ToString());
                if (IsActive(debugTag))
                {
                    obj.GetChild<EEToggle>().SetOn();
                }
                else
                {
                    obj.GetChild<EEToggle>().SetOff();
                }
            }
        }

        public static bool IsActive(EEDebugTag debugTag)
        {
            return debugValues[debugTag];
        }
        
        private static int Load(EEDebugTag tag)
        {
            return PlayerPrefs.GetInt(tag.ToString(), 1);
        }
            
        private static void Save(EEDebugTag tag, bool IsOn)
        {
            PlayerPrefs.SetInt(tag.ToString(), IsOn ? 1 : 0);
        }
    }
}
