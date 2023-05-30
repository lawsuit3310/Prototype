using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerData : MonoBehaviour
{
    private static int atk = 0;
    private static int hp = 0;
    public static Dictionary<string, string> playerData;
    public static Dictionary<string, int> upgrades;
    
    private PlayerDataHandler _playerDataHandler;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    public static int GetATK()
    {
        if (atk == 0) SetUp();
        return atk;
    }
    public static int GetMaxHP()
    {
        if (hp == 0) SetUp();
        return hp;
    }

    private static void SetUp()
    {
        playerData = GameManager.GetDataDictionary<string, string>("PlayerData");
        upgrades = GameManager.GetUpgradeStatus();

        atk = Convert.ToInt32(playerData["ATK"]);
        hp = Convert.ToInt32(playerData["HP"]);
    }
}
