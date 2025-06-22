#if FIREBASE
using Firebase.Auth;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
#endif
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Addons.Firebase
{
    public class EEFirebaseLogin : EEBehaviour
    {
#if FIREBASE
        public EEFirebase Firebase;
        public FirebaseAuth Auth;
        public FirebaseUser User;
        public EENotifier AuthNotifier;
        public bool IsLogged;

        public void Login()
        {
            Auth = FirebaseAuth.DefaultInstance;
            Auth.StateChanged += AuthStateChanged;
            
            var email = SystemInfo.deviceUniqueIdentifier + "@example.com";
            Auth.SignInWithEmailAndPasswordAsync(email, email).ContinueWith(signInTask =>
            {
                if (signInTask.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (signInTask.IsFaulted)
                {
                    Auth.CreateUserWithEmailAndPasswordAsync(email, email).ContinueWith(createUserTask =>
                    {
                        if (createUserTask.IsCanceled)
                        {
                            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                            return;
                        }
                        if (createUserTask.IsFaulted)
                        {
                            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + createUserTask.Exception);
                            return;
                        }

                        var result = createUserTask.Result;
                        Debug.LogFormat("Firebase user created successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);
                    });
                }
                else
                {
                    Debug.Log("User already signed in: " + Auth.CurrentUser.UserId);
                }
            });
        }
        
        void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (Auth.CurrentUser != User)
            {
                var signedIn = User != Auth.CurrentUser && Auth.CurrentUser != null;
                if (!signedIn && User != null)
                {
                    IsLogged = false;
                    Debug.Log("Signed out " + User.UserId);
                }
                User = Auth.CurrentUser;
                AuthNotifier.Notify();
                if (signedIn)
                {
                    IsLogged = true;
                    Debug.Log("Signed in " + User.UserId);
                }
            }
        }
#endif
    }
}
