using System.Linq;

namespace Project.EntenEller.Base.Scripts.Save
{
    public class EESaverParent : EESaver
    {
        public override int GetQueue()
        {
            return 99;
        }

        public override void LocalSave()
        {
            var parent = transform.parent;
            var key = "null";
            if (parent != null)
            {
                var saver = parent.GetComponent<EESaver>();
                key = saver.Key;
            }
            Save(key);
        }

        public override void LocalLoad()
        {
            Load(OnLoad);

            void OnLoad(string data)
            {
                if (data == "null") return;
                var target = Savers.First(a => a.Key == data);
                transform.parent = target.transform;
            }
        }
    }
}
