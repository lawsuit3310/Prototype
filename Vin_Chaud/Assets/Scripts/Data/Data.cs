using System;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Data
{
    //https://devstarsj.github.io/development/2016/06/11/CSharp.NewtonJSON/ newtonJson - 사용법
    //https://infodbbase.tistory.com/113 System.IO - 사용법

    protected static JObject data;
    
    public static Boolean ParsingData()
    {
        var result = true;
        try
        { 
            data = JObject.Parse(
                File.ReadAllText("..\\Vin_Chaud\\Assets\\Scripts\\Data\\VSurvLikeData.json")
                //C:\Users\Public\Documents\GitHub\Prototype\Vin_Chaud\Assets\Scripts\Data\VSurvLikeData.json
            );
            foreach (var VARIABLE in data["gatherableItems"] )
            {
                Debug.Log(VARIABLE["name"]);
            }
        }

        catch (Exception e)
        {
            Debug.Log(e.Message);
            result = false;
        }

        return result;
    }
    
}
