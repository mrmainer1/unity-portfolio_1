using Project.EntenEller.Base.Scripts.UI.Toggles;

namespace Project.EntenEller.Base.Scripts.Save
{
    [ExecuteAfter(typeof(EEToggle))]
    public class EESaverEEToggle : EESaver
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEToggle>().ValueChangedEvent += OnValueChange;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEToggle>().ValueChangedEvent -= OnValueChange;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            LocalLoad();
        }

        private void OnValueChange(bool isOn)
        {
            LocalSave();
        }

        public override void LocalSave()
        {
            Save(GetSelf<EEToggle>().IsOn ? "1" : "0");
        }

        public override void LocalLoad()
        {
            Load(OnLoad);

            void OnLoad(string data)
            {
                GetSelf<EEToggle>().SetState(data == "1");
            }
        }
    }
}
