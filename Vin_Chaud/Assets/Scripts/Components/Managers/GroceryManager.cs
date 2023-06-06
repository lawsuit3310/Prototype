using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroceryManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static GroceryDataHandler _groceryDataHandler;
    public TMP_Text[] groceryAmountText;
    void Awake()
    {
        _groceryDataHandler = new GroceryDataHandler();
    }
    void Start()
    {
        GroceryUI.UpdateGroceryUI(groceryAmountText);
    }
    
}
