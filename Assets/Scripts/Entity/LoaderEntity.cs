using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using Assets.Script.globalVar;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Script.state;

namespace Assets.Script.entity

{
    public class LoaderEntity : MonoBehaviour
    {
        private GameObject barObject;
        private Slider bar;
        public string sceneName;

        public GlobalData globalVar;
        public GameManager manager;
        // Start is called before the first frame update
        void Start()
        {
            barObject = GameObject.Find("LoaderBar");
            bar = barObject.GetComponent<Slider>();
        }

        public void LoadButton()
        {
            StopCoroutine("LoadScene");
            //Start loading the Scene asynchronously and output the progress bar
            StartCoroutine("LoadScene");
        }

        public IEnumerator LoadScene()
        {
            //yield return new WaitForSeconds(1);
            yield return null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                float progressValue = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                //Output the current progress
                bar.value = asyncOperation.progress;
                Debug.Log(asyncOperation.progress);
                // Check if the load has finished
                if (asyncOperation.progress >= 0.9f)
                {
                    if (sceneName == "Play")
                    {
                        asyncOperation.allowSceneActivation = true;
                        manager.switchState(new PlayState(manager, globalVar));
                        StopCoroutine("LoadScene");

                    }
                    else if (sceneName == "RoundSelection")
                    {
                        asyncOperation.allowSceneActivation = true;
                        manager.switchState(new SceneSelectionState(manager, globalVar));
                        StopCoroutine("LoadScene");
                    }
                    else if (sceneName == "Menu")
                    {
                        asyncOperation.allowSceneActivation = true;
                        manager.switchState(new MenuState(manager, globalVar));
                        StopCoroutine("LoadScene");
                    }
                    else if (sceneName == "Shop")
                    {
                        asyncOperation.allowSceneActivation = true;
                        manager.switchState(new ShopState(manager, globalVar));
                        StopCoroutine("LoadScene");
                    }
                    else if (sceneName == "Credits")
                    {
                        asyncOperation.allowSceneActivation = true;
                        manager.switchState(new CreditState(manager, globalVar));
                        StopCoroutine("LoadScene");
                    }
                    yield return null;
                }
            }
        }
        public void stopFire()
        {
        }
        public void fire(Vector2 vec)
        {
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}

