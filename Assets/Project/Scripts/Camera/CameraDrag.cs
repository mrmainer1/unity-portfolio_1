using Project.EntenEller.Base.Scripts.Advanced.Cameras;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.Scripts.Map;
using UnityEngine;

public class CameraDrag : EEBehaviour
{
    [SerializeField] private float smoothingFactor = 0.1f;
    [SerializeField] private CameraDragView cameraDragView;

    private Vector3 fieldMinBounds = Vector3.zero;  
    private Vector3 fieldMaxBounds = Vector3.zero;
    private float minX, minY, maxX, maxY;
    private Camera mainCamera;
    
    protected override void EEAwake()
    {
        base.EEAwake();
        EESingleton.Get<MapResizer>().ChangeSizeFinishNotifier.Event += CalculateBorder;
        EESingleton.Get<CameraZoom>().ZoomNotifier.Event += CalculateBorder;
        mainCamera = EECameraUtils.MainCamera;
    }
    
    protected override void EEDisable()
    {
        base.EEDisable();
        EESingleton.Get<MapResizer>().ChangeSizeFinishNotifier.Event -= CalculateBorder;
        EESingleton.Get<CameraZoom>().ZoomNotifier.Event -= CalculateBorder;
    }
    public void MoveCamera(Vector3 movement)
    {
        var targetPosition = cameraDragView.CurrentPosition + movement;
    
        var x = Mathf.Clamp(targetPosition.x, fieldMinBounds.x, fieldMaxBounds.x);
        var z = Mathf.Clamp(targetPosition.z, fieldMinBounds.z, fieldMaxBounds.z);
    
        targetPosition = new Vector3(x, targetPosition.y, z);
        Vector3 smoothMovement = Vector3.Lerp(cameraDragView.CurrentPosition, targetPosition, smoothingFactor);
        cameraDragView.UpdateCameraPosition(smoothMovement - cameraDragView.CurrentPosition);
    }
    
    private void CalculateBorder()
    {
        var mapResizer = EESingleton.Get<MapResizer>();
        float cameraSize = mainCamera.orthographicSize;
        
        minY = (mapResizer.Height <= cameraSize * 2 - 4) ? mapResizer.Height / 2 : cameraSize - 1;
        maxY = (mapResizer.Height <= cameraSize * 2 - 4) ? mapResizer.Height / 2 : mapResizer.Height - cameraSize + 3;

        minX = (mapResizer.Width <= cameraSize * 2) ? mapResizer.Width / 2 : cameraSize;
        maxX = (mapResizer.Width <= cameraSize * 2) ? mapResizer.Width / 2 : mapResizer.Width - cameraSize - 1;
        
        fieldMinBounds = new Vector3(minX, 0, minY);
        fieldMaxBounds = new Vector3(maxX, 0, maxY);
    }
}
