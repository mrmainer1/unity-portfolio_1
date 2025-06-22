using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using UnityEngine;

public class CameraZoomInputMouse : EEBehaviourUpdate
{
  [SerializeField] private CameraZoom cameraZoom;

  protected override void EEUpdate()
  {
    base.EEUpdate();
    cameraZoom.Zoom(Input.GetAxis("Mouse ScrollWheel"));
  }
}
