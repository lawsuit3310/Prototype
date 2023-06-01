using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConverseManager : MonoBehaviour
{
    [SerializeField] private GameObject converseUI;
    [SerializeField] private Image portrait;
    [SerializeField] private TMP_Text textUI; 
    [SerializeField] private TMP_Text name;

    public bool isShowStorePanelEnd = false;
    
    public void showTextPanel(string _name, string text)
    {
        converseUI.SetActive(true);
        textUI.text = text;
        name.text = _name;
        portrait.gameObject.SetActive(false);
    }

    public void showTextPanel(int index)
    {
        converseUI.SetActive(true);
        name.text = "Debug Status";
        textUI.text = "Debug Status";
        portrait.gameObject.SetActive(true);
    }

    public void disableTextPanel()
    {
        converseUI.SetActive(false);
        if (isShowStorePanelEnd)
        {
            WizardBehavior.StoreUI.ShowStorePanel();
            isShowStorePanelEnd = false;
        }
            
    }

}