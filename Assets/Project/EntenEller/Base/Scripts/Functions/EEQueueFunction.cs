using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Functions
{
    public static class EEQueueFunction
    {
        private static bool isInit;
        private static List<EEQueueAction> actions = new List<EEQueueAction>();

        public static void Add(int componentID, int queue, Action action)
        {
            if (!isInit)
            {
                isInit = true;
                Timing.RunCoroutine(LateUpdateLoop(), Segment.EndOfFrame);
            }
            actions.Add(new EEQueueAction
            {
                ComponentID = componentID,
                Action = action,
                Queue = queue
            });
        }

        public static void Remove(int componentID)
        {
            actions.RemoveAll(a => a.ComponentID == componentID);
        }

        private static IEnumerator<float> LateUpdateLoop()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                while (actions.Count > 0)
                {
                    var minIndex = 0;
                    for (var i = 1; i < actions.Count; i++)
                    {
                        if (actions[i].Queue < actions[minIndex].Queue)
                        {
                            minIndex = i;
                        }
                    }
                    var action = actions[minIndex];
                    actions.RemoveAt(minIndex);
                    action.Action.Invoke();
                }
            }
        }

        [Serializable]
        public class EEQueueAction
        {
            public Action Action;
            public int ComponentID;
            public int Queue;
        }
    }
}
