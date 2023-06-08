using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
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
        op = SceneManager.LoadSceneAsync(TargetSceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
        }
        op.allowSceneActivation = true;
    }
}