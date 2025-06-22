using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using UnityEngine;
using WebSocketSharp;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms
{
    public static class EETransformUtils
    {
        public static Transform GetFirstParent (this Transform target)
        {
            var parent = target.parent;
            while (parent.IsNotNull())
            {
                target = parent;
                parent = target.parent;
            }
            return target;
        }
        
        public static void SetParentResetPRS(this Transform target, Transform parent)
        {
            target.SetParent(parent);
            target.localPosition = Vector3.zero;
            target.localRotation = Quaternion.identity;
            target.localScale = Vector3.one;
        }
        
        public static void SetParentSavePRS(this Transform target, Transform parent)
        {
            target.SetParent(parent);
        }

        public static List<Transform> GetFirstRowOfChildren(this Transform target)
        {
            var list = new List<Transform>();
            for (var i = 0; i < target.childCount; i++)
            {
                list.Add(target.GetChild(i));
            }
            return list;
        }

        public static void SetX(this Transform ts, float x)
        {
            var pos = ts.position;
            pos.x = x;
            ts.position = pos;
        }
        
        public static void SetY(this Transform ts, float y)
        {
            var pos = ts.position;
            pos.y = y;
            ts.position = pos;
        }
        
        public static void SetZ(this Transform ts, float z)
        {
            var pos = ts.position;
            pos.z = z;
            ts.position = pos;
        }
        
        public static void SetLocalX(this Transform ts, float x)
        {
            var pos = ts.localPosition;
            pos.x = x;
            ts.localPosition = pos;
        }
        
        public static void SetLocalY(this Transform ts, float y)
        {
            var pos = ts.localPosition;
            pos.y = y;
            ts.localPosition = pos;
        }
        
        public static void SetLocalZ(this Transform ts, float z)
        {
            var pos = ts.localPosition;
            pos.z = z;
            ts.localPosition = pos;
        }

        public static Transform GetLastChild(this Transform target)
        {
            while (true)
            {
                if (target.childCount == 0) return target;
                target = target.GetChild(0);
            }
        }

        public static string Stringify(this Transform target)
        {
            var pos = target.position;
            var rot = target.rotation;
            var scale = target.localScale;

            var list = new List<float>(10);
            list.Add(pos.x);
            list.Add(pos.y);
            list.Add(pos.z);
            list.Add(rot.x);
            list.Add(rot.y);
            list.Add(rot.z);
            list.Add(rot.w);
            list.Add(scale.x);
            list.Add(scale.y);
            list.Add(scale.z);

            return list.Stringify();
        }

        public static (Vector3, Quaternion, Vector3) ParseTransform(this string data)
        {
            var list = data.ParseListFloat();
            var pos = new Vector3(list[0], list[1], list[2]);
            var rot = new Quaternion(list[3], list[4], list[5], list[6]);
            var scale = new Vector3(list[7], list[8], list[9]);

            return (pos, rot, scale);
        }
    }
}