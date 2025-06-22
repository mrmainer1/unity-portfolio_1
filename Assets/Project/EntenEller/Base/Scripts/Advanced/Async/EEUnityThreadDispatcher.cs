using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;

namespace Project.EntenEller.Base.Scripts.Advanced.Async
{
    public class EEUnityThreadDispatcher : EEBehaviourUpdate
    {
        private static List<Action> actions = new List<Action>();
        
        public void Init() {}
        
        public static void Add(Action action)
        {
            actions.Add(action);
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            for (var i = 0; i < actions.Count; i++)
            {
                actions[0].Invoke();
                actions.RemoveAt(0);
            }
        }
    }
}
