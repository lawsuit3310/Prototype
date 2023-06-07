using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static AsyncOperation op;
    public static string CurrentSceneName = "";
    public static string TargetSceneName;


    private void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    static IEnumerator LoadSceneProgress()
    {
        SceneManager.LoadScene(SceneName.LoadingScene);
        op = SceneManager.LoadSceneAsync(TargetSceneName);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                Debug.Log(op.progress);
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                Debug.Log(timer);
                yield return new WaitForSeconds(5f);
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}