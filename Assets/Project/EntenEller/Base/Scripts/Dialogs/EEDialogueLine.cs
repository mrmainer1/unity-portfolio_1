using System;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Dialogs
{
    [Serializable]
    public class EEDialogueLine
    {
        public EEDialogueCondition Condition;
        public EEDialogueAction Action;
        [Multiline] public string Text;
    }
}
