using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Assets.Script.entity;
using Assets.Script.globalVar;

namespace Assets.Script.state
{


    public class CreditState : GameState
    {
        private GameManager manager;
        public string stateName;

        public GlobalData globalVar;

        private GameObject homeBtn;

        public CreditState(GameManager managerRef, GlobalData globalRef)
        {
            manager = managerRef;
            globalVar = globalRef;
            stateName = "creditState";
        }

        public void getData()
        {
            homeBtn = GameObject.Find("Home");
            homeBtn.GetComponent<Button>().onClick.AddListener(this.goTomenu);
        }

        void goTomenu()
        {
           SceneManager.LoadScene("Loader");
           manager.switchState(new LoaderState(manager, globalVar, "Menu"));
        }
    
        public void stateUpdate()
        {
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
