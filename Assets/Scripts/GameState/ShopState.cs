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
    public class ShopState : GameState
    {
        private GameManager manager;
        private GameObject buttonObject;
        private Sprite activeTabBtn;
        private Sprite inActiveTabBtn;
        private Sprite selectActiveBtn;
        private Sprite selectInActiveBtn;
        private Sprite selectActiveGunBoxImg;
        private Sprite selectInactiveGunBoxImg;
        private Sprite buyBtnImg;
        private Sprite boxActiveImg;
        private Sprite boxInActiveImgPrimary;
        private Sprite boxInActiveImgSec;
        private Sprite boxInActiveImgSpc;
        private Sprite activeWeaponTabBtnSprite;
        private Sprite inActiveWeaponTabBtnSprite;
        private Sprite activeDiamondTabBtnSprite;
        private Sprite inActiveDiamondTabBtnSprite;
        private Sprite activeGuardsTabBtnSprite;
        private Sprite inActiveGuardsTabBtnSprite;

        private GameObject homeBtn;
        private GameObject homeBtnToHome;
        private GameObject gunBox;
        private GameObject uniqueGunBox;
        private GameObject gunSelectionBox;
        private GameObject gunSelectionBar;
        private GameObject gunSelectionBarBottom;
        private GameObject primaryGunBoxContent;
        private GameObject secondaryGunBoxContent;
        private GameObject specialGunBoxContent;
        private GameObject bombGunBoxContent;
        private GameObject uniqueGunBoxContent;
        private GameObject uniqueGunLebel;


        private GameObject primaryTab;
        private Button primaryTabButton;

        private GameObject secondaryTab;
        private Button secondaryTabButton;

        private GameObject specialTab;
        private Button specialTabButton;

        private GameObject bombTab;
        private Button bombTabButton;

        private GameObject BuyDiamond;

        private GameObject uniqueTab;
        private Button uniqueTabButton;

        private GameObject weaponTabBtn;
        private GameObject diamondTabBtn;
        private GameObject guardsTabBtn;
        private GameObject weaponTabContent;
        private GameObject guardsTabContent;
        private GameObject diamondTabContent;

        private TextMeshProUGUI totalCoinTxt;
        private TextMeshProUGUI totalDiamondTxt;

        private GameObject primaryTabContant;
        private GameObject secondaryTabContant;
        private GameObject specialTabContant;
        private GameObject bombTabContent;
        private GameObject uniqueTabContent;
        private GameObject ContentDiamond;
        private GameObject ContentGuards;

        private GameObject modalBack;
        private GameObject modalBackCoinText;
        private GameObject modalBackCrossBtn;

        private GameObject modalBackDmnd;
        private GameObject modalBackDmndText;
        private GameObject modalBackCrossBtnDmnd;

        private AudioSource upgradeAudio;
        private AudioSource coinErrorModal;
        private AudioSource buyAmmoAudio;
        private AudioSource selectBtnAudio;
        private AudioSource tabAudio;
        private AudioSource diamondTap;

        private GameObject[] selectedWeapon;
        private GameObject[] selectedWeaponBottom;

        private GameObject levelIndicatorBox;

        //private GameObject[] gunContents;
        private GameObject[] selectBtn;
        private GameObject[] upgradeBtn;
        private GameObject[] removeMagzineBtn;
        private GameObject[] buyBtn;
        private GameObject[] boxImg;
        private GameObject[] slectBtnTxt;
        private GameObject[] gunContentsI;
        private GameObject[] diamondsBox;
        private GameObject[] guardBox;

        public GlobalData globalVar;
        public InterstitialAd ads;
        public bool sceneSelectionStateTrigget = false;
        public bool homeStateTrigget = false;

        public string stateName;
        RoundGroupClass savedRoundGroupClass;

        public ShopState(GameManager managerRef, GlobalData globalRef)
        {
            manager = managerRef;
            globalVar = globalRef;
            stateName = "shopState";

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
            manager.getSavedGameState();
            int cnt = globalVar.WeaponList.Count;
            loadSprite();
            savedRoundGroupClass = manager.getSavedGameState();
            if (savedRoundGroupClass != null)
            {
                globalVar.totalCoin = savedRoundGroupClass.TotalCoins;
                globalVar.totalDiamond = savedRoundGroupClass.TotalDiamond;
                globalVar.ExistingGuardList = savedRoundGroupClass.Guards;
                generateLayout(savedRoundGroupClass);
            }
            ads = GameObject.Find("AdsObject").GetComponent<InterstitialAd>();
            // ads.LoadAd();

            weaponTabBtn = GameObject.Find("weaponTabBtn");
            diamondTabBtn = GameObject.Find("diamondTabBtn");
            guardsTabBtn = GameObject.Find("guardsTabBtn");
            weaponTabContent = GameObject.Find("weaponTabContent");
            guardsTabContent = GameObject.Find("guardsTabContent");
            diamondTabContent = GameObject.Find("diamondTabContent");
            BuyDiamond = GameObject.Find("BuyDiamond");

            weaponTabBtn.GetComponent<Button>().onClick.AddListener(this.setWeaponTabActive);
            diamondTabBtn.GetComponent<Button>().onClick.AddListener(this.setDiamondTabActive);
            BuyDiamond.GetComponent<Button>().onClick.AddListener(this.setDiamondTabActive);
            guardsTabBtn.GetComponent<Button>().onClick.AddListener(this.setGuardsTabActive);

            totalCoinTxt = GameObject.Find("totalCoin").GetComponent<TextMeshProUGUI>();
            totalCoinTxt.text = globalVar.totalCoin.ToString();
            totalDiamondTxt = GameObject.Find("totalDiamond").GetComponent<TextMeshProUGUI>();
            totalDiamondTxt.text = globalVar.totalDiamond.ToString();

            primaryTabContant = GameObject.Find("primaryTabContent");
            secondaryTabContant = GameObject.Find("secondaryTabContent");
            specialTabContant = GameObject.Find("specialTabContent");
            bombTabContent = GameObject.Find("bombTabContent");
            uniqueTabContent = GameObject.Find("uniqueTabContent");

            //warning modal, default not showing
            modalBack = GameObject.Find("modalBack");
            modalBackCrossBtn = modalBack.transform.Find("Panel").transform.Find("cross").gameObject;
            modalBackCrossBtn.GetComponent<Button>().onClick.AddListener(() => modalBack.SetActive(false));
            modalBack.SetActive(false);

            modalBackDmnd = GameObject.Find("modalBackDmnd");
            modalBackCrossBtnDmnd = modalBackDmnd.transform.Find("Panel").transform.Find("crossDmnd").gameObject;
            modalBackCrossBtnDmnd.GetComponent<Button>().onClick.AddListener(() => modalBackDmnd.SetActive(false));
            modalBackDmnd.SetActive(false);

            primaryTab = GameObject.Find("primaryTab");
            primaryTabButton = primaryTab.GetComponent<Button>();
            primaryTabButton.onClick.AddListener(this.setPrimaryTabActive);

            secondaryTab = GameObject.Find("secondaryTab");
            secondaryTabButton = secondaryTab.GetComponent<Button>();
            secondaryTabButton.onClick.AddListener(this.setSecondaryTabActive);

            specialTab = GameObject.Find("specialTab");
            specialTabButton = specialTab.GetComponent<Button>();
            specialTabButton.onClick.AddListener(this.setSpecialTabActive);

            bombTab = GameObject.Find("bombTab");
            bombTabButton = bombTab.GetComponent<Button>();
            bombTabButton.onClick.AddListener(this.setBombTabActive);

            uniqueTab = GameObject.Find("uniqueTab");
            uniqueTabButton = uniqueTab.GetComponent<Button>();
            uniqueTabButton.onClick.AddListener(this.setuniqueTabActive);

            homeBtn = GameObject.Find("homeBtn");
            homeBtn.GetComponent<Button>().onClick.AddListener(this.adsRunThenGotoSceneselection);

            homeBtnToHome = GameObject.Find("HomeTo");
            homeBtnToHome.GetComponent<Button>().onClick.AddListener(this.adsRunThenGotoHome);

            //default
            setWeaponTabActive();
            setPrimaryTabActive();
        }
        public void adsRunThenGotoSceneselection()
        {
            // ads.ShowAd();
            sceneSelectionStateTrigget = true;
        }
        public void adsRunThenGotoHome()
        {
            // ads.ShowAd();
            homeStateTrigget = true;
        }
        //Hand guns
        void setPrimaryTabActive()
        {
            primaryTabContant.SetActive(true);
            secondaryTabContant.SetActive(false);
            specialTabContant.SetActive(false);
            bombTabContent.SetActive(false);
            uniqueTabContent.SetActive(false);
            primaryTab.GetComponent<Image>().sprite = activeTabBtn;
            secondaryTab.GetComponent<Image>().sprite = inActiveTabBtn;
            specialTab.GetComponent<Image>().sprite = inActiveTabBtn;
            bombTab.GetComponent<Image>().sprite = inActiveTabBtn;
            uniqueTab.GetComponent<Image>().sprite = inActiveTabBtn;
        }
        //machine gun
        void setSecondaryTabActive()
        {
            primaryTabContant.SetActive(false);
            secondaryTabContant.SetActive(true);
            bombTabContent.SetActive(false);
            uniqueTabContent.SetActive(false);
            specialTabContant.SetActive(false);
            primaryTab.GetComponent<Image>().sprite = inActiveTabBtn;
            secondaryTab.GetComponent<Image>().sprite = activeTabBtn;
            specialTab.GetComponent<Image>().sprite = inActiveTabBtn;
            bombTab.GetComponent<Image>().sprite = inActiveTabBtn;
            uniqueTab.GetComponent<Image>().sprite = inActiveTabBtn;
        }
        //luancher 
        void setSpecialTabActive()
        {
            primaryTabContant.SetActive(false);
            secondaryTabContant.SetActive(false);
            bombTabContent.SetActive(false);
            uniqueTabContent.SetActive(false);
            specialTabContant.SetActive(true);
            primaryTab.GetComponent<Image>().sprite = inActiveTabBtn;
            secondaryTab.GetComponent<Image>().sprite = inActiveTabBtn;
            bombTab.GetComponent<Image>().sprite = inActiveTabBtn;
            uniqueTab.GetComponent<Image>().sprite = inActiveTabBtn;
            specialTab.GetComponent<Image>().sprite = activeTabBtn;
        }
        void setBombTabActive()
        {
            primaryTabContant.SetActive(false);
            secondaryTabContant.SetActive(false);
            specialTabContant.SetActive(false);
            bombTabContent.SetActive(true);
            uniqueTabContent.SetActive(false);
            primaryTab.GetComponent<Image>().sprite = inActiveTabBtn;
            secondaryTab.GetComponent<Image>().sprite = inActiveTabBtn;
            specialTab.GetComponent<Image>().sprite = inActiveTabBtn;
            bombTab.GetComponent<Image>().sprite = activeTabBtn;
            uniqueTab.GetComponent<Image>().sprite = inActiveTabBtn;
        }

        void setuniqueTabActive()
        {
            primaryTabContant.SetActive(false);
            secondaryTabContant.SetActive(false);
            specialTabContant.SetActive(false);
            bombTabContent.SetActive(false);
            uniqueTabContent.SetActive(true);
            primaryTab.GetComponent<Image>().sprite = inActiveTabBtn;
            secondaryTab.GetComponent<Image>().sprite = inActiveTabBtn;
            specialTab.GetComponent<Image>().sprite = inActiveTabBtn;
            bombTab.GetComponent<Image>().sprite = inActiveTabBtn;
            uniqueTab.GetComponent<Image>().sprite = activeTabBtn;
        }

        void generateLayout(RoundGroupClass roundGroupClass)
        {
            globalVar.WeaponListForShopPage.Clear();

            primaryGunBoxContent = GameObject.Find("ContentPrimary");
            secondaryGunBoxContent = GameObject.Find("ContentSecondary");
            specialGunBoxContent = GameObject.Find("ContentSpecial");
            bombGunBoxContent = GameObject.Find("ContentBomb");
            uniqueGunBoxContent = GameObject.Find("ContentUnique");
            gunBox = Resources.Load("Prefabs/shop/gunBox") as GameObject;
            uniqueGunBox = Resources.Load("Prefabs/shop/uniqueWeaponBox") as GameObject;

            globalVar.WeaponListForShopPage = roundGroupClass.AllWeapon;
            int cnt = globalVar.WeaponListForShopPage.Count;
            upgradeAudio = GameObject.Find("upgradeAudio").GetComponent<AudioSource>();
            coinErrorModal = GameObject.Find("coinErrorModal").GetComponent<AudioSource>();
            buyAmmoAudio = GameObject.Find("buyAmmoAudio").GetComponent<AudioSource>();
            selectBtnAudio = GameObject.Find("selectBtnAudio").GetComponent<AudioSource>();
            tabAudio = GameObject.Find("tabAudio").GetComponent<AudioSource>();
            diamondTap = GameObject.Find("diamondTap").GetComponent<AudioSource>();
            selectBtn = new GameObject[cnt];
            boxImg = new GameObject[cnt];
            buyBtn = new GameObject[cnt];
            slectBtnTxt = new GameObject[cnt];
            gunContentsI = new GameObject[cnt];
            upgradeBtn = new GameObject[cnt];
            this.generateGunSelectionLayout();
            // ITERATING THE UNLOCKED WEAPON
            for (int n = 0; n < cnt; n++)
            {
                if (globalVar.WeaponListForShopPage[n].category == "primary")
                {
                    gunContentsI[n] =
                    GameObject.Instantiate(gunBox, primaryGunBoxContent.transform, worldPositionStays: false) as GameObject;
                }
                else if (globalVar.WeaponListForShopPage[n].category == "secondary")
                {
                    gunContentsI[n] =
                    GameObject.Instantiate(gunBox, secondaryGunBoxContent.transform, worldPositionStays: false) as GameObject;
                }
                else if (globalVar.WeaponListForShopPage[n].category == "special")
                {
                    gunContentsI[n] =
                    GameObject.Instantiate(gunBox, specialGunBoxContent.transform, worldPositionStays: false) as GameObject;
                }
                else if (globalVar.WeaponListForShopPage[n].category == "bomb")
                {
                    gunContentsI[n] =
                    GameObject.Instantiate(gunBox, bombGunBoxContent.transform, worldPositionStays: false) as GameObject;
                }
                else if (globalVar.WeaponListForShopPage[n].category == "unique")
                {
                    gunContentsI[n] =
                    GameObject.Instantiate(uniqueGunBox, uniqueGunBoxContent.transform, worldPositionStays: false) as GameObject;
                }

                GameObject gunImg = gunContentsI[n].transform.GetChild(0).gameObject;
                gunImg.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalVar.WeaponListForShopPage[n].gunSprite) as Sprite;

                GameObject buyBtnCoinText = gunContentsI[n].transform.Find("buyAmmo").Find("coinText").gameObject;
                GameObject buyBtnCoinImg = gunContentsI[n].transform.Find("buyAmmo").Find("coin").gameObject;
                GameObject buyBtnTxt = gunContentsI[n].transform.Find("buyAmmo").Find("Text").gameObject;
                selectBtn[n] = gunContentsI[n].transform.Find("selectBtn").gameObject;
                upgradeBtn[n] = gunContentsI[n].transform.Find("upgradeBtn").gameObject;
                slectBtnTxt[n] = gunContentsI[n].transform.Find("selectBtn").Find("Text").gameObject;

                buyBtn[n] = gunContentsI[n].transform.Find("buyAmmo").gameObject;
                addEventListener(buyBtn[n], globalVar.WeaponListForShopPage[n], "buy", gunContentsI[n], n);
                addEventListener(selectBtn[n], globalVar.WeaponListForShopPage[n], "select", gunContentsI[n], n);
                addEventListener(upgradeBtn[n], globalVar.WeaponListForShopPage[n], "upgrade", gunContentsI[n], n);

                buyBtn[n].GetComponent<Image>().sprite = buyBtnImg;
                if (globalVar.WeaponListForShopPage[n].bulletPrice > 0)
                {
                    buyBtnCoinText.SetActive(true);
                    buyBtnCoinImg.SetActive(true);
                    buyBtnCoinText.GetComponent<TextMeshProUGUI>().text = globalVar.WeaponListForShopPage[n].bulletPrice.ToString();
                }
                else
                {
                    buyBtnCoinText.SetActive(false);
                    buyBtnCoinImg.SetActive(false);
                    buyBtnTxt.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                }

                GameObject lockedLayer = gunContentsI[n].transform.Find("LockedLayer").gameObject;

                if (!globalVar.WeaponListForShopPage[n].isLocked)
                {
                    lockedLayer.SetActive(false);
                    if (globalVar.WeaponListForShopPage[n].isSelect)
                    {
                        if (globalVar.WeaponListForShopPage[n].category != "unique")
                        {
                            selectBtn[n].GetComponent<Image>().sprite = selectInActiveBtn;
                            buyBtn[n].GetComponent<Button>().interactable = true;
                            selectBtn[n].GetComponent<Button>().interactable = false;
                            slectBtnTxt[n].GetComponent<TextMeshProUGUI>().text = "Selected";
                            gunContentsI[n].GetComponent<Image>().sprite = selectActiveGunBoxImg;
                        }
                        // set top bar gun selection image based on default selection
                        if (globalVar.WeaponListForShopPage[n].category == "primary")
                        {
                            globalVar.primaryGunSprite = globalVar.WeaponListForShopPage[n].gunSprite;
                            globalVar.heroSpritePrimary = globalVar.WeaponListForShopPage[n].heroSprite;
                            this.setSelectionMagazineText(0, globalVar.WeaponListForShopPage[n]);
                            this.setSelectionSprite(0);
                        }
                        else if (globalVar.WeaponListForShopPage[n].category == "secondary")
                        {
                            globalVar.secondaryGunSprite = globalVar.WeaponListForShopPage[n].gunSprite;
                            globalVar.heroSpriteSecondary = globalVar.WeaponListForShopPage[n].heroSprite;
                            this.setSelectionMagazineText(1, globalVar.WeaponListForShopPage[n]);
                            this.setSelectionSprite(1);
                        }
                        else if (globalVar.WeaponListForShopPage[n].category == "special")
                        {
                            globalVar.specialGunSprite = globalVar.WeaponListForShopPage[n].gunSprite;
                            globalVar.heroSpriteSpecial = globalVar.WeaponListForShopPage[n].heroSprite;
                            this.setSelectionMagazineText(2, globalVar.WeaponListForShopPage[n]);
                            this.setSelectionSprite(2);
                        }
                        else if (globalVar.WeaponListForShopPage[n].category == "bomb")
                        {
                            globalVar.bombGunSprite = globalVar.WeaponListForShopPage[n].gunSprite;
                            globalVar.heroSpriteBomb = globalVar.WeaponListForShopPage[n].heroSprite;
                            this.setSelectionMagazineText(3, globalVar.WeaponListForShopPage[n]);
                            this.setSelectionSprite(3);
                        }
                    }
                    else
                    {
                        if (globalVar.WeaponListForShopPage[n].category != "unique")
                        {
                            selectBtn[n].GetComponent<Image>().sprite = selectActiveBtn;
                            buyBtn[n].GetComponent<Button>().interactable = false;
                            selectBtn[n].GetComponent<Button>().interactable = true;
                            slectBtnTxt[n].GetComponent<TextMeshProUGUI>().text = "Select";
                            gunContentsI[n].GetComponent<Image>().sprite = selectInactiveGunBoxImg;
                        }
                    }
                }
                else
                {
                    lockedLayer.SetActive(true);
                }

                if (globalVar.WeaponListForShopPage[n].category == "unique")
                {
                    uniqueGunLebel = gunContentsI[n].transform.Find("details").gameObject;
                    uniqueGunLebel.GetComponent<TextMeshProUGUI>().text = globalVar.WeaponListForShopPage[n].labelName;
                    GameObject levelSection = gunContentsI[n].transform.Find("levelSection").gameObject;
                    selectBtn[n].transform.Find("coinText").GetComponent<TextMeshProUGUI>().text =
                        globalVar.WeaponListForShopPage[n].bulletPrice.ToString();
                    this.setUniqueWeaponTabUi(buyBtn[n], upgradeBtn[n], levelSection);
                    if (globalVar.WeaponListForShopPage[n].totalMagezine > 0)
                    {
                        if (globalVar.WeaponListForShopPage[n].name == "Atom")
                        {
                            globalVar.uniqueWeapon1BulletQuantity = globalVar.WeaponListForShopPage[n].totalMagezine;
                            this.setUniqueSelectionSprite(0);
                            this.setUniqueSelectionMagazineText(0);
                        }
                        else if (globalVar.WeaponListForShopPage[n].name == "Artiliary")
                        {
                            globalVar.uniqueWeapon2BulletQuantity = globalVar.WeaponListForShopPage[n].totalMagezine;
                            this.setUniqueSelectionSprite(1);
                            this.setUniqueSelectionMagazineText(1);
                        }
                        else if (globalVar.WeaponListForShopPage[n].name == "Thunder")
                        {
                            globalVar.uniqueWeapon3BulletQuantity = globalVar.WeaponListForShopPage[n].totalMagezine;
                            this.setUniqueSelectionSprite(2);
                            this.setUniqueSelectionMagazineText(2);
                        }
                    }

                    // buy/select button max reached code for unique weapon
                    if (globalVar.WeaponListForShopPage[n].totalMagezine ==
                        globalVar.WeaponListForShopPage[n].maxMagazine)
                    {
                        selectBtn[n].GetComponent<Button>().interactable = false;
                        selectBtn[n].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Max Reached";
                        selectBtn[n].transform.Find("coinText").gameObject.SetActive(false);
                    }
                    else
                    {
                        selectBtn[n].transform.Find("coinText").gameObject.SetActive(true);
                    }
                }
                else
                { // buy button max reached code for other weapon (not unique)
                    if (globalVar.WeaponListForShopPage[n].totalMagezine ==
                        globalVar.WeaponListForShopPage[n].maxMagazine)
                    {
                        buyBtn[n].GetComponent<Button>().interactable = false;
                        buyBtn[n].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Max Reached";
                    }
                }

                this.setGunLevel(gunContentsI[n], globalVar.WeaponListForShopPage[n]);
                this.setUpgradeBtn(upgradeBtn[n], globalVar.WeaponListForShopPage[n]);
            }

            //Diamond Tab layout
            diamondsBox = new GameObject[4];
            ContentDiamond = GameObject.Find("ContentDiamond");
            GameObject diamonBoxCnt = Resources.Load("Prefabs/shop/diamondBox") as GameObject;
            GameObject diamondSprite;
            GameObject guardImageSprite;
            for (int d = 0; d < 4; d++)
            {
                int g = d + 1;
                diamondsBox[d] =
                    GameObject.Instantiate(diamonBoxCnt, ContentDiamond.transform, worldPositionStays: false) as GameObject;
                diamondSprite = diamondsBox[d].transform.Find("diamondImg").gameObject;
                diamondSprite.GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("Sprites/UI/shop/diamonds/diamonds" + g) as Sprite;

                string texts = d == 0 ? "200 Diamonds" : d == 1 ? "400 diamond" :
                 d == 2 ? "600 diamond" : d == 3 ? "800 diamond" : "";
                int amount = d == 0 ? 5000 : d == 1 ? 7000 : d == 2 ? 8000 : d == 3 ? 12000 : 0;
                diamondsBox[d].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = texts;
                Button btn = diamondsBox[d].transform.Find("selectBtn").GetComponent<Button>();
                int quntiy = ((d + 1) * 100) * 2;
                btn.transform.Find("coinText").GetComponent<TextMeshProUGUI>().text = amount.ToString();
                buyDiamond(btn, amount, quntiy);
            }

            //Guards Tab layout
            globalVar.ExistingGuardList = roundGroupClass.Guards;
            guardBox = new GameObject[globalVar.ExistingGuardList.Count];
            ContentGuards = GameObject.Find("ContentGuards");
            GameObject guardsBoxContent = Resources.Load("Prefabs/shop/guardsBoxContent") as GameObject;
            for (int d = 0; d < globalVar.ExistingGuardList.Count; d++)
            {
                Guard guardClass = globalVar.ExistingGuardList[d];
                int g = d + 1;
                guardBox[d] =
                    GameObject.Instantiate(guardsBoxContent, ContentGuards.transform, worldPositionStays: false) as GameObject;
                guardImageSprite = guardBox[d].transform.Find("guardImage").gameObject;
                guardImageSprite.GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(guardClass.guardType.sprite) as Sprite;
                guardBox[d].transform.Find("GuardName").GetComponent<TextMeshProUGUI>().text =
                 d == 0 ? "Basic Barrack" :
                 d == 1 ? "Standard Barrack" :
                 d == 2 ? "Land Mines" : "";
                guardBox[d].transform.Find("GuardNumber").GetComponent<TextMeshProUGUI>().text = globalVar.ExistingGuardList[d].purchasedGuard.ToString();
                GameObject guardBuyButtonobject = guardBox[d].transform.Find("buyBtn").gameObject;
                Button guardBuyBtn = guardBuyButtonobject.GetComponent<Button>();
                guardBuyButtonobject.transform.Find("coinText").GetComponent<TextMeshProUGUI>().text = globalVar.ExistingGuardList[d].price.ToString();
                buyGuardEventListener(guardBuyBtn, d);

                GameObject guardSelectButtonobject = guardBox[d].transform.Find("selectBtn").gameObject;
                Button guardSelectBtn = guardSelectButtonobject.GetComponent<Button>();
                selectGuardEventListener(guardSelectBtn, d);
                guardSelectBtn.transform.Find("selecButtonText").gameObject.GetComponent<TextMeshProUGUI>().text = guardClass.selected ? "Selected" : "Select";
            }


            for (int h = 0; h < globalVar.uniqueWeaponsLists.Count; h++)
            {
                this.setUniqueSelectionSprite(h);
                this.setUniqueSelectionMagazineText(h);
            }
        }
        void buyGuardEventListener(Button btn, int index)
        {
            btn.onClick.AddListener(() => buyGuard(index));
        }

        void buyGuard(int index)
        {
            if (globalVar.totalDiamond == 0 || globalVar.totalDiamond < globalVar.ExistingGuardList[index].price) //800 is guard price
            {
                this.showLowDiamondMsg();
                return;
            }

            if (globalVar.ExistingGuardList[index] != null)
            {
                diamondTap.Play();
                globalVar.totalDiamond = globalVar.totalDiamond - globalVar.ExistingGuardList[index].price;
                globalVar.ExistingGuardList[index].purchasedGuard += 1;
                guardBox[index].transform.Find("GuardNumber").GetComponent<TextMeshProUGUI>().text = globalVar.ExistingGuardList[index].purchasedGuard.ToString();
                totalDiamondTxt.text = globalVar.totalDiamond.ToString();
            }
        }
        void selectGuardEventListener(Button btn, int index)
        {
            btn.onClick.AddListener(() => selectGuard(index));
        }
        void buyDiamond(Button btn, int amount, int qnty)
        {
            btn.onClick.AddListener(() =>
            {
                //Sound
                if (globalVar.totalCoin >= amount)
                {
                    buyAmmoAudio.Play();
                }
                if (globalVar.totalCoin >= amount)
                {
                    globalVar.totalDiamond = globalVar.totalDiamond + qnty;
                    globalVar.totalCoin = globalVar.totalCoin - amount;
                }
                else
                {
                    this.showLowCoinMsg();
                }
                totalDiamondTxt.text = globalVar.totalDiamond.ToString();
                totalCoinTxt.text = globalVar.totalCoin.ToString();
            });
        }
        void selectGuard(int index)
        {
            if (globalVar.ExistingGuardList.Count > 0)
            {
                selectBtnAudio.Play();
                for (int x = 0; x < globalVar.ExistingGuardList.Count; x++)
                {
                    if (x == index)
                    {
                        globalVar.ExistingGuardList[x].selected = true;
                        guardBox[x].transform.Find("selectBtn").transform.Find("selecButtonText").gameObject.GetComponent<TextMeshProUGUI>().text = "Selected";
                        guardBox[x].transform.Find("selectBtn").gameObject.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        globalVar.ExistingGuardList[x].selected = false;
                        guardBox[x].transform.Find("selectBtn").transform.Find("selecButtonText").gameObject.GetComponent<TextMeshProUGUI>().text = "Select";
                        guardBox[x].transform.Find("selectBtn").gameObject.GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
        void setUniqueWeaponTabUi(GameObject buyBtn, GameObject upgrBtn, GameObject levelSection)
        {
            buyBtn.SetActive(false);
            upgrBtn.SetActive(false);
            levelSection.SetActive(false);
        }

        void setGunLevel(GameObject gunContentsI, Weapon weapon)
        {
            levelIndicatorBox = gunContentsI.transform.Find("levelSection").gameObject;
            GameObject txt = levelIndicatorBox.transform.Find("lvlHeader").gameObject;
            txt.GetComponent<TextMeshProUGUI>().text = "Damage - " + weapon.level;
            GameObject lvl = levelIndicatorBox.gameObject.transform.Find("levelIndicatorBox").gameObject;
            lvl.transform.GetChild(0).gameObject.SetActive(false);
            lvl.transform.GetChild(1).gameObject.SetActive(false);
            lvl.transform.GetChild(2).gameObject.SetActive(false);
            lvl.transform.GetChild(3).gameObject.SetActive(false);
            if (weapon.level == 1)
            {
                lvl.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (weapon.level == 2)
            {
                lvl.transform.GetChild(0).gameObject.SetActive(true);
                lvl.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (weapon.level == 3)
            {
                lvl.transform.GetChild(0).gameObject.SetActive(true);
                lvl.transform.GetChild(1).gameObject.SetActive(true);
                lvl.transform.GetChild(2).gameObject.SetActive(true);
            }
            else if (weapon.level == 4)
            {
                lvl.transform.GetChild(0).gameObject.SetActive(true);
                lvl.transform.GetChild(1).gameObject.SetActive(true);
                lvl.transform.GetChild(2).gameObject.SetActive(true);
                lvl.transform.GetChild(3).gameObject.SetActive(true);
            }
        }

        void setUpgradeBtn(GameObject upgradeBtn, Weapon weapon)
        {
            GameObject upgradeBtnCoin = upgradeBtn.transform.Find("coin").gameObject;
            GameObject upgradeBtnText = upgradeBtn.transform.Find("coinText").gameObject;
            GameObject btnText = upgradeBtn.transform.Find("btnText").gameObject;
            GameObject upImg = upgradeBtn.transform.Find("upImg").gameObject;
            int price = weapon.upgradePrice * weapon.level;
            if (weapon.level == 4)
            {
                upgradeBtn.GetComponent<Button>().interactable = false;
                btnText.GetComponent<TextMeshProUGUI>().text = "Fully Upgraded";
                upgradeBtnText.SetActive(false);
                upgradeBtnCoin.SetActive(false);
                upImg.SetActive(false);
            }
            else
            {
                upgradeBtn.GetComponent<Button>().interactable = true;
                upgradeBtnText.SetActive(true);
                upgradeBtnText.GetComponent<TextMeshProUGUI>().text = price.ToString();
                upgradeBtnCoin.SetActive(true);
                upImg.SetActive(true);
                btnText.GetComponent<TextMeshProUGUI>().text = "Upgrade";
            }
        }

        void addEventListener(GameObject buttonObject, Weapon weapon, string buttonType, GameObject gunContentsI, int n)
        {
            Button btn = buttonObject.GetComponent<Button>();
            if (buttonType == "buy")
                btn.onClick.AddListener(() => buyMagazine(weapon, n));
            else if (buttonType == "select")
                btn.onClick.AddListener(() => selectGun(weapon, btn, n));
            else if (buttonType == "upgrade")
                btn.onClick.AddListener(() => upgradeGun(weapon, btn, gunContentsI));
        }

        void upgradeGun(Weapon weapon, Button btn, GameObject gunContentsI)
        {
            int price = weapon.upgradePrice * weapon.level;
            if (price > globalVar.totalDiamond)
            {
                this.showLowDiamondMsg();
            }
            else
            {
                upgradeAudio.Play();
                weapon.level = weapon.level + 1;
                GameObject upgradeBtn = btn.gameObject;
                this.setUpgradeBtn(upgradeBtn, weapon);
                this.setGunLevel(gunContentsI, weapon);
                globalVar.totalDiamond = globalVar.totalDiamond - price;
                totalDiamondTxt.text = globalVar.totalDiamond.ToString();
            }
        }

        void selectGun(Weapon weapon, Button btn, int indx)
        {
            if (weapon.category == "primary")
            {
                selectBtnAudio.Play();
                globalVar.bulletlimit = weapon.bulletPerMagazine;
                globalVar.primaryGunPower = weapon.power;
                globalVar.totalMagaZinePrimary = weapon.totalMagezine;
                globalVar.primaryGunSprite = weapon.gunSprite;
                globalVar.heroSpritePrimary = weapon.heroSprite;
                globalVar.primaryMagazineSprite = weapon.magazineSprite;
            }
            else if (weapon.category == "secondary")
            {
                selectBtnAudio.Play();
                globalVar.bulletlimitSecondery = weapon.bulletPerMagazine;
                globalVar.secondaryGunPower = weapon.power;
                globalVar.totalMagaZineSecondery = weapon.totalMagezine;
                globalVar.secondaryGunSprite = weapon.gunSprite;
                globalVar.heroSpriteSecondary = weapon.heroSprite;
                globalVar.secondaryMagazineSprite = weapon.magazineSprite;
            }
            else if (weapon.category == "special")
            {
                selectBtnAudio.Play();
                globalVar.bulletlimitSpecial = weapon.bulletPerMagazine;
                globalVar.specialGunPower = weapon.power;
                globalVar.totalMagaZineSpecial = weapon.totalMagezine;
                globalVar.specialGunSprite = weapon.gunSprite;
                globalVar.heroSpriteSpecial = weapon.heroSprite;
                globalVar.specialMagazineSprite = weapon.magazineSprite;
            }
            else if (weapon.category == "bomb")
            {
                selectBtnAudio.Play();
                globalVar.bulletlimitBomb = weapon.bulletPerMagazine;
                globalVar.bombGunPower = weapon.power;
                globalVar.totalMagaZineBomb = weapon.totalMagezine;
                globalVar.bombGunSprite = weapon.gunSprite;
                globalVar.heroSpriteBomb = weapon.heroSprite;
                globalVar.bombMagazineSprite = weapon.magazineSprite;
            }
            else if (weapon.category == "unique")
            {
                if (globalVar.totalCoin >= weapon.bulletPrice)
                {
                    weapon.totalMagezine++;
                    globalVar.totalCoin = globalVar.totalCoin - weapon.bulletPrice;
                    totalCoinTxt.text = globalVar.totalCoin.ToString();
                    if (weapon.name == "Atom")
                    {
                        globalVar.uniqueWeapon1BulletQuantity = weapon.totalMagezine;
                        globalVar.uniqueWeapon1GunSprite = weapon.gunSprite;
                        this.setUniqueSelectionSprite(0);
                        this.setUniqueSelectionMagazineText(0);
                    }
                    else if (weapon.name == "Artiliary")
                    {
                        globalVar.uniqueWeapon2BulletQuantity = weapon.totalMagezine;
                        globalVar.uniqueWeapon2GunSprite = weapon.gunSprite;
                        this.setUniqueSelectionSprite(1);
                        this.setUniqueSelectionMagazineText(1);
                    }
                    else if (weapon.name == "Thunder")
                    {
                        globalVar.uniqueWeapon3BulletQuantity = weapon.totalMagezine;
                        globalVar.uniqueWeapon3GunSprite = weapon.gunSprite;
                        this.setUniqueSelectionSprite(2);
                        this.setUniqueSelectionMagazineText(2);
                    }
                    if (!globalVar.uniqueWeaponsLists.Contains(weapon.name))
                    {
                        globalVar.uniqueWeaponsLists.Add(weapon.name);
                    }
                    //Max quantity reached condition code for UNIQuE WeaPon
                    if (weapon.totalMagezine == weapon.maxMagazine)
                    {
                        btn.interactable = false;
                        btn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Max Reached";
                        btn.transform.Find("coinText").gameObject.SetActive(false);
                    }
                    else
                    {
                        btn.transform.Find("coinText").gameObject.SetActive(true);

                    }
                    selectBtnAudio.Play();
                    //Game Save
                }
                else
                {
                    this.showLowCoinMsg();
                }
            }
            changeUIonSelect(btn, weapon);
        }

        void changeUIonSelect(Button btn, Weapon weapon)
        {
            int cnt = globalVar.WeaponListForShopPage.Count;
            for (int i = 0; i < cnt; i++)
            {
                if (globalVar.WeaponListForShopPage[i].category == "primary")
                {
                    changeUIinnerOnSelect(true, i, globalVar.WeaponListForShopPage[i]);
                    globalVar.WeaponListForShopPage[i].isSelect = false;
                    if (globalVar.WeaponListForShopPage[i].gunSprite == globalVar.primaryGunSprite)
                    {
                        globalVar.WeaponListForShopPage[i].isSelect = true;
                        changeUIinnerOnSelect(false, i, globalVar.WeaponListForShopPage[i]);
                        this.setSelectionMagazineText(0, globalVar.WeaponListForShopPage[i]);
                    }
                    this.setSelectionSprite(0);
                }
                else if (globalVar.WeaponListForShopPage[i].category == "secondary")
                {
                    changeUIinnerOnSelect(true, i, globalVar.WeaponListForShopPage[i]);
                    globalVar.WeaponListForShopPage[i].isSelect = false;
                    if (globalVar.WeaponListForShopPage[i].gunSprite == globalVar.secondaryGunSprite)
                    {
                        globalVar.WeaponListForShopPage[i].isSelect = true;
                        changeUIinnerOnSelect(false, i, globalVar.WeaponListForShopPage[i]);
                        this.setSelectionMagazineText(1, globalVar.WeaponListForShopPage[i]);
                    }
                    this.setSelectionSprite(1);
                }
                else if (globalVar.WeaponListForShopPage[i].category == "special")
                {
                    changeUIinnerOnSelect(true, i, globalVar.WeaponListForShopPage[i]);
                    globalVar.WeaponListForShopPage[i].isSelect = false;
                    if (globalVar.WeaponListForShopPage[i].gunSprite == globalVar.specialGunSprite)
                    {
                        globalVar.WeaponListForShopPage[i].isSelect = true;
                        changeUIinnerOnSelect(false, i, globalVar.WeaponListForShopPage[i]);
                        this.setSelectionMagazineText(2, globalVar.WeaponListForShopPage[i]);
                    }
                    this.setSelectionSprite(2);
                }
                else if (globalVar.WeaponListForShopPage[i].category == "bomb")
                {
                    changeUIinnerOnSelect(true, i, globalVar.WeaponListForShopPage[i]);
                    globalVar.WeaponListForShopPage[i].isSelect = false;
                    if (globalVar.WeaponListForShopPage[i].gunSprite == globalVar.bombGunSprite)
                    {
                        globalVar.WeaponListForShopPage[i].isSelect = true;
                        changeUIinnerOnSelect(false, i, globalVar.WeaponListForShopPage[i]);
                        this.setSelectionMagazineText(3, globalVar.WeaponListForShopPage[i]);
                    }
                    this.setSelectionSprite(3);
                }
            }
        }

        void changeUIinnerOnSelect(bool isNotSelect, int i, Weapon weapon)
        {
            if (isNotSelect)
            {
                selectBtn[i].GetComponent<Image>().sprite = selectActiveBtn;
                buyBtn[i].GetComponent<Button>().interactable = false;
                selectBtn[i].GetComponent<Button>().interactable = true;
                slectBtnTxt[i].GetComponent<TextMeshProUGUI>().text = "Select";
                //gunContentsI[i].GetComponent<Image>().sprite = selectInactiveGunBoxImg;
            }
            else
            {
                selectBtn[i].GetComponent<Image>().sprite = selectInActiveBtn;
                selectBtn[i].GetComponent<Button>().interactable = false;
                slectBtnTxt[i].GetComponent<TextMeshProUGUI>().text = "Selected";
                //gunContentsI[i].GetComponent<Image>().sprite = selectActiveGunBoxImg;
                // check Max magazine reached or not -- code
                if (weapon.totalMagezine == weapon.maxMagazine)
                {
                    buyBtn[i].GetComponent<Button>().interactable = false;
                    buyBtn[i].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Max Reached";
                }
                else
                {
                    buyBtn[i].GetComponent<Button>().interactable = true;
                }
            }
        }

        void buyMagazine(Weapon weapon, int indx)
        {
            //Sound
            if (globalVar.totalCoin >= weapon.bulletPrice)
            {
                buyAmmoAudio.Play();
            }
            if (weapon.category == "primary")
            {
                if (globalVar.totalCoin >= weapon.bulletPrice)
                {
                    weapon.totalMagezine++;
                    globalVar.totalMagaZinePrimary = weapon.totalMagezine;
                    globalVar.totalCoin = globalVar.totalCoin - weapon.bulletPrice;
                    this.setSelectionMagazineText(0, weapon);
                }
                else
                {
                    this.showLowCoinMsg();
                }
            }
            else if (weapon.category == "secondary")
            {
                if (globalVar.totalCoin >= weapon.bulletPrice)
                {
                    weapon.totalMagezine++;
                    globalVar.totalMagaZineSecondery = weapon.totalMagezine;
                    globalVar.totalCoin = globalVar.totalCoin - weapon.bulletPrice;
                    this.setSelectionMagazineText(1, weapon);
                }
                else
                {
                    this.showLowCoinMsg();
                }
            }
            else if (weapon.category == "special")
            {
                if (globalVar.totalCoin >= weapon.bulletPrice)
                {
                    weapon.totalMagezine++;
                    globalVar.totalMagaZineSpecial = weapon.totalMagezine;
                    globalVar.totalCoin = globalVar.totalCoin - weapon.bulletPrice;
                    this.setSelectionMagazineText(2, weapon);
                }
                else
                {
                    this.showLowCoinMsg();
                }
            }
            else if (weapon.category == "bomb")
            {
                if (globalVar.totalCoin >= weapon.bulletPrice)
                {
                    weapon.totalMagezine++;
                    globalVar.totalMagaZineBomb = weapon.totalMagezine;
                    globalVar.totalCoin = globalVar.totalCoin - weapon.bulletPrice;
                    this.setSelectionMagazineText(3, weapon);
                }
                else
                {
                    this.showLowCoinMsg();
                }
            }
            totalCoinTxt.text = globalVar.totalCoin.ToString();
            //Max reached condition code
            if (weapon.totalMagezine == weapon.maxMagazine)
            {
                buyBtn[indx].GetComponent<Button>().interactable = false;
                buyBtn[indx].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Max Reached";
            }
            //Game Save
        }

        void showLowCoinMsg()
        {
            coinErrorModal.Play();
            modalBack.SetActive(true);
            modalBackCoinText = modalBack.transform.Find("Panel").transform.Find("coinBack").transform.Find("totalCoin").gameObject;
            modalBackCoinText.GetComponent<TextMeshProUGUI>().text = globalVar.totalCoin.ToString();
        }

        void showLowDiamondMsg()
        {
            coinErrorModal.Play();
            modalBackDmnd.SetActive(true);
            modalBackDmndText = modalBackDmnd.transform.Find("Panel").transform.Find("diamondBack").transform.Find("totalDmnd").gameObject;
            modalBackDmndText.GetComponent<TextMeshProUGUI>().text = globalVar.totalDiamond.ToString();
        }

        void loadSprite()
        {
            activeTabBtn = Resources.Load<Sprite>("Sprites/UI/shop/activeBtn") as Sprite;
            inActiveTabBtn = Resources.Load<Sprite>("Sprites/UI/shop/inActiveBtn") as Sprite;
            selectInActiveBtn = Resources.Load<Sprite>("Sprites/UI/shop/activeselectBtn") as Sprite;
            selectActiveBtn = Resources.Load<Sprite>("Sprites/UI/shop/selectBtn") as Sprite;
            boxActiveImg = Resources.Load<Sprite>("Sprites/UI/shop/BgPanelBox5") as Sprite;
            boxInActiveImgPrimary = Resources.Load<Sprite>("Sprites/UI/shop/WeaponSmallBox") as Sprite;
            boxInActiveImgSec = Resources.Load<Sprite>("Sprites/UI/shop/selectedNotSec") as Sprite;
            boxInActiveImgSpc = Resources.Load<Sprite>("Sprites/UI/shop/selectedNotSpec") as Sprite;
            buyBtnImg = Resources.Load<Sprite>("Sprites/UI/shop/buyBtn") as Sprite;

            activeWeaponTabBtnSprite = Resources.Load<Sprite>("Sprites/UI/shop/Btn_Weapon_Active") as Sprite;
            inActiveWeaponTabBtnSprite = Resources.Load<Sprite>("Sprites/UI/shop/Btn_Weapon") as Sprite;
            activeDiamondTabBtnSprite = Resources.Load<Sprite>("Sprites/UI/shop/Btn_Store_Active") as Sprite;
            inActiveDiamondTabBtnSprite = Resources.Load<Sprite>("Sprites/UI/shop/Btn_Store") as Sprite;
            activeGuardsTabBtnSprite = Resources.Load<Sprite>("Sprites/UI/shop/Btn_Champ_Active") as Sprite;
            inActiveGuardsTabBtnSprite = Resources.Load<Sprite>("Sprites/UI/shop/Btn_Champ") as Sprite;

            // selectActiveGunBoxImg = Resources.Load<Sprite>("Sprites/UI/shop/gunBox_long_drk") as Sprite;
            // selectInactiveGunBoxImg = Resources.Load<Sprite>("Sprites/UI/shop/gunBox_long") as Sprite;

            selectActiveGunBoxImg = Resources.Load<Sprite>("Sprites/UI/shop/BgPanelBox5") as Sprite;
            selectInactiveGunBoxImg = Resources.Load<Sprite>("Sprites/UI/shop/BgPanelBox5") as Sprite;
        }

        // first layout of gun selection panel (onload secene)
        void generateGunSelectionLayout()
        {
            gunSelectionBox = Resources.Load("Prefabs/shop/gunSelection") as GameObject;
            gunSelectionBar = GameObject.Find("selectionPanel");
            gunSelectionBarBottom = GameObject.Find("selectionPanelBottom");
            removeMagzineBtn = new GameObject[7]; // *** as length = 4, and lengthBottom = 3, 4+3 = 7;
            int length = 4;
            Button[] clickToGoTab = new Button[length];
            selectedWeapon = new GameObject[length];
            for (int n = 0; n < length; n++)
            {
                selectedWeapon[n] = GameObject.Instantiate(gunSelectionBox, gunSelectionBar.transform, worldPositionStays: false);
                removeMagzineBtn[n] = selectedWeapon[n].transform.Find("removeMagazine").gameObject;
                clickToGoTab[n] = selectedWeapon[n].transform.Find("Button").gameObject.GetComponent<Button>();
                this.tabActiveFunction(clickToGoTab[n], n);
                this.setSelectionSprite(n);
                // this.setSelectionMagazineText(n, globalVar.WeaponListForShopPage[0]);
            }

            int lengthBottom = 3;
            selectedWeaponBottom = new GameObject[lengthBottom];
            Button[] clickToGoTabUnique = new Button[lengthBottom];
            for (int n = 0; n < lengthBottom; n++)
            {
                selectedWeaponBottom[n] = GameObject.Instantiate(gunSelectionBox, gunSelectionBarBottom.transform, worldPositionStays: false);
                removeMagzineBtn[n] = selectedWeaponBottom[n].transform.Find("removeMagazine").gameObject;
                clickToGoTabUnique[n] = selectedWeaponBottom[n].transform.Find("Button").gameObject.GetComponent<Button>();
                clickToGoTabUnique[n].onClick.AddListener(() =>
                {
                    this.setWeaponTabActive();
                    this.setuniqueTabActive();
                });
            }

        }

        void tabActiveFunction(Button btn, int indx)
        {
            if (indx == 0)
            {
                btn.onClick.AddListener(() =>
                {
                    this.setWeaponTabActive();
                    this.setPrimaryTabActive();
                });
            }
            else if (indx == 1)
            {
                btn.onClick.AddListener(() =>
                {
                    this.setWeaponTabActive();
                    this.setSecondaryTabActive();
                });
            }
            else if (indx == 2)
            {
                btn.onClick.AddListener(() =>
                {
                    this.setWeaponTabActive();
                    this.setSpecialTabActive();
                });
            }
            else if (indx == 3)
            {
                btn.onClick.AddListener(() =>
                {
                    this.setWeaponTabActive();
                    this.setBombTabActive();
                });
            }
        }
        void setSelectionSprite(int indx)
        {
            if (indx == 0)
            {
                selectedWeapon[0].transform.Find("gun").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(globalVar.primaryGunSprite) as Sprite;
            }
            else if (indx == 1)
            {
                selectedWeapon[1].transform.Find("gun").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(globalVar.secondaryGunSprite) as Sprite;
            }
            else if (indx == 2)
            {
                selectedWeapon[2].transform.Find("gun").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(globalVar.specialGunSprite) as Sprite;
            }
            else if (indx == 3)
            {
                selectedWeapon[3].transform.Find("gun").GetComponent<Image>().sprite =
            Resources.Load<Sprite>(globalVar.bombGunSprite) as Sprite;
            }
        }

        void setUniqueSelectionSprite(int indx)
        {
            if (indx == 0)
            {
                selectedWeaponBottom[0].transform.Find("gun").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(globalVar.uniqueWeapon1GunSprite) as Sprite;

            }
            else if (indx == 1)
            {
                selectedWeaponBottom[1].transform.Find("gun").GetComponent<Image>().sprite =
                        Resources.Load<Sprite>(globalVar.uniqueWeapon2GunSprite) as Sprite;
            }
            else if (indx == 2)
            {
                selectedWeaponBottom[2].transform.Find("gun").GetComponent<Image>().sprite =
                        Resources.Load<Sprite>(globalVar.uniqueWeapon3GunSprite) as Sprite;
            }
        }

        void setSelectionMagazineText(int indx, Weapon weapon)
        {
            if (indx == 0)
            {
                selectedWeapon[0].transform.Find("magazineText").GetComponent<TextMeshProUGUI>().text =
                    weapon.totalMagezine.ToString();
            }
            else if (indx == 1)
            {
                selectedWeapon[1].transform.Find("magazineText").GetComponent<TextMeshProUGUI>().text =
                   weapon.totalMagezine.ToString();
            }
            else if (indx == 2)
            {
                selectedWeapon[2].transform.Find("magazineText").GetComponent<TextMeshProUGUI>().text =
                     weapon.totalMagezine.ToString();
            }
            else if (indx == 3)
            {
                selectedWeapon[3].transform.Find("magazineText").GetComponent<TextMeshProUGUI>().text =
                  weapon.totalMagezine.ToString();
            }
        }
        void setUniqueSelectionMagazineText(int indx)
        {
            if (indx == 0)
            {
                selectedWeaponBottom[0].transform.Find("magazineText").GetComponent<TextMeshProUGUI>().text =
                    globalVar.uniqueWeapon1BulletQuantity.ToString();

            }
            else if (indx == 1)
            {
                selectedWeaponBottom[1].transform.Find("magazineText").GetComponent<TextMeshProUGUI>().text =
                    globalVar.uniqueWeapon2BulletQuantity.ToString();
            }
            else if (indx == 2)
            {
                selectedWeaponBottom[2].transform.Find("magazineText").GetComponent<TextMeshProUGUI>().text =
                    globalVar.uniqueWeapon3BulletQuantity.ToString();
            }
        }

        void setWeaponTabActive()
        {
            weaponTabContent.SetActive(true);
            guardsTabContent.SetActive(false);
            diamondTabContent.SetActive(false);
            weaponTabBtn.GetComponent<Image>().sprite = activeWeaponTabBtnSprite;
            diamondTabBtn.GetComponent<Image>().sprite = inActiveDiamondTabBtnSprite;
            guardsTabBtn.GetComponent<Image>().sprite = inActiveGuardsTabBtnSprite;
            tabAudio.Play();
        }
        void setGuardsTabActive()
        {
            weaponTabContent.SetActive(false);
            guardsTabContent.SetActive(true);
            diamondTabContent.SetActive(false);
            weaponTabBtn.GetComponent<Image>().sprite = inActiveWeaponTabBtnSprite;
            diamondTabBtn.GetComponent<Image>().sprite = inActiveDiamondTabBtnSprite;
            guardsTabBtn.GetComponent<Image>().sprite = activeGuardsTabBtnSprite;
        }
        void setDiamondTabActive()
        {
            weaponTabContent.SetActive(false);
            guardsTabContent.SetActive(false);
            diamondTabContent.SetActive(true);
            weaponTabBtn.GetComponent<Image>().sprite = inActiveWeaponTabBtnSprite;
            diamondTabBtn.GetComponent<Image>().sprite = activeDiamondTabBtnSprite;
            guardsTabBtn.GetComponent<Image>().sprite = inActiveGuardsTabBtnSprite;
            modalBackDmnd.SetActive(false);
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
            if (sceneSelectionStateTrigget)
            {
                // if (ads.isAdRuning == false)
                // {
                //     this.switchState();
                //     sceneSelectionStateTrigget = false;
                // }
                this.switchState();
                sceneSelectionStateTrigget = false;
            }
            if (homeStateTrigget)
            {
                // if (ads.isAdRuning == false)
                // {
                //     this.switchStateToHome();
                //     homeStateTrigget = false;
                // }
                this.switchStateToHome();
                homeStateTrigget = false;
            }
        }
        public void switchState()
        {
            savedRoundGroupClass.TotalCoins = globalVar.totalCoin;
            savedRoundGroupClass.TotalDiamond = globalVar.totalDiamond;
            savedRoundGroupClass.Guards = globalVar.ExistingGuardList;
            savedRoundGroupClass.AllWeapon = globalVar.WeaponListForShopPage;
            manager.saveCurrentGameState(savedRoundGroupClass);

            SceneManager.LoadScene("Loader");
            manager.switchState(new LoaderState(manager, globalVar, "RoundSelection"));

        }
        public void switchStateToHome()
        {
            savedRoundGroupClass.TotalCoins = globalVar.totalCoin;
            savedRoundGroupClass.TotalDiamond = globalVar.totalDiamond;
            savedRoundGroupClass.Guards = globalVar.ExistingGuardList;
            savedRoundGroupClass.AllWeapon = globalVar.WeaponListForShopPage;
            manager.saveCurrentGameState(savedRoundGroupClass);

            SceneManager.LoadScene("Loader");
            manager.switchState(new LoaderState(manager, globalVar, "Menu"));

        }
        public string getStateName()
        {
            return this.stateName;
        }
        public void action(string action)
        {

        }
        ~ShopState()
        {
        }
    }
}
