using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformApproachPRS : EEBehaviour
    {
        public void SetTarget(Transform ts)
        {
            GetSelf<EETransformApproachPosition>().SetTarget(ts);
            GetSelf<EETransformApproachRotation>().SetTarget(ts);
            GetSelf<EETransformApproachScale>().SetTarget(ts);
        }

        public void SetTarget(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GetSelf<EETransformApproachPosition>().Position.SetTarget(position);
            GetSelf<EETransformApproachRotation>().Rotation.SetTarget(rotation);
            GetSelf<EETransformApproachScale>().Scale.SetTarget(scale);
        }

        public void SetApproachStyles(Approach positionApproach, Approach rotationApproach, Approach scaleApproach)
        {
            GetSelf<EETransformApproachPosition>().Position.Approach = positionApproach;
            GetSelf<EETransformApproachRotation>().Rotation.Approach = rotationApproach;
            GetSelf<EETransformApproachScale>().Scale.Approach = scaleApproach;
        }
    }
}
