using System.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.Advanced.Layouts
{
    public class EELayout : EEBehaviour
    {
        private IEnumerator Start()
        {
            GetSelf<CanvasGroup>().alpha = 0;
            yield return null;
            GetSelf<LayoutGroup>().enabled = false;
            yield return new WaitForEndOfFrame();
            GetSelf<LayoutGroup>().enabled = true;
            GetSelf<CanvasGroup>().alpha = 1;
        }
    }
}
