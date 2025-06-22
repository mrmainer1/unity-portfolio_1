using Project.EntenEller.Base.Scripts.Advanced.Editors;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers
{
    public static class EEPointerUtils
    {
        public static bool IsTouchDevice()
        {
            #if UNITY_EDITOR
            return EEEditorUtils.IsSimulatorEditor();
            #endif
#pragma warning disable CS0162
            return Input.touchSupported;
#pragma warning restore CS0162
        }
    }
}
