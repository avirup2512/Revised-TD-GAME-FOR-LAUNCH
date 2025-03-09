using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Script.entity;
using Assets.Script.globalVar;

namespace Assets.Script.state
{


    public class LoaderState : GameState
    {
        private GameManager manager;
        public string stateName;
        public LoaderEntity entity;
        public GameState nextstate;
        public string scene;

        private bool goNextState = false;

        GameObject entityObject;

        public GlobalData globalVar;


        public LoaderState(GameManager managerRef, GlobalData globalRef, string sceneName)
        {
            manager = managerRef;
            globalVar = globalRef;
            // nextstate = nextState;


            scene = sceneName;
            stateName = "loaderState";
        }

        public void getData()
        {
            // GET HERO ENTITY
            entityObject = GameObject.Find("Entity");
            entity = entityObject.GetComponent<LoaderEntity>();
            entity.sceneName = scene;
            entity.globalVar = globalVar;
            entity.manager = manager;
            entity.LoadButton();
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




