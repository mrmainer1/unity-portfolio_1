using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.UI;
using Project.EntenEller.Base.Scripts.UI.Menu;
using Project.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;

public class HoverInformationView : EEBehaviour
{
   [SerializeField] private EEUIWorldSyncPoint worldSyncPoint;
   [SerializeField] private EEMenu menu;
   [SerializeField] private EEText usernameText, carNumberText;

   protected override void EEAwake()
   {
      base.EEAwake();
      EESingleton.Get<HoverInformationMouseInput>().HaveTargetNotifier.Event += Active;
      EESingleton.Get<HoverInformationMouseInput>().NotHaveTargetNotifier.Event += Disable;
   }

   protected override void EEDestroy()
   {
      base.EEDestroy();
      EESingleton.Get<HoverInformationMouseInput>().HaveTargetNotifier.Event -= Active;
      EESingleton.Get<HoverInformationMouseInput>().NotHaveTargetNotifier.Event -= Disable;
   }

   public void SetTarget(EEGameObject eeGameObject)
   {
      worldSyncPoint.Target.SetGameObject(eeGameObject);
      worldSyncPoint.Target.Restart();
   }

   public void SetInformation(string username, string carNumber)
   {
      usernameText.SetData(username);
      carNumberText.SetData(carNumber);
   }

   private void Active()
   {
      menu.SetState(true);
   }
   private void Disable()
   {
      menu.SetState(false);
   }
}
