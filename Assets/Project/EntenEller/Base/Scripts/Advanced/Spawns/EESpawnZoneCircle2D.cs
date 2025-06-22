using Project.EntenEller.Base.Scripts.Advanced.Colliders;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Spawns
{
    public class EESpawnZoneCircle2D : EESpawnZone
    {
        protected override bool TryGenerate(out Vector3 position)
        {
            position = default;
            var coll = GetSelf<CircleCollider2D>();
            if (coll.IsNull()) return false;
            position = coll.GetRandomPointInside();
            return true;
        }
    }
}
