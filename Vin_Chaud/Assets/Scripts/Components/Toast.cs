using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    // Start is called before the first frame update
    private static Image _background; 
    private static TMP_Text _msg; 
    void OnEnable()
    {
        _background = GetComponentInChildren<Image>();
        _msg = GetComponentInChildren<TMP_Text>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        var defaultAlpha = _background.color.a;

        yield return new WaitForSeconds(1);
        
        for (float alpha = 1; alpha > 0; alpha -= 0.1f)
        {
            _background.color = new Color()
            {
                r = _background.color.r,
                g = _background.color.g,
                b = _background.color.b,
                a = _background.color.a - 0.1f
            };
            _msg.color = new Color()
            {
                r = _msg.color.r,
                g = _msg.color.g,
                b = _msg.color.b,
                a = _msg.color.a - 0.1f
            };
            yield return new WaitForSeconds(0.1f);
        }
        _background.color = new Color()
        {
            r = _background.color.r,
            g = _background.color.g,
            b = _background.color.b,
            a = defaultAlpha
        };
        _msg.color = new Color()
        {
            r = _msg.color.r,
            g = _msg.color.g,
            b = _msg.color.b,
            a = 1
        };
        this.gameObject.SetActive(false);
    }
}
