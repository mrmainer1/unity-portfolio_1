using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Meshes
{
    public class EEMask3D : EEBehaviour
    {
        public void SetRender(int order)
        {
            if (GetSelf<MeshRenderer>()) GetSelf<MeshRenderer>().material.renderQueue = order;
            if (GetSelf<SkinnedMeshRenderer>()) GetSelf<SkinnedMeshRenderer>().material.renderQueue = order;
        }
    }
}