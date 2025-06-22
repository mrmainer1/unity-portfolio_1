using System.Collections.Generic;
using System.Linq;
using MEC;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.UnitTests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.EntenEller.Base.Scripts.UI.Menu.Tests.Resources
{
    public class EETestMenuSystem : EEBehaviourTest
    {
        [SerializeField] private EEMenuResource starting;
        [SerializeField] private EEMenuResource add1;
        [SerializeField] private EEMenuResource add2;
        [SerializeField] private EEMenuResource recreate;
        [SerializeField] private EEMenuResource noHistory;
        
        public IEnumerator<float> TestMenu()
        {
            starting.CleanAll();
            yield return Timing.WaitForOneFrame;
            starting.On();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            yield return Timing.WaitForOneFrame;
            recreate.On();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            yield return Timing.WaitForOneFrame;
            recreate.Off();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            yield return Timing.WaitForOneFrame;
            recreate.On();
            add1.Off();
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            yield return Timing.WaitForOneFrame;
            add1.On();
            add2.On();
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 4);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), add2);
            yield return Timing.WaitForOneFrame;
            starting.Back();
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 3);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), add1);
            yield return Timing.WaitForOneFrame;
            starting.Back();
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            yield return Timing.WaitForOneFrame;
            starting.Back();
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), starting);
            yield return Timing.WaitForOneFrame;
            starting.Back();
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            yield return Timing.WaitForOneFrame;
            starting.Back();
            starting.On();
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(EETagUtils.FindEETagInScenes(starting.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            yield return Timing.WaitForOneFrame;
            recreate.On();
            add1.On();
            add2.On();
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(EETagUtils.FindEETagInScenes(starting.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                false);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(recreate.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(add1.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(add2.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            yield return Timing.WaitForOneFrame;
            add1.CleanUntil();
            yield return Timing.WaitForOneFrame;
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(starting.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                false);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(recreate.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(add1.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            Assert.AreEqual(EETagUtils.FindEETagInScenes(add2.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                false);
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 3);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.First(), starting);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), add1);
            yield return Timing.WaitForOneFrame;
            starting.CleanAll();
            starting.On();
            starting.On();
            yield return Timing.WaitForOneFrame;
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            
            yield return Timing.WaitForOneFrame;
            add1.On();
            yield return Timing.WaitForOneFrame;
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), add1);
            
            yield return Timing.WaitForOneFrame;
            add1.Off();
            add1.Off();
            yield return Timing.WaitForOneFrame;
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 1);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), starting);
            
            yield return Timing.WaitForOneFrame;
            starting.CleanAll();
            
            starting.On();
            recreate.On();
            noHistory.On();
            yield return Timing.WaitForOneFrame;
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            Assert.AreEqual(EETagUtils.FindEETagInScenes(noHistory.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                true);
            
            yield return Timing.WaitForOneFrame;
            noHistory.Off();
            yield return Timing.WaitForOneFrame;
            
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 2);
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Last(), recreate);
            Assert.AreEqual(EETagUtils.FindEETagInScenes(noHistory.MenuEETags.First()).GetSelf<EEMenu>().IsActive,
                false);
            yield return Timing.WaitForOneFrame;
            starting.CleanAll();
            yield return Timing.WaitForOneFrame;
            Assert.AreEqual(EESingleton.Get<EEMenuManager>().History.Count, 0);
        }
    }
}
