using System;
using System.Globalization;
using Project.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Project.EntenEller.Base.Scripts.Translation;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.UI.Text
{
    public class EETextAnimationNumberLerp : EETextSimple
    {
        public EEFloatApproach Approach;
        [SerializeField] private int FractionalPart = 0;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (Data == "") return;
            Approach.Current = int.Parse(Data);
        }

        public void Set(float value)
        {
            Approach.SetTarget(value);
        }

        protected override void MenuLoop()
        {
            base.MenuLoop();
            if (!Approach.IsReached)
            {
                SetData(Math.Round(Approach.Current, FractionalPart).ToString(CultureInfo.InvariantCulture));
            }
            Approach.Proceed();
        }
    }
}
