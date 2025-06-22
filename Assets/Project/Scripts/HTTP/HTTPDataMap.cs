using System;
using System.Collections.Generic;

namespace Project.Scripts.HTTP
{
    public class HTTPDataMap : HTTPData
    {
          public Data data;
          
          [Serializable]
          public class Data
          {
              public List<Building> buildings;
              public List<MapInfo> map;
          }
  
          [Serializable]
          public class Building
          {
              public string x;
              public string y;
              public string angle;
              public string seat_row;
              public string seat_number;
              public string id_car;
              public string id;
              public string id_building;
              public string additional_data;
          }
  
          [Serializable]
          public class MapInfo
          {
              public string id;
              public string width;
              public string height;
          }
    }
}