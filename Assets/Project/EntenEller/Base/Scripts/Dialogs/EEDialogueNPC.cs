using UnityEngine;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.Text;

namespace Project.EntenEller.Base.Scripts.Dialogs
{
    public class EEDialogueNPC : EEBehaviour
    {
        [SerializeField] private EEText text;
        public EEDialogueResource CurrentDialogue;
        public EENotifier StartDialogueNotifier;
        public EENotifier PrepareLineNotifier, StartShowLineNotifier, FinishShowLineNotifier;
        public EENotifier StartHideLineNotifier, FinishHideLineNotifier;
        public EENotifier LastLineNotifier;
        public int Index;
        private EEDialogueLine npcLine;
        private bool isLastLine;
        
        public void StartDialogue()
        {
            isLastLine = false;
            StartDialogueNotifier.Notify();
        }
        
        public void SetDialogue(EEDialogueResource dialogue)
        {
            Index = 0;
            CurrentDialogue = dialogue;
        }
        
        public void SetIndex(int index)
        {
            Index = index;
        }

        public void PrepareLine()
        {
            if (CurrentDialogue == null) return;
            PrepareLineNotifier.Notify();
            if (isLastLine)
            {
                SetDialogue(CurrentDialogue.NextDialogue);
            }
            npcLine = CurrentDialogue.NPCLines[Index];
            npcLine.Action.Call();
        }
        
        public void PrepareLineFromSave()
        {
            if (CurrentDialogue == null) return;
            PrepareLineNotifier.Notify();
            if (isLastLine)
            {
                SetDialogue(CurrentDialogue.NextDialogue);
            }
            npcLine = CurrentDialogue.NPCLines[Index];
        }

        public void ShowText()
        {
            text.SetData(npcLine.Text);
            StartShowLineNotifier.Notify();
        }
        
        public void FinishShowLine()
        {
            Index++;
            FinishShowLineNotifier.Notify();
            isLastLine = Index == CurrentDialogue.NPCLines.Count;
            if (isLastLine) Finish();
        }
        
        public void StartHideLine()
        {
            StartHideLineNotifier.Notify();
        }
        
        public void FinishHideLine()
        {
            FinishHideLineNotifier.Notify();
        }

        private void Finish()
        {
            if (CurrentDialogue.NextDialogue != null) return;
            LastLineNotifier.Notify();
        }
    }
}
