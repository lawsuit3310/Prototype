using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

public class PlayerDataHandler
{
    private readonly string _environmentPath = Application.dataPath + "/Scripts/Data/PlayerData.json";
    //총괄 데이터 - UUID나 게임 플레이 데이터 등
    private static JObject _playerData;
    private FileStream _fileStream;
    public PlayerDataHandler()
    {
        #region 세이브 파일이 존재하는지 확인한 후에 없으면 파일 생성
        //Debug.Log(_environmentPath);

        if (File.Exists(_environmentPath))
        {
            _playerData = JObject.Parse(
                File.ReadAllText(_environmentPath));
        }

        else
        {
            _fileStream = File.Create(_environmentPath);
            _fileStream.Close();

            if (!InitSaveData())
            {
                Debug.LogError("Save File initialize failed");
            }
            
            File.WriteAllText(_environmentPath, _playerData.ToString(), encoding: Encoding.UTF8);
        }
        #endregion

        
    }
    
    private bool InitSaveData()
    {
        var result = true;
        try
        {
            _playerData = new JObject
            {
                ["UUID"] = SystemInfo.deviceUniqueIdentifier,
                ["PlayerData"] = new JObject()
                {
                    ["Name"] = "John",
                    ["ATK"] = 5,
                    ["HP"] = 100,
                    ["Money"] = 0,
                    ["CriticalRate"] = 30,
                    ["Date"] = 0
                },
                ["Inventory"] = new JObject()
                {
                    //VSurvLikeData.json의 아이템 인덱스 : 아이템의 보유 갯수
                    ["0"] = 0,
                    ["1"] = 0,
                    ["2"] = 0
                },
                ["Upgrades"] = new JObject()
                {
                    ["ATK"] = 0,
                    ["HP"] = 0,
                    ["CriticalRate"] = 0
                },
                ["Alcohols"] = new JObject()
                {
                    ["0"] = 0,
                    ["1"] = 0,
                    ["2"] = 0
                }
            };
        }
        catch (Exception e)
        {
            result = false;
        }
        return result;
    }
    public bool WriteSaveData()
    {
        var result = true;
        try
        {
            
            //json으로 변환 불가능한 텍스트가 오면 에러 던짐
            string _context = JObject.Parse(_playerData.ToString()).ToString();
            File.WriteAllText(_environmentPath, _context, encoding: Encoding.UTF8);
        }
        catch (Exception e)
        {
            result = false;
        }
        return result;
    }
    public bool WriteSaveData(string _jObject)
    {
        var result = true;

        if (_jObject == "*") return WriteSaveData(_playerData);
        try
        {
            File.WriteAllText(_environmentPath, _jObject, encoding: Encoding.UTF8);
        }
        catch (Exception e)
        {
            result = false;
        }
        return result;
    }
    public bool WriteSaveData(string key,string context)
    {
        var result = true;
        try
        {
            //json으로 변환 불가능한 텍스트가 오면 에러 던짐
            _playerData[key] = JObject.Parse(context);
            File.WriteAllText(_environmentPath, _playerData.ToString(), encoding: Encoding.UTF8);
        }
        catch (Exception e)
        {
            result = false;
        }
        return result;
    }
    public bool WriteSaveData(JObject jObject)
    {
        return WriteSaveData(jObject.ToString());
    }

    public bool AddItemToJObject(string key, JToken value)
    {
        var result = true;
        try
        {
            _playerData[key] = value;
        }
        catch (Exception e)
        {
            result = false;
        }
        return result;
    }
    public JObject GetJObject()
    {
        return _playerData;
    }
}
