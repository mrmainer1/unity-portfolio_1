using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Editors;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Animators.Editor
{
    public class EENestedAnimator : OdinEditorWindow
    {
        [Title("Animator Name")]
        public string animatorName;
        [Title("Animations Names")]
        public List<string> animations = new List<string>();

        [MenuItem("Window/EntenEller/Nested Animator")]
        private static void OpenWindow()
        {
            GetWindow<EENestedAnimator>().Show();
        }

        protected override void OnImGUI()
        {
            base.OnImGUI();
            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("Save", GUILayout.Height(30)))
            {
                Save();
            }
            EditorGUILayout.EndVertical();
        }
        
        private void Save()
        {
            if (IsAnimatorNameEmpty()) return;
            if (IsAnimationAmountIsZero()) return;
            if (IsAnimationNameEmpty()) return;
            TryToSave();
      
            bool IsAnimatorNameEmpty()
            {
                animatorName = EditorGUILayout.TextField("Animator Name", animatorName);
                var isError = string.IsNullOrEmpty(animatorName);
                if (isError) EEInfoWindowUtils.ShowInfoWindow("Error!", "Empty animator name!");
                return isError;
            }
            
            bool IsAnimationAmountIsZero()
            {
                var isError = animations.Count == 0;
                if (isError) EEInfoWindowUtils.ShowInfoWindow("Error!", "Animator has zero animations!");
                return isError;
            }

            bool IsAnimationNameEmpty()
            {
                var isError = animations.Any(string.IsNullOrEmpty);
                if (isError) EEInfoWindowUtils.ShowInfoWindow("Error!", "Animator has zero animations!");
                return isError;
            }

            void TryToSave()
            {
                var result = EESaveFilePanelUtils.TryToGetSavePath(this, animatorName, "controller", out var pathToSave);
                if (!result) return;
                var animator = AnimatorController.CreateAnimatorControllerAtPath(pathToSave);
                foreach (var animationClip in animations.Select(animation => new AnimationClip {name = animation}))
                {
                    AssetDatabase.AddObjectToAsset(animationClip, animator);
                    AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(animationClip));
                }
                EEInfoWindowUtils.ShowInfoWindow("Success!", "Nested animator was created!");
            }
        }
    }
}