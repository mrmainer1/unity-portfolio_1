#if FIREBASE
using System;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;
#endif
using Project.EntenEller.Base.Scripts.Save;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Addons.Firebase
{
    public abstract class EESaverFirebase : EESaverSource
    {
#if FIREBASE
        private EEFirebaseLogin firebaseLogin;
        private Action actionLoad, actionSave;
        private bool isReady;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            firebaseLogin = EESingleton.Get<EEFirebaseLogin>();
            firebaseLogin.AuthNotifier.Event += OnAuth;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            firebaseLogin.AuthNotifier.Event -= OnAuth;
        }

        private void OnAuth()
        {
            actionLoad?.Invoke();
            actionSave?.Invoke();
        }

        protected override void OnSave(string key, string value)
        {
            actionSave = () =>
            {
                var task = GetDatabaseReference().Child(key).SetValueAsync(value);
                SaveAsync(task);
            };
            if (firebaseLogin.IsLogged) actionSave.Invoke();
        }

        protected override void OnLoad(string key, string defaultValue)
        {
            actionLoad = () =>
            {
                LoadAsync(key, defaultValue);
            };
            if (firebaseLogin.IsLogged) actionLoad.Invoke();
        }

        public void LoadAgain()
        {
            actionLoad.Invoke();
        }

        private async void SaveAsync(Task task)
        {
            await task;
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to save data: " + task.Exception);
                FinishSave(false);
            }
            else if (task.IsCompleted)
            {
                FinishSave(true);
            }
        }

        private async void LoadAsync(string key, string defaultValue)
        {
            await GetDatabaseReference().Child(key).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Failed to load data: " + task.Exception);
                    FinishLoad(defaultValue, false);
                }
                else if (task.IsCompleted)
                {
                    var snapshot = task.Result;
                    if (snapshot.Value == null)
                    {
                        FinishLoad(defaultValue, true);
                        return;
                    }
                    var value = snapshot.Value.ToString();
                    FinishLoad(value, true);
                }
            });
        }
        
        protected abstract DatabaseReference GetDatabaseReference();
#endif
    }
}