using System;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Random = UnityEngine.Random;

namespace Project.EntenEller.Base.Scripts.Advanced.Spawns
{
    public class EESpawnZoneController : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EESpawner>().BeforeEnable += OnBeforeEnable;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EESpawner>().BeforeEnable -= OnBeforeEnable;
        }

        private void OnBeforeEnable(EEGameObject obj)
        {
            #if DEBUG
            if (GetAllChild<EESpawnZone>().Length == 0) throw new Exception("No spawn zones!");
            #endif
            var spawnerZones = GetAllChild<EESpawnZone>();
            while (true)
            {
                var spawnZone = spawnerZones.GetRandom();
                if (spawnZone.GetSelf<EECollectionRandomOrDefined>().List[0].Value <= Random.value) continue;
                if (!spawnZone.TryGeneratePosition(obj, out var pos)) continue;
                obj.transform.position = pos;
                return;
            }
        }
    }
}
