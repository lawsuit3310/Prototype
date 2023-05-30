using UnityEngine;

namespace Data
{
    public class AlcoholScheduler
    {
        private readonly string _environmentPath = Application.dataPath + "Scripts/Data/AlcoholSchedule.json";

        public AlcoholScheduler()
        {
            Debug.Log(_environmentPath);
        }
    }
}