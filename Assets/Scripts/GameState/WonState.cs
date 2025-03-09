using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Assets.Script.entity;
using Assets.Script.globalVar;
using TMPro;

namespace Assets.Script.state
{


    public class WonState : GameState
    {
        private GameManager manager;
        public string stateName;

        public GlobalData globalVar;

        private GameObject playButtonObject;
        private Button playButton;

        private Button NextButton;

        private GameObject menuButtonObject;
        private Button menuButton;

        RoundGroupClass savedRoundGroupClass;
        GameObject CoinTextObject;
        TextMeshProUGUI CoinText;

        GameObject DiamondTextObject;
        TextMeshProUGUI DiamondText;

        Round NextRound;

        public WonState(GameManager managerRef, GlobalData globalRef)
        {
            manager = managerRef;
            globalVar = globalRef;
            stateName = "lostState";
        }

        public void getData()
        {
            savedRoundGroupClass = manager.getSavedGameState();
            int diamondIncrease = 1;
            if (globalVar.activeRound.roundNumber > 5 && globalVar.activeRound.roundNumber < 11)
            {
                diamondIncrease = 2;
            }
            else if (globalVar.activeRound.roundNumber > 11 && globalVar.activeRound.roundNumber < 15)
            {
                diamondIncrease = 3;
            }
            else if (globalVar.activeRound.roundNumber > 15 && globalVar.activeRound.roundNumber < 21)
            {
                diamondIncrease = 4;
            }
            else if (globalVar.activeRound.roundNumber > 21)
            {
                diamondIncrease = 5;
            }
            globalVar.totalDiamond = (globalVar.totalDiamond) + 100 * diamondIncrease;

            if (savedRoundGroupClass != null)
            {
                for (int i = 0; i < savedRoundGroupClass.roundGroup.Count; i++)
                {
                    for (int x = 0; x < savedRoundGroupClass.roundGroup[i].totalRounds.Count; x++)
                    {
                        if (globalVar.activeRound.roundNumber == savedRoundGroupClass.roundGroup[i].totalRounds[x].roundNumber)
                        {
                            if (savedRoundGroupClass.roundGroup[i].totalRounds[x].mainRound)
                            {
                                if ((i + 1) < savedRoundGroupClass.roundGroup.Count)
                                {
                                    savedRoundGroupClass.roundGroup[i + 1].totalRounds[0].locked = false;
                                    NextRound = savedRoundGroupClass.roundGroup[i + 1].totalRounds[0];
                                }
                            }
                            else
                            {
                                if ((x + 1) < savedRoundGroupClass.roundGroup[i].totalRounds.Count)
                                {
                                    savedRoundGroupClass.roundGroup[i].totalRounds[x + 1].locked = false;
                                    NextRound = savedRoundGroupClass.roundGroup[i].totalRounds[x + 1];
                                }
                            }

                        }
                    }
                }
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
            playButtonObject = GameObject.Find("PlayButton");
            playButton = playButtonObject.GetComponent<Button>();

            NextButton = GameObject.Find("NextButton").GetComponent<Button>();
            NextButton.onClick.AddListener(this.PlayNextRound);
            if (globalVar.activeRound.roundNumber == 50)
                NextButton.interactable = false;

            menuButtonObject = GameObject.Find("MenuButton");
            menuButton = menuButtonObject.GetComponent<Button>();

            playButton.onClick.AddListener(this.playAgain);
            menuButton.onClick.AddListener(this.goTomenu);

            CoinTextObject = GameObject.Find("CoinText");
            CoinText = CoinTextObject.GetComponent<TextMeshProUGUI>();
            CoinText.text = globalVar.totalCoin.ToString();

            DiamondTextObject = GameObject.Find("DiamondText");
            DiamondText = DiamondTextObject.GetComponent<TextMeshProUGUI>();
            DiamondText.text = globalVar.totalDiamond.ToString();
        }

        void playAgain()
        {
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "Play"));
        }

        void PlayNextRound()
        {
            globalVar.setActiveRound(NextRound);
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "Play"));
        }

        void goTomenu()
        {
            // SceneManager.LoadScene("menuState");
            //   manager.switchState(new menuState(manager,globalVar));

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



