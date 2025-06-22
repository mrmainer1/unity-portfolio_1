using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Cameras;
using UnityEngine;
using UnityEngine.UI;

namespace Project.EntenEller.Base.Scripts.Advanced.Canvases
{
    public static class EECanvasUtils
    {
        public static Rect GetScreenRect(this RectTransform rt)
        {
            var canvas = rt.GetComponentInParent<Canvas>(true);
            switch (canvas.renderMode)
            {
                case RenderMode.ScreenSpaceCamera:
                    return GetForCamera(rt);
                case RenderMode.ScreenSpaceOverlay:
                    return GetForOverlay(rt);
                case RenderMode.WorldSpace:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            throw new ArgumentOutOfRangeException();
        }

        private static Rect GetForCamera(RectTransform rt)
        {
            var worldCorners = GetWorldCorners(rt);
            var screenCorners = new Vector3[4];
            for (var i = 0; i < 4; i++)
            {
                screenCorners[i] = EECameraUtils.MainCamera.WorldToScreenPoint(worldCorners[i]);
                screenCorners[i].x = screenCorners[i].x;
                screenCorners[i].y = screenCorners[i].y;
            }
            return GetRect(screenCorners);
        }

        public static Rect GetForOverlay(RectTransform rt)
        {
            var worldCorners = new Vector3[4];
            rt.GetWorldCorners(worldCorners);
            return GetRect(worldCorners);
        }

        private static Vector3[] GetWorldCorners(RectTransform rt)
        {
            var worldCorners = new Vector3[4];
            rt.GetWorldCorners(worldCorners);
            return worldCorners;
        }
        
        private static Rect GetRect(IList<Vector3> corners)
        {
            return new Rect
            (
                corners[0].x,
                corners[0].y,
                corners[2].x - corners[0].x,
                corners[2].y - corners[0].y
            );
        }
    }
}