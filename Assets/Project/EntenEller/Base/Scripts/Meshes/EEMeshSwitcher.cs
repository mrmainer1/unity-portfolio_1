using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Meshes
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class EEMeshSwitcher : EEBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private List<MeshData> meshData;
        
#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying) return;
            for (var i = 0; i < meshData.Count; i++)
            {
                if (i != index) continue;
                Switch(i);
            }
        }
#endif

        public void SetMesh(Mesh mesh)
        {
            meshData.ForEach(n => n.Mesh = mesh);
        }

        public List<MeshData> GetMeshData()
        {
            return meshData;
        }
        public void Switch(int i)
        {
            index = i;
            var md = meshData[index];
            if (GetSelf<MeshFilter>())
            {
                GetSelf<MeshFilter>().mesh = md.Mesh;
                GetSelf<MeshRenderer>().materials = md.Materials;
            }

            if (GetSelf<SkinnedMeshRenderer>())
            {
                GetSelf<SkinnedMeshRenderer>().sharedMesh = md.Mesh;
                GetSelf<SkinnedMeshRenderer>().materials = md.Materials;
            }
        }

        [Serializable]
        public class MeshData
        {
            public Mesh Mesh;
            public Material[] Materials = new Material[0];
        }
    }
}
