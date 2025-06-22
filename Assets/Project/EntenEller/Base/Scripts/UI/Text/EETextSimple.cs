namespace Project.EntenEller.Base.Scripts.UI.Text
{
    public class EETextSimple : EEText
    {
        protected override void Change()
        {
            var translation = GetTranslated();
            Set(translation);
        }
    }
}
