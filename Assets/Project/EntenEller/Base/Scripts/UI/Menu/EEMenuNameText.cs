using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.Text;

namespace Project.EntenEller.Base.Scripts.UI.Menu
{
    public class EEMenuNameText : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            EEMenuManager.GotNewNameEvent += GotNewName;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            EEMenuManager.GotNewNameEvent -= GotNewName;
        }

        private void GotNewName(string menuName)
        {
            GetSelf<EEText>().SetData(menuName);
        }
    }
}
