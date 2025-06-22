using Project.EntenEller.Base.Scripts.Advanced.Colliders;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Spawns
{
    public class EESpawnZoneSphere3D : EESpawnZone
    {
        protected override bool TryGenerate(out Vector3 position)
        {
            position = default;
            var coll = GetSelf<SphereCollider>();
            if (coll.IsNull()) return false;
            position = coll.GetRandomPointInside();
            return true;
        }
    }
}
