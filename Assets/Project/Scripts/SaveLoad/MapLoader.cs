using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.UI.InputField;
using Project.Scripts.Builder.Building;
using Project.Scripts.Builder.Building.Car;
using Project.Scripts.Map;
using Project.Scripts.Skin;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.SaveLoad
{
    public class MapLoader : EEBehaviour
    {
        [TextArea] public string JSON;

        public static bool IsLoaded;

        [Button]
        private void LoadMapDataFromJSON()
        {
            MapClear.Clear();
            
            var mapData = EEJSON.Deserialize<MapData>(JSON);
            var mapResizer = EESingleton.Get<MapResizer>();
            
            mapResizer.SetSize(mapData.Width,mapData.Height);
            mapResizer.ChangeSizeFinishNotifier.Event += OnChangeSizeFinish;
            
            
            void OnChangeSizeFinish()
            {
                mapResizer.ChangeSizeFinishNotifier.Event -= OnChangeSizeFinish;
                foreach (var building in mapData.List)
                {
                    var spawner = EETagUtils.FindEETagInScenes("spawner-" + building.IDSpawner);
                    CreateBuilding(building, spawner.GetSelf<EESpawner>());
                }

                IsLoaded = true;
            }
        }

        public void Load(string json)
        {
            JSON = json;
            LoadMapDataFromJSON();
        }

        private void CreateBuilding(BuildingData buildingData, EESpawner spawner)
        {
            var spawnBuilding = spawner.Spawn();
            var building = spawnBuilding.GetSelf<Building>();

            if (building.GetSelf<EETagHolder>().LastTag != "building-symbol")
                spawnBuilding.GetChild<BuildingRotator>().SetQuadrant(buildingData.Quadrant);
            
            building.Place(buildingData.X, buildingData.Y);
            spawnBuilding.transform.position = new Vector3(buildingData.X,0,buildingData.Y);

            if (building.GetSelf<EETagHolder>().LastTag == "building-car")
            {
                SetDataInputField(buildingData.Row, building, "row");
                SetDataInputField(buildingData.Number, building, "place");

                var carID = building.GetSelf<CarID>();
                carID.SetCarID(buildingData.IDCar);
                
                building.GetChild<CarSkinID>().ApplyCarSkin(carID.GetCarID());
            }
            
            if (building.GetSelf<EETagHolder>().LastTag == "building-symbol")
                 SetDataInputField(buildingData.Additional,building,"input-field");

        }

        private void SetDataInputField(int value, Building building, string tagField)
        {
            var inputField = EETagUtils.FindEETagInChildren(building, tagField).GetSelf<EEInputField>();
            if (value != 0) inputField.Set(value.ToString());
        }
        
    }
}
