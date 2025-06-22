using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Variables;

namespace Project.EntenEller.Base.Scripts.UI.Text
{
    public class EETextEEValueByVariable : EEVariableFinder
    {
        protected override void Change()
        {
            GetSelf<EEText>().SetData(VariablesInfo.First().Variables.First().Value.ToString());
        }
    }
}