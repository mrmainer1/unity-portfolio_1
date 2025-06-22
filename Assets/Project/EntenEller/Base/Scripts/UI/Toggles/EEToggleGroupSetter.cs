using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.UI.Toggles
{
    public class EEToggleGroupSetter : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Toggle>().group = GetComponentInParent<ToggleGroup>(true);
        }
    }
}
