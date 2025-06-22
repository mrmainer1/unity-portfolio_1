using System;
using System.Text.RegularExpressions;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Pool;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.EntenEller.Base.Scripts.UI.InputField;
using Project.Scripts.Builder.Building;
using Project.Scripts.Builder.Building.Car;
using Project.Scripts.Map;

namespace Project.Scripts.SaveLoad
{
    public class CollelctBuildingData : EEBehaviour
    {
        private BuildingData buildingData;

        public void SaveBuilding()
        {
            var obj =  gameObject.GetEEGameObject();

            var quadrant = 0;
            var buildinfRotator = obj.GetChild<BuildingRotator>();
            if (buildinfRotator != null)
                quadrant = buildinfRotator.GetQuadrant();

            var id = obj.GetSelf<BuildingID>().GetID();

            var data = new BuildingData()
            {
                ID = id,
                IDSpawner = ExtractNumberFromSpawner(GetSelf<EEPoolObject>().Spawner.GetSelf<EETagHolder>().EETags[2]),
                X = (int) obj.transform.position.x,
                Y = (int) obj.transform.position.z,
                Quadrant = quadrant
            };

            if (obj.GetSelf<EETagHolder>().LastTag == "building-car")
            {
                var inputFieldRow = EETagUtils.FindEETagInChildren(obj, "row").GetSelf<EEInputField>();
                var row = inputFieldRow.Data;

                var inputFieldPlace = EETagUtils.FindEETagInChildren(obj, "place").GetSelf<EEInputField>();
                var place = inputFieldPlace.Data;

                var idCar = obj.GetSelf<CarID>().GetCarID();

                data.IDCar = idCar;
                data.Row = row.ParseInt();
                data.Number = place.ParseInt();
            }

            if (obj.GetSelf<EETagHolder>().LastTag == "building-symbol")
                data.Additional = EETagUtils.FindEETagInChildren(obj, "input-field").GetSelf<EEInputField>().Data.ParseInt();

            buildingData = data;
            EESingleton.Get<MapSaver>().AddBuilding(buildingData);
        }

        public void RemoveBuilding()
        {
            EESingleton.Get<MapSaver>().DeleteBuilding(buildingData);
        }

        private int ExtractNumberFromSpawner(string input)
        {
            var match = Regex.Match(input, @"-(\d+)");

            if (match.Success)
                return int.Parse(match.Groups[1].Value);

            throw new FormatException("The string does not match the expected format.");
        }
    }
}
