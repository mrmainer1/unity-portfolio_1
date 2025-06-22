using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Editors
{
    public static class EEEditorUtils
    {
        public static bool IsNormalEditor()
        {
            return !IsSimulatorEditor();
        }
        
        public static bool IsSimulatorEditor()
        {
            return UnityEngine.Device.Application.isMobilePlatform;
        }
    }
}
