using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Colliders
{
    public static class EECollidersUtils
    {
        public static Vector3 GetRandomPointInside(this BoxCollider boxCollider)
        {
            var extents = boxCollider.size / 2;
            var ts = boxCollider.transform;
            return GetRandomPointBox(ts, extents);
        }
        
        public static Vector2 GetRandomPointInside(this BoxCollider2D boxCollider)
        {
            var extents = boxCollider.size / 2;
            var ts = boxCollider.transform;
            return GetRandomPointBox(ts, extents);
        }

        private static Vector3 GetRandomPointBox(Transform ts, Vector3 extents)
        {
            var rotation = ts.rotation;
            var position = ts.position;
            var scale = ts.lossyScale;

            extents.x *= scale.x;
            extents.y *= scale.y;
            extents.z *= scale.z;

            var randomPoint = new Vector3
            (
                Random.Range(-extents.x, extents.x),
                Random.Range(-extents.y, extents.y),
                Random.Range(-extents.z, extents.z)
            );

            return position + rotation * randomPoint;
        }
        
        public static Vector3 GetRandomPointInside(this SphereCollider sphereCollider, bool ignoreY = true)
        {
            var randomPoint = sphereCollider.transform.position + Random.insideUnitSphere * sphereCollider.radius;
            if (ignoreY) randomPoint.y = sphereCollider.transform.position.y;
            return randomPoint;
        }
        
        public static Vector2 GetRandomPointInside(this CircleCollider2D circleCollider)
        {
            var randomAngle = Random.Range(0, 2 * Mathf.PI);
            var randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
            var randomPoint = (Vector2)circleCollider.transform.position + randomDirection * circleCollider.radius;
            return randomPoint;
        }
    }
}
