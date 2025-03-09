using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Script.entity;
using Assets.Script.globalVar;
using UnityEngine.EventSystems;
using TMPro;

namespace Assets.Script.state
{
    public class SceneSelectionState : GameState
    {
        private GameManager manager;
        public string stateName = "stateSelection";

        public GameObject roundObject;
        public GameObject roundGroupObject;
        public GameObject[] round;
        public GameObject scrollPanel;
        public GameObject scrollView;

        public GameObject Viewport;

        public GameObject Content;
        public RectTransform ContentRect;

        public RectTransform rect;
        public RectTransform scrollRect;
        public float buttonWidth;

        public GameObject comingSoonObject;

        public Text text;
        public GameObject textObject;
        public RoundSelectionButton roundButtonCS;
        public GlobalData globalVar;

        private TextMeshProUGUI coinCollectionTextComponent;
        private TextMeshProUGUI diamondCollectionTextComponent;

        public Button btn;
        public int ScrollCounter = 0;
        public int activeRoundCounter = 0;
        RoundGroupClass savedRoundGroupClass;

        private float[] roundsStack = new float[10];

        public ScrollRect scrollRectComponent;
        Button NextButton;
        Button PreviousButton;
        public SceneSelectionState(GameManager managerRef, GlobalData globalRef)
        {
            manager = managerRef;
            globalVar = globalRef;
            //gameData = new globalGameData();
        }
        public void OnEnable()
        {

        }

        public void stateUpdate()
        {

        }
        void gotoShop()
        {
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "Shop"));
        }
        public void getData()
        {
            //Application.targetFrameRate = -1;
            scrollPanel = GameObject.Find("MainPanel");
            if (scrollPanel)
            {
                // ADD THE NEXT AND PREVIOUS BUTTON FUNCTIONALITY STARTS
                GameObject NextButtonObject = GameObject.Find("Next");
                NextButton = NextButtonObject.GetComponent<Button>();

                GameObject shopBtn = GameObject.Find("shopBtn");
                Button shopBtnComp = shopBtn.GetComponent<Button>();
                shopBtnComp.onClick.AddListener(this.gotoShop);

                GameObject PreviousButtonObject = GameObject.Find("Previous");
                PreviousButton = PreviousButtonObject.GetComponent<Button>();
                PreviousButton.interactable = false;

                NextButton.onClick.AddListener(delegate { this.ScrollItem("Next", NextButton, PreviousButton); });
                PreviousButton.onClick.AddListener(delegate { this.ScrollItem("Previous", NextButton, PreviousButton); });
                // ADD THE NEXT AND PREVIOUS BUTTON FUNCTIONALITY ENDS

                GameObject BackButtonObject = GameObject.Find("Back Button");
                Button BackButton = BackButtonObject.GetComponent<Button>();
                BackButton.onClick.AddListener(this.gotoMenu);

                scrollView = GameObject.Find("Scroll View");
                Viewport = GameObject.Find("Viewport");
                Content = GameObject.Find("Content");
                ContentRect = Content.GetComponent<RectTransform>();
                scrollRect = scrollPanel.GetComponent<RectTransform>();

                // ADD AUTO SCROLL FUNCTIONALITY
                scrollRectComponent = scrollView.GetComponent<ScrollRect>();
                scrollRectComponent.onValueChanged.AddListener(this.onValueChanged);
                round = new GameObject[10];
                float contentWidth = 0;
                float contentHeight = ContentRect.rect.height;
                roundGroupObject = Resources.Load("Prefabs/Round Selection Assets/Round Group") as GameObject;
                roundObject = Resources.Load("Prefabs/Round Selection Assets/Round") as GameObject;
                // Getting Saved Game Data
                savedRoundGroupClass = manager.getSavedGameState();
                if (savedRoundGroupClass != null)
                {
                    coinCollectionTextComponent = GameObject.Find("coinText").GetComponent<TextMeshProUGUI>();
                    coinCollectionTextComponent.text = savedRoundGroupClass.TotalCoins.ToString();

                    diamondCollectionTextComponent = GameObject.Find("diamondText").GetComponent<TextMeshProUGUI>();
                    diamondCollectionTextComponent.text = savedRoundGroupClass.TotalDiamond.ToString();


                    for (int i = 0; i < savedRoundGroupClass.roundGroup.Count; i++)
                    {
                        roundsStack[i] = float.Parse("0." + i);
                        round[i] = GameObject.Instantiate(roundGroupObject, Content.transform) as GameObject;
                        rect = round[i].GetComponent<RectTransform>();
                        buttonWidth = rect.rect.width;
                        rect.anchoredPosition = new Vector3(0, 0.5f, 0);
                        rect.pivot = new Vector2(1.00f, 0.5f);
                        rect.localPosition = new Vector3((buttonWidth + 10) * i, 0, 0);
                        contentWidth += buttonWidth + 10;
                        for (int x = 0; x < savedRoundGroupClass.roundGroup[i].totalRounds.Count; x++)
                        {
                            Round roundClass = savedRoundGroupClass.roundGroup[i].totalRounds[x];
                            GameObject roundWithinGroup = GameObject.Instantiate(roundObject, round[i].transform) as GameObject;
                            roundButtonCS = roundWithinGroup.GetComponent<RoundSelectionButton>();
                            roundButtonCS.setName(roundClass);
                            btn = roundWithinGroup.transform.GetComponent<Button>();
                            Transform LockedImage = roundWithinGroup.transform.GetChild(1);
                            if (!roundClass.locked)
                            {
                                activeRoundCounter++;
                                btn.interactable = true;
                                LockedImage.gameObject.SetActive(false);
                                SetButtonOnClick(btn, roundClass);
                            }
                            else
                            {
                                LockedImage.gameObject.SetActive(true);
                            }
                        }
                    }
                }
                ContentRect.sizeDelta = new Vector2(contentWidth, contentHeight);
                ContentRect.pivot = new Vector2(0, 0.5f);
            }
            if (activeRoundCounter > 5)
            {
                scrollRectComponent.horizontalNormalizedPosition = roundsStack[1];
            }
            if (activeRoundCounter > 10)
            {
                scrollRectComponent.horizontalNormalizedPosition = roundsStack[2];
            }
            if (activeRoundCounter > 15)
            {
                scrollRectComponent.horizontalNormalizedPosition = roundsStack[3];
            }
            if (activeRoundCounter > 20)
            {
                scrollRectComponent.horizontalNormalizedPosition = roundsStack[4];
            }
            if (activeRoundCounter > 25)
            {
                scrollRectComponent.horizontalNormalizedPosition = roundsStack[5];
            }
            if (activeRoundCounter > 30)
            {
                scrollRectComponent.horizontalNormalizedPosition = roundsStack[6];
            }
            if (activeRoundCounter > 35)
            {
                scrollRectComponent.horizontalNormalizedPosition = roundsStack[7];
            }
            if (activeRoundCounter > 40)
            {
                scrollRectComponent.horizontalNormalizedPosition = roundsStack[8];
            }
            if (activeRoundCounter > 45)
            {
                scrollRectComponent.horizontalNormalizedPosition = roundsStack[9];
            }
        }
        void gotoPlay(Round round)
        {
            globalVar.setActiveRound(round);
            globalVar.setTotalIncomingEnemy(round.totalIncomingEnemy);
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "Play"));
        }
        void gotoMenu()
        {
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "Menu"));
        }
        void SetButtonOnClick(Button btn, Round round)
        {

            btn.onClick.AddListener(delegate { this.gotoPlay(round); });
        }

        void ScrollItem(string indicator, Button NextBtn, Button PrevBtn)
        {

            if (indicator == "Next" && ScrollCounter < 9)
            {
                if (ScrollCounter < 9)
                {
                    ScrollCounter++;
                    scrollRectComponent.horizontalNormalizedPosition = roundsStack[ScrollCounter];
                }

            }
            else if (indicator == "Previous" && scrollRectComponent.horizontalNormalizedPosition >= 0f)
            {
                if (ScrollCounter > 0)
                {
                    ScrollCounter--;
                    scrollRectComponent.horizontalNormalizedPosition = roundsStack[ScrollCounter];
                }
            }

        }
        void onValueChanged(Vector2 pos)
        {
            float currentStack = scrollRectComponent.horizontalNormalizedPosition;
            string curentStack = string.Format("{0:0.0}", currentStack);
            for (int h = 0; h < roundsStack.Length; h++)
            {
                if (roundsStack[h] == float.Parse(curentStack))
                {
                    ScrollCounter = h;
                }
            }
            if (ScrollCounter == 0)
            {
                PreviousButton.interactable = false;
                NextButton.interactable = true;
            }
            else if (ScrollCounter == 9)
            {
                PreviousButton.interactable = true;
                NextButton.interactable = false;
            }
            else
            {
                NextButton.interactable = true;
                PreviousButton.interactable = true;
            }
        }
        public string getStateName()
        {
            return this.stateName;
        }
        public void action(string action)
        {

        }
        ~SceneSelectionState()
        {
        }
    }
}
