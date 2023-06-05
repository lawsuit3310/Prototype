using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainTimeUI : MonoBehaviour
{
    [SerializeField] private Brewery brewery = null; 
    // Start is called before the first frame update

    private void Start()
    {
        brewery = transform.parent.GetComponent<Brewery>();
    }

    // Update is called once per frame
    void Update()
    {
        TaskChecker();
    }

    private void TaskChecker()
    {
        if (!AlcoholScheduler.IsScheduleProcessing(brewery.getBreweryID()) &&
            !AlcoholScheduler.IsScheduleDone(brewery.getBreweryID()))
        {
            Destroy(this.gameObject);
        }
    }
}
