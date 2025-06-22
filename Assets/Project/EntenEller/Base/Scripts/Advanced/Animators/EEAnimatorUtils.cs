using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Animators
{
    public static class EEAnimatorUtils
    {
        public static int GetFrame(Animator animator, int indexLayer, int indexAnimation, float position)
        {
            var clip = animator.GetCurrentAnimatorClipInfo(indexLayer)[indexAnimation].clip;
            var currentFrame = (int) (clip.frameRate * clip.length * position);
            return currentFrame;
        }
    }
}