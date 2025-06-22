using System;
using System.Collections.Generic;
using System.IO;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Save
{
    public class EESaverSourceFile : EESaverSource
    {
        private CoroutineHandle saveCoroutine;
        private readonly Dictionary<string, string> dataToWrite = new Dictionary<string, string>();
        public int frameLoad;
        private Dictionary<string, string> fileData = new Dictionary<string, string>();

        private static string GetFilePath()
        {
            return Path.Combine(Application.persistentDataPath, EESingleton.Get<EESaverSlot>().SlotID + ".sav");
        }

        public override void Save(EESaver saver)
        {
            if (!saveCoroutine.IsRunning) saveCoroutine = Timing.RunCoroutine(SaveCoroutine());
            dataToWrite.Add(saver.FullKey, saver.Value);
        }

        private IEnumerator<float> SaveCoroutine()
        {
            yield return Timing.WaitForOneFrame;
            saveCoroutine.IsRunning = false;
            var path = GetFilePath();
            var json = EEJSON.Serialize(dataToWrite);
            try
            {
                File.WriteAllText(path, json);
                dataToWrite.Clear();
                SaveFinish();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save data to file: {path}\nException: {ex.Message}");
            }
        }
        
        public override void Load(EESaver saver, Action<string> actionOnLoad)
        {
            if (Time.frameCount != frameLoad)
            {
                frameLoad = Time.frameCount;
                var filePath = GetFilePath();
                try
                {
                    var text = File.ReadAllText(filePath);
                    fileData = EEJSON.Deserialize<Dictionary<string, string>>(text);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to load file: {filePath}\nException: {ex.Message}");
                }
            }
            if (fileData.ContainsKey(saver.FullKey)) LoadFinish(actionOnLoad, fileData[saver.FullKey]);
            else LoadFinish(actionOnLoad, saver.Value);
        }
    }
}
