using DG.Tweening;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;

namespace Project.EntenEller.Base.Scripts.Advanced.Animators.DotTweens
{
    public class EEDotTweenController : EEBehaviour
    {
        private DOTweenAnimation[] _cache;
        private DOTweenAnimation[] cache
        {
            get
            {
                if (_cache != null) return _cache;
                _cache = GetComponents<DOTweenAnimation>();
                _cache.ForEach(a => a.CreateTween());
                return _cache;
            }
        }

        public void Restart()
        {
            cache.ForEach(a => a.DORestart());
        }
        
        public void PlayForward()
        {
            cache.ForEach(a => a.DOPlayForward());
        }
        
        public void PlayBackward()
        {
            cache.ForEach(a => a.DOPlayBackwards());
        }

        public void SetDelays(float delay)
        {
            cache.ForEach(a => a.delay = delay);
        }
    }
}
