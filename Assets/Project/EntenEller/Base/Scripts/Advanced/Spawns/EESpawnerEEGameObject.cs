using Project.EntenEller.Base.Scripts.Advanced.GameObjects;

namespace Project.EntenEller.Base.Scripts.Advanced.Spawns
{
    public class EESpawnerEEGameObject : EESpawner
    {
        public EEGameObject Prefab;
        
        protected override EEGameObject OnSpawn()
        {
            var obj = EESpawnUtils.Spawn(Prefab);
            obj.Prefab = Prefab;
            return obj;
        }
        
        public override void Despawn(EEGameObject obj)
        {
            SortSpawnArray(obj);
            obj.Destroy();
        }
    }
}
