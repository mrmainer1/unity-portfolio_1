using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.ScriptableObjects;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Dialogs
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "EntenEller/Dialogue/DialogueData", order = 1)]
    public class EEDialogueResource : EEResource
    {
        public List<EEDialogueLine> NPCLines;
        public EEDialogueResource NextDialogue;
        public List<EEDialogueLinePlayer> PlayerLines;
    }
}
