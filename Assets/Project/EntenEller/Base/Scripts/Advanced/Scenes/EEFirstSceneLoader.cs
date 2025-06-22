using System.Collections.Generic;
using MEC;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Scenes
{
    public class EEFirstSceneLoader : EEBehaviour
    {
        [SerializeField] private EESceneResource firstScene;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            Timing.RunCoroutine(Load());
        }

        private IEnumerator<float> Load()
        {
            yield return Timing.WaitForSeconds(0.1f);
            firstScene.Load(firstScene);
        }
    }
}
