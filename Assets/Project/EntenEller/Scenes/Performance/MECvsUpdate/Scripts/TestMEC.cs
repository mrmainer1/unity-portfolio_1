using System.Collections.Generic;
using MEC;
using Project.EntenEller.Scenes.Performance.Base;
using UnityEngine;

namespace Project.EntenEller.Scenes.Performance.MECvsUpdate
{
    public class TestMEC : MonoBehaviour
    {
        private void Awake()
        {
            Timing.RunCoroutine(Go());
        }

        private IEnumerator<float> Go()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                TestPerformance.Easy();
            }
        }
    }
}