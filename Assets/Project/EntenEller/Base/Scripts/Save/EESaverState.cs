using Project.EntenEller.Base.Scripts.Advanced.Variables;

namespace Project.EntenEller.Base.Scripts.Save
{
    public class EESaverState : EESaver
    {
        public override int GetQueue()
        {
            return 100;
        }

        public override void LocalSave()
        {
            Save(gameObject.activeSelf.Stringify());
        }

        public override void LocalLoad()
        {
            Load(OnLoad);

            void OnLoad(string data)
            {
                var isActive = data.ParseBool();
                gameObject.SetActive(isActive);
            }
        }
    }
}
