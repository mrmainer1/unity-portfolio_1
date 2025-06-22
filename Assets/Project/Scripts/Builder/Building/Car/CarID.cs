using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.Scripts.Skin;
using UnityEngine;

namespace Project.Scripts.Builder.Building.Car
{
   public class CarID : EEBehaviour
   {
      [SerializeField] private int carID;
      [SerializeField] private CarSkinID carSkinID;
      protected override void EEDisable()
      {
         base.EEDisable();
         ClearCarId();
      }

      public int GetCarID()
      {
         return carID;
      }
      public void SetCarID(int id) => carID = id;

      public void UpdateVisual(int id)
      {
         carID = id;
         carSkinID.ApplyCarSkin(carID);
      }

      public void ClearCarId() => carID = 0;
   }
}
