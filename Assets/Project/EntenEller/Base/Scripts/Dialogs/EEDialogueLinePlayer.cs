using System;
using System.Collections.Generic;

namespace Project.EntenEller.Base.Scripts.Dialogs
{
    [Serializable]
    public class EEDialogueLinePlayer : EEDialogueLine
    {
        public List<EEDialogueResource> NextDialogues;
    }
}
