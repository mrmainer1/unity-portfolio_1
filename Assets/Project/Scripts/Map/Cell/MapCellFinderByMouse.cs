using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Cameras;
using UnityEngine;

namespace Project.Scripts.Map.Cell
{
    public class MapCellFinderByMouse : EEBehaviourUpdate
    {
        [SerializeField] private MapCellHolder CellHolder;
        public Vector2Int CursorCellPosition;
        private RaycastHit hit;
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            var ray = EECameraUtils.MainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit)) return;
            GetCellFromWorldPosition(hit.point);
        }
        
        public void GetCellFromWorldPosition(Vector3 worldPosition)
        {
            var x = Mathf.FloorToInt(worldPosition.x);
            var y = Mathf.FloorToInt(worldPosition.z);
            CursorCellPosition.x = worldPosition.x - x >= 0.5f ? 1 : -1;
            CursorCellPosition.y = worldPosition.z - y >= 0.5f ? 1 : -1;
            CellHolder.Refresh(x, y);
        }
    }
}
