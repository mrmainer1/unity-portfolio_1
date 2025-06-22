using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Methods;

namespace Project.EntenEller.Base.Scripts.Dialogs
{
    [Serializable]
    public class EEDialogueAction
    {
        public List<EEStringMethodResource> List;

        public void Call()
        {
            List.ForEach(a => a.Data.FindAndCall());
        }
    }
}
