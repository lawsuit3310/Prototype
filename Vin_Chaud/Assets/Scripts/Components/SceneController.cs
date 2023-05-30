using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VinChaud
{
    public class SceneController : MonoBehaviour
    {
        static string _sceneName;

        #region thread로 시도하다 실패 www
        //public static void LoadScene(string sceneName)
        //{
        //    Thread LoadingProgress = new Thread(() =>{

        //        op = op == null ? SceneManager.LoadSceneAsync(sceneName) : op;

        //        while (op.isDone)
        //        {
        //            Debug.Log(op.progress);
        //            Thread.Sleep(1000);
        //        }
        //    });

        //    LoadingProgress.Start();
        //}

        //LoadSceneAsync 는 메인 스레드에서만 사용 가능
        #endregion

        private void Awake()
        {
            GameObject gm = GameObject.Find("SceneController");
            if (gm != null && gm != this.gameObject)
            {
                Destroy(this);
            }
            DontDestroyOnLoad(this);
        }




        public void LoadScene(string sceneName)
        {
            _sceneName = sceneName;
            SceneManager.LoadScene("LoadingScene");
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(_sceneName);

            op.allowSceneActivation = false;

            yield return null;

            //while (!op.isDone)
            //{
            //    Debug.Log("Process Started");
            //    yield return null;
            //    Debug.Log("wait 1 seconds");
            //    yield return new WaitForSeconds(1);
            //    Debug.Log("process is done");
            //    yield break;
            //}
        }


    }
}