using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.WaypointSystem
{
    public class EEWaypointSystem : EEBehaviour
    {
        [SerializeField] private float approximateError = EEConstants.MeasurementAccuracyLow;
        public int AmountOfPoints => GetSelf<LineRenderer>().positionCount;
        
        public Vector3 GetNext(Vector3 position, ref int index, ref int direction, out bool isEnd, int indexTarget = -1, bool isLoop = true)
        {
            isEnd = false;
            var line = GetSelf<LineRenderer>();
            var pos = GetPositionOfIndex(index);
            if (!position.IsAlmostEqual(pos, approximateError)) return pos;

            if (index == indexTarget)
            {
                isEnd = true;
                return pos;
            }
            
            if (direction > 0)
            {
                if (index == line.positionCount - 1)
                {
                    if (!isLoop)
                    {
                        isEnd = true;
                        return GetPositionOfIndex(index);
                    }
                    direction = -1;
                    index--;
                }
                else index++;
            }
            else if (direction < 0)
            {
                if (index == 0)
                {
                    if (!isLoop)
                    {
                        isEnd = true;
                        return GetPositionOfIndex(index);
                    }
                    direction = 1;
                    index++;
                }
                else index--;
            }

            return GetPositionOfIndex(index);
        }

        public Vector3 MoveToIndex(Vector3 position, ref int index, int indexTarget)
        {
            var direction = indexTarget - index;
            return GetNext(position, ref index, ref direction, out _, -1, false);
        }
        
        public Vector3 GetPositionOfIndex(int i)
        {
            return transform.TransformPoint(GetSelf<LineRenderer>().GetPosition(i));
        }
    }
}
