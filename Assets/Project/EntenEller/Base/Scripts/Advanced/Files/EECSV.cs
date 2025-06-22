using System;
using System.Collections.Generic;
using System.IO;
using NReco.Csv;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;

namespace Project.EntenEller.Base.Scripts.Advanced.Files
{
    public static class EECSV
    {
        public static List<List<string>> Read(string path)
        {
            try
            {
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var streamReader = new StreamReader(fileStream);
                var csv = new CsvReader(streamReader);
                var list = new List<List<string>>();
                while (csv.Read()) 
                {
                    var subList = new List<string>();
                    for (var i = 0; i < csv.FieldsCount; i++) 
                    {
                        subList.Add(csv[i]);
                    }
                    list.Add(subList);
                }
                return list;
            }
            catch(Exception ex)
            {
                EEDebug.Log(ex, EEDebug.LogType.Warning);
            }
            return null;
        }
    }
}
