using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.Scripts.Builder.Building;
using Project.Scripts.HTTP;
using Project.Scripts.Map;

namespace Project.Scripts.SaveLoad
{
    public class MapSaver : EEBehaviour
    {
        public MapData Data;
        public List<BuildingData> buildingDataList;
        public string JSONData;
        public EENotifier SaveSuccessNotifier;

        private bool isValidSave = true;

        protected override void EEAwake()
        {
            base.EEAwake();
            Building.FailPlaceEvent += OnFailPlace;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            Building.FailPlaceEvent -= OnFailPlace;
        }

        public void Save()
        {
            if (!isValidSave) return;
            if (!MapLoader.IsLoaded) return;

            var map = EESingleton.Get<MapResizer>();

            var mapData = new MapData
            {
                Id = 1,
                Width = map.Width,
                Height = map.Height,
                List = buildingDataList
                
            };
            Data = mapData;
            GenerateJSON(Data);
            SaveSuccessNotifier.Notify();
        }

        public void AddBuilding(BuildingData buildingData)
        {
            var found = false;
            for (int i = 0; i < buildingDataList.Count; i++)
            {
                if (buildingDataList[i].ID != buildingData.ID) continue;
                buildingDataList[i] = buildingData;
                found = true;
                break;
            }

            if (!found)
            {
                buildingDataList.Add(buildingData);
            }
            Save();
        }

        public void DeleteBuilding(BuildingData buildingData)
        {
            if (buildingData == null) return;

            for (int i = 0; i < buildingDataList.Count; i++)
            {
                if (buildingDataList[i].ID != buildingData.ID) continue;
                buildingDataList.RemoveAt(i);
                Save();
                return;
            }
        }

        private void GenerateJSON(MapData mapData)
        {
            JSONData = EEJSON.Serialize(mapData);
        }


        public void DeleteAllBuildings() => buildingDataList.Clear();

        private void OnFailPlace() => isValidSave = false;
    }
}
