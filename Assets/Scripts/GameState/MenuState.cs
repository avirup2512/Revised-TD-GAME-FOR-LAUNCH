using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Script.entity;
using Assets.Script.interfaces;
using Assets.Script.globalVar;
using TMPro;


namespace Assets.Script.state
{
    public class MenuState : GameState
    {
        private GameManager manager;
        private GameObject buttonObject;
        private Button playButton;
        private GameObject buttonObject2;
        private Button shopButton;

        private GameObject UpdateBtn;

        public GameObject soundBtn;
        public GameObject muteBtn;
        public GameObject SoundOn;

        private GameObject exitButtonObject;
        private Button exitButton;
        public GameObject RateUs;

        private GameObject creditsButtonObject;
        private Button creditsButton;

        public GlobalData globalVar;

        private GameObject attackButtonObject;
        private Button attackButton;

        public string stateName;

        // Modal Variable
        private GameObject modal;
        private GameObject modalYesButtonObject;
        private Button modalYesButton;
        private GameObject modalNoButtonObject;
        private Button modalNoButton;


        public MenuState(GameManager managerRef, GlobalData globalRef)
        {
            manager = managerRef;
            globalVar = globalRef;
            stateName = "menuState";

        }
        void credits()
        {
            SceneManager.LoadScene("Loader");
            manager.switchState(new LoaderState(manager, globalVar, "credits"));
        }
        void play()
        {
            this.switchState();
        }
        void exit()
        {
            Application.Quit();
        }

        void attack()
        {
            manager.switchState(new LoaderState(manager, globalVar, "PlayState"));
        }

        public void getData()
        {

            buttonObject = GameObject.Find("Play");
            playButton = buttonObject.GetComponent<Button>();
            playButton.onClick.AddListener(this.play);

            // buttonObject2 = GameObject.Find("Shop");
            // shopButton = buttonObject2.GetComponent<Button>();
            // shopButton.onClick.AddListener(this.goToSceneShop);

            // creditsButtonObject = GameObject.Find("Credits");
            // creditsButton = creditsButtonObject.GetComponent<Button>();
            // creditsButton.onClick.AddListener(this.goToCredit);

            // modal = FindInActiveObjectByName("Exit Canvas");
            // modalYesButtonObject = FindInActiveObjectByName("Yes Button");
            // modalYesButton = modalYesButtonObject.GetComponent<Button>();
            // modalNoButtonObject = FindInActiveObjectByName("No Button");
            // modalNoButton = modalNoButtonObject.GetComponent<Button>();


            // modalYesButton.onClick.AddListener(this.exit);
            // modalNoButton.onClick.AddListener(this.back);


            // exitButtonObject = GameObject.Find("Exit");
            // exitButton = exitButtonObject.GetComponent<Button>();
            // exitButton.onClick.AddListener(this.openExitModal);


            // creditsButtonObject = GameObject.Find("credits");
            // creditsButton = creditsButtonObject.GetComponent<Button>();
            // creditsButton.onClick.AddListener(this.credits);

        }
        public void goToSceneShop()
        {
            SceneManager.LoadScene("Loader");
            manager.switchState(new LoaderState(manager, globalVar, "Shop"));

        }
        public void goToGooglePLay()
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.GameGambu.MonstersAttack");
        }
        public void goToCredit()
        {
            SceneManager.LoadScene("Loader");
            manager.switchState(new LoaderState(manager, globalVar, "Credits"));

        }

        void unmute()
        {
            AudioListener.volume = 1;
            globalVar.isMute = false;
            muteBtn.SetActive(false);
            soundBtn.SetActive(true);
            SoundOn.GetComponent<TextMeshProUGUI>().text = "sound on";
        }
        void mute()
        {
            AudioListener.volume = 0;
            globalVar.isMute = true;
            muteBtn.SetActive(true);
            soundBtn.SetActive(false);
            SoundOn.GetComponent<TextMeshProUGUI>().text = "sound off";
        }

        void back()
        {
            modal.SetActive(false);
        }

        void openExitModal()
        {
            modal.SetActive(true);
        }

        GameObject FindInActiveObjectByName(string name)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].name == name)
                    {
                        return objs[i].gameObject;
                    }
                }
            }
            return null;
        }
        public void stateUpdate()
        {

        }
        public void switchState()
        {
            SceneManager.LoadScene("Loader");
            manager.switchState(new LoaderState(manager, globalVar, "RoundSelection"));

        }
        public string getStateName()
        {
            return this.stateName;
        }
        public void action(string action)
        {

        }
    }
}
