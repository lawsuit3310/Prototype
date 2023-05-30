using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GroceryUI : MonoBehaviour
{
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

    public void CloseStorePanel()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }

    public void UpgradeHP() { GameManager.RenewalStatus(StatusKeys.HP);}
    public void UpgradeATK() { GameManager.RenewalStatus(StatusKeys.ATK); }
    public void UpgradeCriticalRate() { GameManager.RenewalStatus(StatusKeys.CriticalRate);}
    public void UpgradeALL() { GameManager.RenewalStatus("*");}
}
