using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
public class CameraDragView : EEBehaviour
{
    [SerializeField] private Transform cameraNode;
    public Vector3 CurrentPosition => cameraNode.position; 

    public void UpdateCameraPosition(Vector3 pos)
    {
        cameraNode.position += pos;
    }
}
