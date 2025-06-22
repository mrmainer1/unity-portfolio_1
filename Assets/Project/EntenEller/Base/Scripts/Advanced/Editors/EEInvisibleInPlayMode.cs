using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.ForEditor
{
    public class EEInvisibleInPlayMode : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
