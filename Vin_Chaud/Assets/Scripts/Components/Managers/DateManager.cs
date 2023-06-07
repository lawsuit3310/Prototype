using Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DateManager : MonoBehaviour
{
    private static string[] _adventureVoice =
    {
        "じゃあ行っくよー？！　せーぇのっ！" , "みんなー！ 準備はいい？"
    };
    private static string[] _calculationVoice=
    {
        "ねぇ、お仕事は〜？" , "今日も一日お疲れ様でした！"
    };
    private ConverseManager _converseManager;
    [FormerlySerializedAs("converseManager")] [SerializeField] private Converse converse;
    [SerializeField] private TMP_Text DateText;
    // Start is called before the first frame update

    private void Awake()
    {
        _converseManager = new ConverseManager("Converse");
    }

    private void Start()
    {
        if (LoadingSceneManager.CurrentSceneName == SceneName.Title || LoadingSceneManager.CurrentSceneName == "")
        {
            AudioManager.PlaySound(
                Resources.Load($"Sound/Voice/いらっしゃいませ{Random.Range(1,5)}") as AudioClip);
            _converseManager.Converse(0,1);
            
        }
        else if (LoadingSceneManager.CurrentSceneName == SceneName.Adventure)
        {
            IncreaseDate();
            int idx = Random.Range(0, _calculationVoice.Length);
            AudioManager.PlaySound(
                Resources.Load(
                    $"Sound/Voice/{_calculationVoice[idx]}") as AudioClip);
            _converseManager.Converse(1 + idx,1);
        }
        DateText.text = GameManager.GetStatus(StatusKeys.Date) + " 일차";
    }
    public static void IncreaseDate()
    {
        GameManager.IncreaseDate();
    }

    public static void StartAdventure()
    {
        AudioManager.PlaySound(
            Resources.Load(
                $"Sound/Voice/{_adventureVoice[Random.Range(0,_adventureVoice.Length)]}") as AudioClip);
        GameManager.LoadScene("Adventure");
    }
}
