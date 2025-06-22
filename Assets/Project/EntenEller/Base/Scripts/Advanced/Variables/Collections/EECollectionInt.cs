using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Save;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables.Collections
{
    public class EECollectionInt : EEBehaviour
    {
        public List<int> List;

        private void OnLoad(string value)
        {
            List = EEJSON.Deserialize<List<int>>(value);
        }

        public void Change(int i, int n)
        {
            List[i] = n;
        }
    }
}