using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Materials
{
    public class EEMaterialsHighlighter : EEBehaviourUpdate
    {
        [SerializeField] private string colorName = "_Color";
        [ReadOnly] [SerializeField] private List<Color> startColors = new List<Color>();
        [SerializeField] private List<Color> highlightColors = new List<Color>();
        [SerializeField] private float lerp = 0.5f;
        private List<Color> targetColors;
        private bool isAvaibleToChange = true;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            foreach (var material in GetSelf<Renderer>().materials)
            {
                startColors.Add(material.GetColor(colorName));
            }
            targetColors = startColors;
        }

        public void Lock()
        {
            isAvaibleToChange = false;
        }
        
        public void Unlock()
        {
            isAvaibleToChange = true;
        }

        public void On()
        {
            if (isAvaibleToChange) targetColors = highlightColors;
        }

        public void Off()
        {
            if (isAvaibleToChange) targetColors = startColors;
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            var materials = GetSelf<Renderer>().materials;
            for (var i = 0; i < materials.Length; i++)
            {
                var material = materials[i];
                var color = material.GetColor(colorName);
                color = Color.Lerp(color, targetColors[i], lerp);
                material.SetColor(colorName, color);
            }
        }
    }
}
