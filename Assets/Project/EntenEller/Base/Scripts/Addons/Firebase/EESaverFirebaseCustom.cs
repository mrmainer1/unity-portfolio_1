#if FIREBASE
using Firebase.Database;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
#endif
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Addons.Firebase
{
#if FIREBASE
    public class EESaverFirebaseCustom : EESaverFirebase
    {
        [SerializeField] private string databaseName;

        protected override DatabaseReference GetDatabaseReference()
        {
            return EESingleton.Get<EEFirebase>().Reference.Child(databaseName);
        }
#else
    public class EESaverFirebaseCustom : MonoBehaviour
    {
#endif
    }
}
