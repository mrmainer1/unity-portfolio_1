using System.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Misc
{
    public class EEMouseCellFollower2D : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            StartCoroutine(Loop());

            IEnumerator Loop()
            {
                while (true)
                {
                    yield return null;
                    // var position = EESingleton.Get<EEMouse>().WorldPosition2D;
                    //  position.x = (int) position.x + 0.5f;
                    //  position.y = (int) position.y + 0.5f;
                    //  GetSelf<Transform>().position = position;
                }
            }
        }
    }
}
