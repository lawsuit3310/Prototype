using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroceryDataHandler
{
    private readonly string _environmentPath = Application.dataPath + "/Scripts/Data/VSurvLikeData.json";
    private static JObject _gatherableItems;
    private FileStream _fileStream;
    private static Dictionary<int, string> GroceryDictionary;

    public GroceryDataHandler()
    {
        GroceryDictionary = new Dictionary<int, string>();
        _gatherableItems = JObject.Parse(File.ReadAllText(_environmentPath));
        //Debug.Log(_gatherableItems["GatherableItems"][0]);

        foreach (var node in _gatherableItems["GatherableItems"])
        {
            GroceryDictionary.Add(
                Convert.ToInt32(node["ID"]), node["NAME"].ToString());
        }
    }

    public Dictionary<int, string> GetGroceryDictionary()
    {
        return GroceryDictionary;
    }
}
