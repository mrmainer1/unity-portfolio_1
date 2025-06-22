using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Scenes;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Timers
{
    public static class EETime
    {
        private static readonly List<EETimerData> timers = new List<EETimerData>(10000);
        private static bool init = false;
        private static uint TokenGlobal = 0;
        
        [Serializable]
        public class EETimerData
        {
            public int ComponentID;
            public bool IsUnscaled;
            public bool IsNotifyingUpdate;
            public bool IsIgnoreSceneSwitching;

            public float CurrentTime;
            public float FinalTime;

            public uint Token;

            public EETimerState TimerState;

            public Action Action;
            public EENotifier CancelNotifier = new EENotifier();
            public EENotifier UpdateNotifier = new EENotifier();
            public EENotifier PauseNotifier = new EENotifier();
            public EENotifier ResumeNotifier = new EENotifier();
            public EENotifier CompleteNotifier = new EENotifier();
            
            public enum EETimerState
            {
                Update,
                Pause,
                Done,
                Cancel
            }

            public void Prepare()
            {
                CurrentTime = 0f;
                TimerState = EETimerState.Update;
                timers.Add(this);
                Token = TokenGlobal;
                TokenGlobal++;
            }
        }
        
        public static void StartTimer(EETimerData timerData)
        {
            if (!init)
            {
                init = true;
                EEDebug.Log("EETimer initialized!");
                EESceneResource.ScenesStartedChangesEvent += () =>
                {
                    EEDebug.Log("Cleaning timers!");
                    for (var i = 0; i < timers.Count; i++)
                    {
                        var timer = timers[i];
                        if (timer.IsIgnoreSceneSwitching) continue;
                        StopTimer(timer);
                        i--;
                    }
                };
            }
            
            if (timerData.FinalTime.IsAlmostZero())
            {
                timerData.Action.Call();
                return;
            }

            timerData.Prepare();
            Async(timerData);
            
            static async void Async(EETimerData timerData)
            {
                var token = timerData.Token;
                while (timerData.CurrentTime < timerData.FinalTime)
                {
                    await UniTask.DelayFrame(1);
                    if (token != timerData.Token) return;
                    switch (timerData.TimerState)
                    {
                        case EETimerData.EETimerState.Update:
                        {
                            timerData.CurrentTime += timerData.IsUnscaled ? Time.unscaledDeltaTime : Time.deltaTime;
                            if (timerData.IsNotifyingUpdate) timerData.UpdateNotifier.Notify();
                            continue;
                        }
                        case EETimerData.EETimerState.Pause:
                            continue;
                    }
                }

                timerData.TimerState = EETimerData.EETimerState.Done;
                timerData.Action.Call();
                timerData.CompleteNotifier.Notify();
                Remove(timerData);
            }
        }

        public static void StopTimer(Action action)
        {
            var timer = timers.FirstOrDefault(a => a?.Action == action);
            if (timer == null) return;
            StopTimer(timer);
        }
        
        public static void StopTimer(EETimerData timerData)
        {
            timerData.TimerState = EETimerData.EETimerState.Cancel;
            timerData.CancelNotifier.Notify();
            Remove(timerData);
        }

        private static void Remove(EETimerData timerData)
        {
            timers.Remove(timerData);
        }

        public static void StopAllTimersForComponentID(int componentID)
        {
            for (var i = 0; i < timers.Count; i++)
            {
                var timerData = timers[i];
                if (timerData.ComponentID != componentID) continue;
                StopTimer(timerData);
                i--;
            }
        }

        public static bool IsTimerExist(EETimerData timerData)
        {
            return timerData != null;
        }

        public static void PauseTimer(EETimerData timerData)
        {
            timerData.TimerState = EETimerData.EETimerState.Pause;
            timerData.PauseNotifier.Notify();
        }

        public static void ResumeTimer(EETimerData timerData)
        {
            timerData.TimerState = EETimerData.EETimerState.Update;
            timerData.ResumeNotifier.Notify();
        }
    }
}