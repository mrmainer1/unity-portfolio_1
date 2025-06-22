using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Cameras;
using Project.Scripts.Map.Cell;
using UnityEngine;

namespace Project.Scripts.Builder.Building
{
    public class BuildingMouseFollower : EEBehaviourUpdate
    {
        [SerializeField] private Building building;
        private Vector3 position = new Vector3();
        private MapCellHolder mapCellHolder;
        public Vector2Int Cell;

        private bool canMove;
        protected override void EEAwake()
        {
            base.EEAwake();
            mapCellHolder = building.mapCellHolder;
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();

            var ray = EECameraUtils.MainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (building.IsValid)
            {
                Cell = mapCellHolder.Cell;
                position.x = Cell.x;
                position.z = Cell.y;
            }
            else
            {
                position.x = hit.point.x - building.Width / 2f;
                position.z = hit.point.z - building.Height / 2f;
            }

            transform.parent.position = position;
        }
    }
}
