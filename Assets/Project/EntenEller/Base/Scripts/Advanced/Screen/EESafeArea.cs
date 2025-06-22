//source: https://forum.unity.com/threads/canvashelper-resizes-a-recttransform-to-iphone-xs-safe-area.521107

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.EntenEller.Base.Scripts.Advanced.Screen
{
    [RequireComponent(typeof(Canvas))]
    public class EESafeArea : MonoBehaviour
    {
        private static List<EESafeArea> helpers = new List<EESafeArea>();
        private static bool screenChangeVarsInitialized = false;
        private static ScreenOrientation lastOrientation = ScreenOrientation.LandscapeLeft;
        private static Vector2 lastResolution = Vector2.zero;
        private static Rect lastSafeArea = Rect.zero;
 
        private Canvas canvas;
        private RectTransform rectTransform;
        private RectTransform safeAreaTransform;

        private void Awake()
        {
            if(!helpers.Contains(this))
                helpers.Add(this);
 
            canvas = GetComponent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
 
            safeAreaTransform = transform.Find("Content") as RectTransform;
 
            if(!screenChangeVarsInitialized)
            {
                lastOrientation = UnityEngine.Screen.orientation;
                lastResolution.x = UnityEngine.Screen.width;
                lastResolution.y = UnityEngine.Screen.height;
                lastSafeArea = UnityEngine.Screen.safeArea;
 
                screenChangeVarsInitialized = true;
            }
 
            ApplySafeArea();
        }

        private void Update()
        {
            if(helpers[0] != this)
                return;
 
            if(Application.isMobilePlatform && UnityEngine.Screen.orientation != lastOrientation)
                OrientationChanged();
 
            if(UnityEngine.Screen.safeArea != lastSafeArea)
                SafeAreaChanged();
 
            if(UnityEngine.Screen.width != lastResolution.x || UnityEngine.Screen.height != lastResolution.y)
                ResolutionChanged();
        }

        private void ApplySafeArea()
        {
            if(safeAreaTransform == null)
                return;
 
            var safeArea = UnityEngine.Screen.safeArea;
 
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= canvas.pixelRect.width;
            anchorMin.y /= canvas.pixelRect.height;
            anchorMax.x /= canvas.pixelRect.width;
            anchorMax.y /= canvas.pixelRect.height;
 
            safeAreaTransform.anchorMin = anchorMin;
            safeAreaTransform.anchorMax = anchorMax;
        }

        private void OnDestroy()
        {
            if(helpers != null && helpers.Contains(this))
                helpers.Remove(this);
        }
 
        private static void OrientationChanged()
        {
            lastOrientation = UnityEngine.Screen.orientation;
            lastResolution.x = UnityEngine.Screen.width;
            lastResolution.y = UnityEngine.Screen.height;
        }
 
        private static void ResolutionChanged()
        {
            lastResolution.x = UnityEngine.Screen.width;
            lastResolution.y = UnityEngine.Screen.height;
        }
 
        private static void SafeAreaChanged()
        {
            lastSafeArea = UnityEngine.Screen.safeArea;
            for (int i = 0; i < helpers.Count; i++)
            {
                helpers[i].ApplySafeArea();
            }
        }
    }
}