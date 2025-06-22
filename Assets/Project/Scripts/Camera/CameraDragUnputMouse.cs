using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;
using UnityEngine;

public class CameraDragUnputMouse : EEBehaviourUpdate
{
    [SerializeField] private float maxMovement = 0.4f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private CameraDrag cameraDrag;
    [SerializeField] private EEPointerUI pointerUI;
    
    private Vector3 lastMousePosition;
    private bool isFirstMove = true;
    protected override void EEAwake()
    {
        base.EEAwake();
        lastMousePosition = Input.mousePosition;
        pointerUI.DragNotifier.Event += MoveCameraWithMouse;
    }
    
    private void MoveCameraWithMouse()
    {
        var mouseDelta = Input.mousePosition - lastMousePosition;

        if (isFirstMove)
        {
            lastMousePosition = Input.mousePosition;
            isFirstMove = false;
            return;
        }
        
        var targetMovement = new Vector3(-mouseDelta.x, 0, -mouseDelta.y) * moveSpeed * Time.deltaTime;
        targetMovement = Vector3.ClampMagnitude(targetMovement, maxMovement);
        
        cameraDrag.MoveCamera(targetMovement);

        lastMousePosition = Input.mousePosition;
    }


}