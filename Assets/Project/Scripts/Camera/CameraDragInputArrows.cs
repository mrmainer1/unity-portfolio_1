using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using UnityEngine;

public class CameraDragInputArrows : EEBehaviourUpdate
{
    [SerializeField] private float moveSpeed = 4f;
    
    [SerializeField] private CameraDrag cameraDrag;

    protected override void EEUpdate()
    {
        base.EEUpdate();
        MoveCameraWithArrows();
    }

    private void MoveCameraWithArrows()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        var movement = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        cameraDrag.MoveCamera(movement);
    }
}
