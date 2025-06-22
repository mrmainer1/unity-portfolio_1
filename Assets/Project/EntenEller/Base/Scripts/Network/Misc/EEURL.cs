using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.Misc
{
    public class EEURL : MonoBehaviour
    {
        public void Call(string url)
        {
            Application.OpenURL(url);
        }
    }
}
