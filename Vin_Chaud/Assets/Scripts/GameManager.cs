using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace VinChaud
{
    public class GameManager : MonoBehaviour
    {
        //[SerializeField]
        //private SceneController sc;

        void Awake()
        {
            GameObject gm = GameObject.Find("GameManager");
            if (gm != null && gm != this.gameObject)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += AddListenerToSceneLoader;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadScene(string sceneName)
        {
            try
            {
                //sc = GameObject.Find("SceneController").GetComponent<SceneController>();
                SceneManager.LoadScene(sceneName);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }            
        }
        public void LoadScene(int idx)
        {
            SceneManager.LoadScene(idx);
        }

        //It Calls When Any Scene is Loaded
        void AddListenerToSceneLoader(Scene scene, LoadSceneMode mode)
        {
            GameObject.FindGameObjectWithTag("SCENEMOVE").GetComponent<Button>().onClick.AddListener( () => {
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 0:
                        Debug.Log("title scene activated");
                        LoadScene(1);
                        break;
                    case 1:
                        Debug.Log("game scene activated");

                        LoadScene(0);
                        break;
                }
            });
        }

    }
}