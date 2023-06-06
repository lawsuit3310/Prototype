using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GroceryUI : MonoBehaviour
{
    public GameObject storePanel;
    public TMP_Text moneyText;

    private void Awake()
    {
        RefreshUI();
    }

    public static void UpdateGroceryUI(TMP_Text[] groceryAmountText, GroceryDataHandler _groceryDataHandler)
    {
        var inventory = GameManager.GetInventory();
        var groceryDictionary = _groceryDataHandler.GetGroceryDictionary();
        for (int i = 0; i < groceryAmountText.Length; i++)
        {
            groceryAmountText[i].text =
                $"{groceryDictionary[i]} : {inventory[i]}";
        }
    }

    public void ShowStorePanel()
    {
        storePanel.SetActive(true);
    }

    public void CloseStorePanel()
    {
        Time.timeScale = 1;
        storePanel.SetActive(false);
    }

    private void RefreshUI()
    {
        //업그레이드 메소드가 호출 되었을 때 기본적으로 실행할 내용
        moneyText.text = GameManager.GetStatus(StatusKeys.Money) + "$";
    }
    public void UpgradeHP()
    {
        RefreshUI();
        if (Convert.ToInt32(GameManager.GetStatus(StatusKeys.Money)) -
            GameManager.CalcUpgradeCost(
                Convert.ToInt32(GameManager.GetUpgrades(StatusKeys.HP)) + 1) >= 0)
        {
            GameManager.RenewalStatus(StatusKeys.HP);
            GameManager.RenewalStatus(
                StatusKeys.Money, Convert.ToInt32(
                    GameManager.CalcUpgradeCost(GameManager.GetUpgrades(StatusKeys.HP))));
        }
        else
        {
            GameManager.ShowToastMessage($"잔액이 부족합니다");
            CloseStorePanel();
        }
    }
    public void UpgradeATK()
    {
        
        RefreshUI();
        if (Convert.ToInt32(GameManager.GetStatus(StatusKeys.Money)) -
          GameManager.CalcUpgradeCost(
              Convert.ToInt32(GameManager.GetUpgrades(StatusKeys.ATK)) + 1) >= 0)
        {
            GameManager.RenewalStatus(StatusKeys.ATK);
            GameManager.RenewalStatus(
                StatusKeys.Money, Convert.ToInt32(
                    GameManager.CalcUpgradeCost(GameManager.GetUpgrades(StatusKeys.ATK))));
        }
        else
        {
            GameManager.ShowToastMessage($"잔액이 부족합니다");
            CloseStorePanel();
        }
    }

    public void UpgradeCriticalRate()
    {
        RefreshUI();
        if (Convert.ToInt32(GameManager.GetStatus(StatusKeys.Money)) -
            GameManager.CalcUpgradeCost(
                Convert.ToInt32(GameManager.GetUpgrades(StatusKeys.CriticalRate)) + 1) >= 0)
        {
            GameManager.RenewalStatus(StatusKeys.CriticalRate);
            GameManager.RenewalStatus(
                StatusKeys.Money, Convert.ToInt32(
                    GameManager.CalcUpgradeCost(GameManager.GetUpgrades(StatusKeys.CriticalRate))));
        }
        else
        {
            GameManager.ShowToastMessage($"잔액이 부족합니다");
            CloseStorePanel();
        }
    }

    public void UpgradeALL() { GameManager.RenewalStatus("*");}


}
