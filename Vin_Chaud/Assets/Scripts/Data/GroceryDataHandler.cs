using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroceryDataHandler
{
    private readonly static string _environmentPath = Application.dataPath + "/Scripts/Data/VSurvLikeData.json";
    private static JArray _gatherableItems;
    private static JArray _alcoholDictionary;
    private static Dictionary<int, string> GroceryDictionary;


    public GroceryDataHandler()
    {
        GroceryDictionary = new Dictionary<int, string>();
        _gatherableItems =
            (JArray)JObject.Parse(File.ReadAllText(_environmentPath))["GatherableItems"];
        _alcoholDictionary =
            (JArray)JObject.Parse(File.ReadAllText(_environmentPath))["Alcohol"];
        
        foreach (var node in _gatherableItems)
        {
            GroceryDictionary.Add(
                Convert.ToInt32(node["ID"]), node["NAME"].ToString());
        }
    }

    public Dictionary<int, string> GetGroceryDictionary()
    {
        return GroceryDictionary;
    }

    public static JArray GetAlcoholDictionary()
    {
        if (_alcoholDictionary == null)
            _alcoholDictionary = 
                (JArray)JObject.Parse(File.ReadAllText(_environmentPath))["Alcohol"];
        return _alcoholDictionary;
    }
}
