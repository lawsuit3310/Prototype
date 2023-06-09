using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    public GameObject Customer;
    public GameObject CustomerText;
    public GameObject GroceryUI;
    public static Stack<GameObject> CustomerInstance;
    public bool isCustomerSpawnable = true;

    public float frequency = 1;
    public float SpawnChance = 2;

    private void Awake()
    {
        CustomerInstance = new Stack<GameObject>();
        CustomerBehavior.CustomerText = CustomerText;
        CustomerBehavior.GroceryUI = GroceryUI.GetComponent<GroceryUI>();
        StartCoroutine(SpawnCustomer());
    }
    IEnumerator SpawnCustomer()
    {
        while (true)
        {
            if (!isCustomerSpawnable)
            {
                yield return null;
                continue;
            }

            //2%
            if (Random.Range(0, 1000) < SpawnChance * 10)
            {
                CustomerInstance.Push(Instantiate(Customer));
                CustomerInstance.Peek().transform.parent = this.transform;
            }
            
            yield return new WaitForSeconds(frequency);
        }
    }
}