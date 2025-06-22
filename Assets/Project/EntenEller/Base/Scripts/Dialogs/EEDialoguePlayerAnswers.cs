using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using UnityEngine;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.Button;
using Project.EntenEller.Base.Scripts.UI.Text;
using Sirenix.Utilities;

namespace Project.EntenEller.Base.Scripts.Dialogs
{
    public class EEDialoguePlayerAnswers : EEBehaviour
    {
        [SerializeField] private EESpawner spawner;
        [SerializeField] private EEDialogueNPC DialogueNpc;
        public EENotifier AnswerDoneNotifier;
        private Dictionary<EEButton, EEDialogueLinePlayer> cache = new Dictionary<EEButton, EEDialogueLinePlayer>();

        protected override void EEAwake()
        {
            base.EEAwake();
            DialogueNpc.LastLineNotifier.Event += OnFinalQuote;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            DialogueNpc.LastLineNotifier.Event -= OnFinalQuote;
        }

        private void OnFinalQuote()
        {
            spawner.DespawnAll();
            
            foreach (var answer in DialogueNpc.CurrentDialogue.PlayerLines)
            {
                var obj = spawner.Spawn();
                obj.GetChild<EEText>().SetData(answer.Text);
                var button = obj.GetChild<EEButton>();
                button.ClickEvent += OnClick;
                cache.Add(button, answer);
            }
            
            void OnClick(EEButton button)
            {
                var answer = cache[button];
                answer.Action.Call();
                DialogueNpc.SetDialogue(answer.NextDialogues[0]);
                CleanCache();
                AnswerDoneNotifier.Notify();
            }

            void CleanCache()
            {
                cache.ForEach(a =>
                {
                    a.Key.ClickEvent -= OnClick;
                });
                cache.Clear();
            }
        }
    }
}
