using System.Diagnostics;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Kiosk
{
    public class KioskWindowsExplorerKiller : EEBehaviour
    {
        public void Call()
        {
            if (Application.platform != RuntimePlatform.WindowsPlayer) return;
            var processInfo = new ProcessStartInfo("taskkill", "/F /IM explorer.exe")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var process = new Process
            {
                StartInfo = processInfo
            };

            process.Start();
            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            if (!string.IsNullOrEmpty(output))
            {
                UnityEngine.Debug.Log("Output: " + output);
            }

            if (!string.IsNullOrEmpty(error))
            {
                UnityEngine.Debug.LogError("Error: " + error);
            }
        }
    }
}
