using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrewryManager : MonoBehaviour
{
    [SerializeField] private GameObject breweryUI;
    [SerializeField] private GameObject progressUI;
    
    private int currentBreweryID = 0;
    
    // Start is called before the first frame update
    private void Awake()
    {
        new AlcoholScheduler();
    }

    private void Start()
    {
        AlcoholScheduler.ScheduleCheck();
    }

    private void Update()
    {
        AlcoholScheduler.ScheduleCheck();
    }

    public void StartBrewery(int index)
    {
        if (!AlcoholScheduler.AddSchedule(currentBreweryID, index))
        {
            GameManager.ShowToastMessage("재료가 부족합니다!");
        }
        CloseBreweryUI();
    }

    public void ShowBreweryUI(int breweryID)
    {
        currentBreweryID = breweryID;
        breweryUI.SetActive(true^false);
    }

    public void ShowProgressUI(Brewery caller)
    {
        TMP_Text t;
        try
        {
            t = caller.GetComponentInChildren<TMP_Text>();
            t.transform.parent.localPosition = new Vector3(0, 0, 0.2f);
            t.text = AlcoholScheduler.IsScheduleProcessing(caller.getBreweryID()) ?
                parseTimeSpan(AlcoholScheduler.GetRemainTime(caller.getBreweryID() + "")) :
                "Done";
        }
        catch (Exception e)
        {
            var v = Instantiate(progressUI);
            v.transform.SetParent(caller.transform);
            t = caller.GetComponentInChildren<TMP_Text>();
        }
    }
    public void CloseBreweryUI()
    {
        breweryUI.SetActive(1 == 0);
    }

    //timespan 타입을 00시 00분 00초 형식으로 변환
    public static string parseTimeSpan(TimeSpan span)
    {
        string result = "";

        result += span.Days == 0 ? "" : span.Days + "d ";
        result += span.Hours == 0 ? "" : span.Hours + "h ";
        result += span.Minutes == 0 ? "" : span.Minutes + "m ";
        result += span.Seconds == 0 ? "" : span.Seconds + "s";
        
        return result;
    }
}
