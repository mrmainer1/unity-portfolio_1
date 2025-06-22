using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Misc
{
    public class MouseClickVisualizer : EEBehaviour
    {
       // [SerializeField] private Transform nodeToStick = null;
       // [SerializeField] private EEPoolObject poolObject = null;
        
        protected override void EEEnable()
        {
            base.EEEnable();
        //    EESingleton.Get<EEMouse>().MouseLeftDownEvent += Spawn;
        }
        
        protected override void EEDisable()
        {
            base.EEDisable();
           // EESingleton.Get<EEMouse>().MouseLeftDownEvent -= Spawn;
        }

        private void Spawn()
        {
          //  var obj = EEPool.SpawnPoolObject(poolObject);
          //  obj.GetSelf<Transform>().SetParentAdvanced(nodeToStick);
          //  obj.GetSelf<Transform>().position = EESingleton.Get<EEMouse>().Position;
           // obj.Enable();
        }
    }
}
