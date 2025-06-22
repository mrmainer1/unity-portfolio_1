using Project.EntenEller.Base.Scripts.Advanced.Cameras;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

public class CameraZoom : EEBehaviour
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoom = 5f; 
    [SerializeField] private float maxZoom = 50f;

    private Camera mainCamera;
    private float scroll;

    public EENotifier ZoomNotifier;
    protected override void EEAwake()
    {
        base.EEAwake();
        mainCamera = EECameraUtils.MainCamera;
    }

    public void Zoom(float scrollInput)
    {
        scroll = scrollInput;
        if(mainCamera.orthographic)
            OrthographicsZoom();
        else
            PerspectiveZoom();    
        ZoomNotifier.Notify();
    }

    private void OrthographicsZoom()
    {
        mainCamera.orthographicSize -= scroll * zoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
    }
    
    private void PerspectiveZoom()
    {
        mainCamera.fieldOfView -= scroll * zoomSpeed;
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, minZoom, maxZoom);
    }
}
