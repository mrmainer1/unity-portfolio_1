using System;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Save
{
    public class EESaverSourcePlayerPrefs : EESaverSource
    {
        public override void Save(EESaver saver)
        {
            PlayerPrefs.SetString(saver.FullKey, saver.Value);
            SaveFinish();
        }

        public override void Load(EESaver saver, Action<string> actionOnLoad)
        {
            if (!PlayerPrefs.HasKey(saver.FullKey))
            {
                LoadFinish(actionOnLoad, saver.DefaultValue);
                return;           
            }
            LoadFinish(actionOnLoad, PlayerPrefs.GetString(saver.FullKey));
        }
    }
}
