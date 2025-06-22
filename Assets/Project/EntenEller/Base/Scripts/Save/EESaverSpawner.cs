using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Save
{
    [RequireComponent(typeof(EESpawner))]
    public class EESaverSpawner : EESaver
    {
        private EESpawner spawner;
        public List<EESaver> savers = new List<EESaver>();

        public override int GetQueue()
        {
            return -100;
        }

        protected override void EEAwake()
        {
            base.EEAwake();
            spawner = GetComponent<EESpawner>();
            spawner.BeforeEnable += OnBeforeEnable;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            spawner.BeforeEnable -= OnBeforeEnable;
        }

        private void OnBeforeEnable(EEGameObject obj)
        {
            var list = obj.GetComponentsInChildren<EESaver>(true);
            SetSpawnKeys(obj, list);
        }

        public override void LocalSave()
        {
            Save(spawner.ActiveAmount.ToString());
        }
        
        public override void LocalLoad()
        {
            savers.ForEach(a => a.Off());
            savers.Clear();
            spawner.DespawnAll();
            Load(OnLoad);

            void OnLoad(string data)
            {
                var amount = data.ParseInt();
                for (var i = 0; i < amount; i++)
                {
                    var obj = spawner.Spawn(false);
                    obj.GetComponentsInChildren<EESaver>(true).ForEach(a => a.LocalLoad());
                }
            }
        }

        private void SetSpawnKeys(EEGameObject obj, IEnumerable<EESaver> list)
        {
            foreach (var saver in list)
            {
                saver.On();
                savers.Add(saver);
                saver.SetKey(Key + "_" + obj.SpawnIndex + "_" + saver.Key);
            }
        }
    }
}