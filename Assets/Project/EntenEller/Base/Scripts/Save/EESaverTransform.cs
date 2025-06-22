using Project.EntenEller.Base.Scripts.Advanced.Transforms;

namespace Project.EntenEller.Base.Scripts.Save
{
    public class EESaverTransform : EESaver
    {
        public override void LocalSave()
        {
            Save(transform.Stringify());
        }

        public override void LocalLoad()
        {
            Load(OnLoad);

            void OnLoad(string data)
            {
                var (pos, rot, scale) = data.ParseTransform();
                transform.position = pos;
                transform.rotation = rot;
                transform.localScale = scale;
            }
        }
    }
}
