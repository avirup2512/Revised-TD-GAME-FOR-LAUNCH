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


    public class LostState : GameState
    {
        private GameManager manager;
        public string stateName;

        public GlobalData globalVar;

        private GameObject playButtonObject;
        private Button playButton;

        private GameObject menuButtonObject;
        private Button menuButton;
        RoundGroupClass savedRoundGroupClass;
        public LostState(GameManager managerRef, GlobalData globalRef)
        {
            manager = managerRef;
            globalVar = globalRef;
            stateName = "lostState";
        }

        public void getData()
        {

            playButtonObject = GameObject.Find("PlayButton");
            playButton = playButtonObject.GetComponent<Button>();


            menuButtonObject = GameObject.Find("MenuButton");
            menuButton = menuButtonObject.GetComponent<Button>();

            playButton.onClick.AddListener(this.playAgain);
            menuButton.onClick.AddListener(this.goTomenu);
            savedRoundGroupClass = manager.getSavedGameState();
            if (savedRoundGroupClass != null)
            {
                for (int x = 0; x < savedRoundGroupClass.AllWeapon.Count; x++)
                {
                    if (savedRoundGroupClass.AllWeapon[x].name == globalVar.PrimaryWeaponSelected.name)
                    {
                        savedRoundGroupClass.AllWeapon[x] = globalVar.PrimaryWeaponSelected;
                    }
                    if (savedRoundGroupClass.AllWeapon[x].name == globalVar.SecondaryWeaponSelected.name)
                    {
                        savedRoundGroupClass.AllWeapon[x] = globalVar.SecondaryWeaponSelected;
                    }
                    if (savedRoundGroupClass.AllWeapon[x].name == globalVar.SpecialWeaponSelected.name)
                    {
                        savedRoundGroupClass.AllWeapon[x] = globalVar.SpecialWeaponSelected;
                    }
                    if (savedRoundGroupClass.AllWeapon[x].name == globalVar.BombWeaponSelected.name)
                    {
                        savedRoundGroupClass.AllWeapon[x] = globalVar.BombWeaponSelected;
                    }
                    if (savedRoundGroupClass.AllWeapon[x].name == globalVar.ArtiliaryWeaponSelected.name)
                    {
                        savedRoundGroupClass.AllWeapon[x] = globalVar.ArtiliaryWeaponSelected;
                    }
                    if (savedRoundGroupClass.AllWeapon[x].name == globalVar.ThunderWeaponSelected.name)
                    {
                        savedRoundGroupClass.AllWeapon[x] = globalVar.ThunderWeaponSelected;
                    }
                    if (savedRoundGroupClass.AllWeapon[x].name == globalVar.AtomBombWeaponSelected.name)
                    {
                        savedRoundGroupClass.AllWeapon[x] = globalVar.AtomBombWeaponSelected;
                    }
                }
                savedRoundGroupClass.TotalCoins = globalVar.totalCoin;
                savedRoundGroupClass.TotalDiamond = globalVar.totalDiamond;
                manager.saveCurrentGameState(savedRoundGroupClass);
            }

        }

        void playAgain()
        {
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "Play"));
        }

        void goTomenu()
        {
            SceneManager.LoadScene("loader");
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


