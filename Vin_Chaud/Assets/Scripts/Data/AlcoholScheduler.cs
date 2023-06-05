using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using static System.DateTime;


public class AlcoholScheduler
{
    private readonly static string EnvironmentPath = Application.dataPath + "/Scripts/Data/AlcoholSchedule.json";
    private static JObject _schedule;

    public AlcoholScheduler()
    {
        _schedule = JObject.Parse(
            File.ReadAllText(EnvironmentPath));
    }

    public static void ScheduleCheck()
    {
        foreach (var row in _schedule)
        {
            //지났으면 -1 안지났으면 1
            try
            {
                switch (Compare(Parse(row.Value["EndTime"]?.ToString()), UtcNow))
                {
                    case -1:
                        _schedule[row.Key]["IS_DONE"] = true;
                        _schedule[row.Key]["IS_PROCESSING"] = false;
                        break;
                    default:
                        _schedule[row.Key]["IS_DONE"] = false;
                        break;
                }
            }
            //parsing에 실패한 상황이므로, 스케쥴 편성이 되지 않은 상황
            catch (Exception e)
            {
                _schedule[row.Key]["IS_DONE"] = false;
            }
            

            WriteSaveData("*");
        }
    }

    #region 스케쥴 관리용 메소드들
    public static bool AddSchedule(string breweryID, int index)
    {
        //아이템 갯수가 0개 일경우 메소드 바로 종료
        if (!GameManager.DecreaseItemAmount(Convert.ToInt32(index)))
        {
            return false;
        }
        if (_schedule[breweryID]["EndTime"].ToString() != "")
            return false;
        _schedule[breweryID]["EndTime"] =
            UtcNow.AddSeconds(
                Convert.ToDouble(GroceryDataHandler.GetAlcoholDictionary()[index]["SPEND_TIME"]));
        _schedule[breweryID]["Type"] = index;
        _schedule[breweryID]["IS_PROCESSING"] = true;
        WriteSaveData("*");
        return true;
    }
    public static bool AddSchedule(int breweryID, int index)
    {
        return AddSchedule(breweryID + "" , index);
    }

    public static bool IsScheduleDone(int breweryID)
    {
        return IsScheduleDone(breweryID + "");
    }
    public static bool IsScheduleDone(string breweryID)
    {
        return _schedule[breweryID]?["IS_DONE"]?.ToString() == "True";
    }
    public static bool IsScheduleProcessing(int breweryID)
    {
        return IsScheduleProcessing(breweryID + "");
    }
    public static bool IsScheduleProcessing(string breweryID)
    {
        return _schedule[breweryID]?["IS_PROCESSING"]?.ToString() == "True";
    }

    public static void EndSchedule(string breweryID)
    {
        _schedule[breweryID]["EndTime"] = "";
        _schedule[breweryID]["IS_DONE"] = false;
        _schedule[breweryID]["IS_PROCESSING"] = false;
        GameManager.IncreaseAlcoholAmount(Convert.ToInt32(breweryID));
        WriteSaveData("*");
    }
    public static void EndSchedule(int breweryID)
    {
        EndSchedule(breweryID + "");
    }

    public static TimeSpan GetRemainTime(string index)
    {
        return Parse(_schedule[index]["EndTime"].ToString()) - UtcNow;
    }
    #endregion

    public static bool WriteSaveData(string _jObject)
    {
        var result = true;

        if (_jObject == "*") return WriteSaveData(_schedule);
        try
        {
            File.WriteAllText(EnvironmentPath, _jObject, encoding: Encoding.UTF8);
        }
        catch (Exception e)
        {
            result = false;
        }
        return result;
    }
    public static bool WriteSaveData(JObject jObject)
    {
        return WriteSaveData(jObject.ToString());
    }
}
