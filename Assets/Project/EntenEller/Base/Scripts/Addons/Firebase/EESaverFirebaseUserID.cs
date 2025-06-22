#if FIREBASE
using Firebase.Database;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
#endif
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Addons.Firebase
{
#if FIREBASE
    public class EESaverFirebaseUserID : EESaverFirebase
    {
        private const string databaseName = "save";
        
        protected override DatabaseReference GetDatabaseReference()
        {
            return EESingleton.Get<EEFirebase>().Reference.Child("users").Child(EESingleton.Get<EEFirebaseLogin>().User.UserId).Reference.Child(databaseName);
        }
#else
    public class EESaverFirebaseUserID : MonoBehaviour
    {
#endif
    }
}
