using Project.EntenEller.Base.Scripts.Advanced.Spawns;

namespace Project.EntenEller.Base.Scripts.UnitTests
{
    public class EEUnitTestingButtonsBuilder : EESpawnerEEGameObject
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            EEUnitTestingClassAndFunctionGetter.GotDataEvent += (test, type, method) =>
            {
                var button = Spawn();
                button.GetSelf<EEUnitTestButton>().Setup(test, type, method);
            };
        }
    }
}
