using Project.EntenEller.Base.Scripts.Advanced.Cameras;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

public class SetCameraInCanvas : EEBehaviour
{
   [SerializeField] private Canvas canvas;

   protected override void EEAwake()
   {
      base.EEAwake();
      canvas.worldCamera = EECameraUtils.MainCamera;
   }
}
