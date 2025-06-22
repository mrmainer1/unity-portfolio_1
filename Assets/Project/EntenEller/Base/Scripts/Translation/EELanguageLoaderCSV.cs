using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Files;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Translation
{
    public class EELanguageLoaderCSV : EEBehaviour
    {
        [SerializeField] private string path;

        public void Call()
        {
            var data = EECSV.Read(Application.streamingAssetsPath + "/" + path);
            foreach (var list in data)
            {
                var key = list.First();
                list.RemoveAt(0);
                EESingleton.Get<EELanguageManager>().AddData(key, list);
            }
            EESingleton.Get<EELanguageManager>().Switch(0);
        }
    }
}
