using System;
using System.Collections;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Market
{
    public class EEScreenshot : EEBehaviourUpdate
    {
        public void Call()
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/../screenshots/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png");
            print("Screenshot MADE!");
        }
    }
}
