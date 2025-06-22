using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Editors
{
    [Serializable]
    public class EEEditorTimePoint
    {
        public static int _OrderOfLastCall = 0;
        [ReadOnly] public int FrameOfLastCall = -1;
        [ReadOnly] public int OrderOfLastCall = -1;
        [ReadOnly] public int AmountOfCall = 0;

        public void Refresh()
        {
            FrameOfLastCall = Time.frameCount;
            OrderOfLastCall = _OrderOfLastCall;
            AmountOfCall++;
            _OrderOfLastCall++;
        }
    }
}
