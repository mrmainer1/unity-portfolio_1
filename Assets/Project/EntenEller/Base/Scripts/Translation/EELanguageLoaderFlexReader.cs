using System.Collections;
using System.Collections.Generic;
using System.IO;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using FlexFramework.Excel;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine.Networking;

namespace Project.EntenEller.Base.Scripts.Translation
{
    public class EELanguageLoaderFlexReader : EEBehaviour
    {
        [SerializeField] private string path;
        private WorkBook book;
        
        public void Call()
        {
            StartCoroutine(Load(Path.Combine(Application.streamingAssetsPath, path)));
        }
        
        private IEnumerator Load(string fullPath)
        {
            using (var req = UnityWebRequest.Get(fullPath))
            {
                yield return req.SendWebRequest();
                var bytes = req.downloadHandler.data;
                book = new WorkBook(bytes);
                Body();
            }
        }

        private void Body()
        {
            var sheet = book[0];
            var n = 0;
            foreach (var row in sheet)
            {
                n++;
                var values = new List<string>();
                for (var i = 1; i < row.Count; i++)
                {
                    var value = "ERROR VALUE " + i;
                    if (row[i].IsString)
                    {
                        value = row[i];
                    }
                    values.Add(value);
                }
                var key = "ERROR KEY " + n;
                if (row[0].IsString)
                {
                    key = row[0].String.Trim();
                }
                EESingleton.Get<EELanguageManager>().AddData(key, values);
            }
            EESingleton.Get<EELanguageManager>().Switch(0);
        }
    }
}
