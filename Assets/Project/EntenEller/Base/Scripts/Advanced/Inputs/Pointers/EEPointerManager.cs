using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Pointers
{
    [ExecutionOrder(-9000)]
    
    public class EEPointerManager : EEBehaviourUpdate
    {
        private const int maxMouseButtons = 5;
        private const int maxPointers = 128;
        
        [ReadOnly] public List<PointerData> PointersData = new List<PointerData>();

        protected override void EEAwake()
        {
            base.EEAwake();
            for (var i = 0; i < maxPointers; i++)
            {
                PointersData.Add(new PointerData());
            }
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            #if UNITY_WEBGL
                Mouse();
                return;
            #endif
            if (EEPointerUtils.IsTouchDevice()) Touch();
            else Mouse();
        }
        
        private void Touch()
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);
                var pointerData = PointersData[i];
                pointerData.UpdatePosition(touch.position);
                if (pointerData.IsDown) pointerData.DownHold();
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        pointerData.Down();
                        break;
                    case TouchPhase.Ended:
                        pointerData.Up();
                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        private void Mouse()
        {
            for (var i = 0; i < maxMouseButtons; i++)
            {
                var pointerData = PointersData[i];
                pointerData.UpdatePosition(Input.mousePosition);
                if (Input.GetMouseButtonDown(i)) pointerData.Down();
                else if (Input.GetMouseButtonUp(i)) pointerData.Up();
                if (pointerData.IsDown) pointerData.DownHold();
            }
        }

        [Serializable]
        public class PointerData
        {
            private const float maxDistanceToCancelClick = 5;
            
            [ReadOnly] public bool IsDown;
            
            [ReadOnly] public Vector2 PositionOnDown;
            [ReadOnly] public Vector2 DeltaPosition;
            [ReadOnly] public Vector2 Position;

            public Action RawDownEvent, RawUpEvent, RawClickEvent, RawDownHoldEvent;
            public Action<Vector2> RawDragEvent;

            public void UpdatePosition(Vector2 pos)
            {
                if (pos.IsAlmostEqual(Position))
                {
                    DeltaPosition = Vector2.zero;
                    return;
                }
                DeltaPosition = pos - Position;
                Position = pos;
                if (IsDown) RawDragEvent.Call(DeltaPosition);
            }
            
            public void Down()
            {
                IsDown = true;
                PositionOnDown = Position;
                RawDownEvent.Call();
            }
            
            public void DownHold()
            {
                RawDownHoldEvent.Call();
            }
            
            public void Up()
            {
                IsDown = false;
                if (Position.IsAlmostEqual(PositionOnDown, maxDistanceToCancelClick)) RawClickEvent.Call();
                RawUpEvent.Call();
            }
        }
    }
}
