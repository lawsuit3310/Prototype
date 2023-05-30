using Data;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    //[SerializeField]
    //private SceneController sc;
    private static StackTrace st;
    private static AlcoholScheduler _alcoholScheduler;
    private static PlayerDataHandler _playerDataHandler;
    [CanBeNull] private static Dictionary<int, int> _inventory;
    [CanBeNull] private static Dictionary<string, int> _upgrades;

    private void Awake()
    {
        //만약 이미 게임매니저 오브젝트가 존재하고 있을 경우 자신을 파괴
        GameObject gm = GameObject.Find("GameManager");

        if (gm != null && gm != this.gameObject)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
        _playerDataHandler = new PlayerDataHandler();
        _alcoholScheduler = new AlcoholScheduler();

        _inventory = GetDataDictionary<int, int>("Inventory");
        _upgrades = GetDataDictionary<string, int>("Upgrades");
        
        //JsonConvert.DeserializeObject<Dictionary<string, string>>(_PlayerDataHandler.GetJObject().ToString());
        
        ReplaceData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //데이터를 딕셔너리 타입으로 가져오는 메소드
    public static Dictionary<T1, T2> GetDataDictionary<T1, T2>(string key)
    {
        Dictionary<T1, T2> result = null;
        try
        {
            result = JsonConvert.DeserializeObject<Dictionary<T1, T2>>(
            _playerDataHandler.GetJObject()[key].ToString());
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
        return result;
    }

    public static Dictionary<int, int> GetInventory()
    {
        return _inventory;
    }
    //딕셔너리에 데이터를 추가하는 메소드
    public static bool AddDataToInventory(int key, int value)
    {
        var result = true;
        try
        {
            _inventory.Add(key, value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = false;
        }
        return result;
    }
    //인벤토리 아이템 갯수 증가
    public static bool IncreaseItemAmount(int key, int amount)
    {
        var result = true;
        try
        {
            _inventory[key] += amount;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = false;
        }
        return result;
    }
    //인벤토리 아이템 갯수 감소 단, 0개 미만이 되면 실패(false)
    public static bool DecreaseItemAmount(int key, int amount)
    {
        var result = true;
        try
        {
            if (_inventory[key] - amount < 0)
            {
                throw new Exception("Uncorrect Amount");
            }

            _inventory[key] -= amount;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = false;
        }
        return result;
    }
    //인벤토리 초기화
    public static bool ClearInventory()
    {
        var result = true;
        try
        {
            _inventory.Clear();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = false;
        }
        return result;
    }

    public static Dictionary<string, int> GetUpgradeStatus()
    {
        return _upgrades;
    }

    //this method for rewrite
    
    //매개변수를 주지 않으면 기본적으로 인벤토리 저장
    public static void ReplaceData()
    {
        //PlayerDataHandler.GetJObject()["PlayerData"]["Inventory"] = JsonConvert.SerializeObject(Inventory);
        if (!_playerDataHandler.WriteSaveData("Inventory", TextImporter.DictionaryToString(_inventory)))
        {
            st = st == null ? new StackTrace(new StackFrame(true)) : st;
            Debug.Log($"SaveWasFailed : GameManager : {st.GetFrame(0).GetFileLineNumber()}");
        }
    }
    public static void ReplaceData<T1, T2>(string key, Dictionary<T1, T2> dict)
    {
        if (!_playerDataHandler.WriteSaveData(key, TextImporter.DictionaryToString(dict)))
        {
            st = st == null ? new StackTrace(new StackFrame(true)) : st;
            Debug.Log($"SaveWasFailed : GameManager : {st.GetFrame(0).GetFileLineNumber()}");
        }
    }
    
    public static void RenewalStatus(char _key)
    {
        RenewalStatus(_key.ToString());
    }
    public static void RenewalStatus(string _key)
    {
        
        var data = _playerDataHandler.GetJObject();
        //아이템 등을 강화 시켜 스테이스가 변화할 경우 그거 적용 시키는 용도
        //업그레이드 횟수와 능력치 둘 다 갱신
        if (_key == "*")
        {
            Debug.Log(_upgrades.Keys.Count);
            //renewal all
            foreach (var keyValueFair in _upgrades)
            {
                
                switch (keyValueFair.Key)
                {
                    case StatusKeys.ATK :
                        data[StatusKeys.PlayerData][keyValueFair.Key] =
                            Convert.ToInt32(data[StatusKeys.Upgrades][keyValueFair.Key]) + 5;
                        break;
                    case StatusKeys.HP :
                        data[StatusKeys.PlayerData][keyValueFair.Key] =
                            Convert.ToInt32(data[StatusKeys.Upgrades][keyValueFair.Key]) * 10 + 100;
                        break;
                    case StatusKeys.CriticalRate :
                        data[StatusKeys.PlayerData][keyValueFair.Key] = 
                            Convert.ToInt32(data[StatusKeys.Upgrades][keyValueFair.Key]) * 10 + 30;
                        break;
                }

                data[StatusKeys.Upgrades][keyValueFair.Key] = Convert.ToInt32(data[StatusKeys.Upgrades][keyValueFair.Key]) + 1;
            }
        }
        else
        { 
            switch (_key)
            {
                case StatusKeys.ATK :
                    data[StatusKeys.PlayerData][_key] =
                        Convert.ToInt32(data[StatusKeys.Upgrades][_key]) + 5;
                    break;
                case StatusKeys.HP :
                    data[StatusKeys.PlayerData][_key] =
                        Convert.ToInt32(data[StatusKeys.Upgrades][_key]) * 10 + 100;
                    break;
                case StatusKeys.CriticalRate :
                    data[StatusKeys.PlayerData][_key] = 
                        Convert.ToInt32(data[StatusKeys.Upgrades][_key]) * 10 + 30;
                    break;
            }
            data[StatusKeys.Upgrades]![_key] = Convert.ToInt32(data[StatusKeys.Upgrades][_key]) + 1;
           
        }
        
        if(!_playerDataHandler.WriteSaveData(data))
            Debug.Log("failed");
    }
    
}