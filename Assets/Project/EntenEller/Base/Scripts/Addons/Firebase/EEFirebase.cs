using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
#if FIREBASE
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
#endif

namespace Project.EntenEller.Base.Scripts.Addons.Firebase
{
    public class EEFirebase : EEBehaviour
    {
#if FIREBASE
        public DatabaseReference Reference;
        public EENotifier ReadyNotifier;
        [ReadOnly] public bool IsReady;

        public void Init()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
#if UNITY_EDITOR
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
#endif
                Reference = FirebaseDatabase.DefaultInstance.RootReference;
                IsReady = true;
                ReadyNotifier.Notify();
            });
        }
#endif
    }
}
