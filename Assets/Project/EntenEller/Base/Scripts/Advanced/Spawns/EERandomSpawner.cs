using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Spawns
{
    public class EERandomSpawner : EEBehaviour
    {
        public void Spawn(int n)
        {
            var spawners = GetAllChild<EESpawner>();
            var i = 0;
            while (i < n)
            {
                var spawner = spawners.GetRandom();
                if (spawner.GetSelf<EECollectionRandomOrDefined>().List.First().Value >= Random.value)
                {
                    spawner.SimpleSpawn();
                    i++;
                }
            }
        }
    }
}
