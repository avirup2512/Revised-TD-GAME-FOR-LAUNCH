using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Assets.Script.entity;
using Assets.Script.globalVar;
using UnityEngine.Events;
using TMPro;

namespace Assets.Script.state
{
    public class PlayState : GameState
    {
        private GameManager manager;
        public string stateName;

        public GameObject HeroObject;
        public GameObject PlayManagerObject;
        public PlayManager PlayManagerScript;
        public HeroEntity Hero;

        public GlobalData globalVar;

        private Camera cameraMain;

        private EnemyEntity Enemy;

        GameObject MenuButtonObject;
        Button MenuButton;

        GameObject ExitButtonObject;
        Button ExitButton;

        GameObject RestartButtonObject;
        Button RestartButton;

        GameObject ResumeButtonObject;
        Button ResumeButton;
        private float delay = 0;
        private float heroLifeDelay = 0;

        private bool Bool = false;

        private Toggle PrimaryGun;
        private Toggle SecondaryGun;
        private Toggle specialGun;
        private Toggle BombGun;
        private ToggleGroup GunSelection;
        GameObject[] UniqWeaponButtonObject;
        private Button UniqWeaponButton;
        private Button UniqWeaponButton2;
        private Button UniqWeaponButton3;

        public string selectedWeapon;
        public bool primarySelected;
        public bool secondarySelected;
        public bool specialSelected;
        public bool bombSelected;

        public bool isThunder = false;
        float timerThunder;

        GameObject Bullets;
        private GameObject shopBtn;
        private GameObject shopBtn2;

        public Round activeRound;
        public GameObject reloadingMsg;

        public GameObject CoinCollector;
        public GameObject bombObject;
        public float delayLoadMagaZine = 2;
        float timer;
        GameObject coinCollectionTextObject;
        RoundGroupClass savedRoundGroupClass;
        TextMeshProUGUI coinCollectionTextComponent;

        private GameObject DiamondText;

        public List<Guard> ExistingGuardList;
        public GameObject PauseCanvas;
        public GameObject tutorialText;
        PopUpEntity PauseModalPopUp;
        public GameObject AtomMagazine;
        public TextMeshProUGUI AtomMagazineText;
        public TextMeshProUGUI ArtiliaryMagazine;
        public TextMeshProUGUI ThunderMagazineText;

        AudioSource coinCollection;
        AudioSource EmptyGun;
        public AudioSource luncherBlast;

        // Tutorial Variable
        GameObject GunList;
        GameObject TutorialCanvas;
        GameObject Hand;
        GameObject DownMenu;
        GameObject TutorialNextButton;
        int tutorialCounter = 0;
        GameObject FirstText;
        GameObject SecondText;
        GameObject ThirdText;
        GameObject FourthText;
        GameObject DownMenuDuplicate;
        GameObject TopMenuDuplicate;
        GameObject gunListTutorial;
        GameObject HandSecondPosition;
        GameObject HandThirdPosition;
        GameObject HandFourthPosition;
        GameObject TopMenu;
        public PlayState(GameManager managerRef, GlobalData globalRef)
        {
            manager = managerRef;
            globalVar = globalRef;
            stateName = "attackState";
            Time.timeScale = 1;

            globalVar.isReloading = false;
            globalVar.loaded = false;
            globalVar.currentAlpha = 0;
            globalVar.instantiateEnemy = true;
            globalVar.heroIsHitting = false;
            globalVar.isPause = false;
            globalVar.enemyRemain = globalVar.totalIncomingEnemy;
            globalVar.heroLife = 100f;
            globalVar.bornEnemy.RemoveAll(deleteAll);
            globalVar.floorBossIntantiated = false;
            globalVar.floorBossDied = false;
            globalVar.isPauseModalOn = false;
            globalVar.isWon = false;
            activeRound = globalVar.activeRound;
            globalVar.currentIndexSpecial = 0;
            globalVar.currentIndexSecondary = 0;
            globalVar.currentIndex = 0;
        }

        public void Fire(UnityEngine.Vector2 screenPosition)
        {
            if (cameraMain == null)
            {
                cameraMain = Camera.main;
            }
            Bool = true;
            RaycastHit2D hit = Physics2D.Raycast(cameraMain.ScreenToWorldPoint(screenPosition), Vector2.zero);
            if (hit.collider != null && hit.transform.gameObject.transform.name == "Tutorial Canvas" || globalVar.isPauseModalOn)
            {
                return;
            }
            if (hit.collider != null && hit.transform.gameObject.transform.name == "Shooting Panel")
            {
                Collider2D[] affectedCoin = Physics2D.OverlapCircleAll(cameraMain.ScreenToWorldPoint(screenPosition), 0.5f);
                for (int i = 0; i < affectedCoin.Length; i++)
                {
                    GameObject touchedObject = affectedCoin[i].transform.gameObject;
                    if (touchedObject.tag == "Coin")
                    {
                        CoinCollection(touchedObject);
                        return;
                    }
                }
                if (primarySelected)
                {
                    primaryWeaponsFire(hit, screenPosition);
                }
                else if (secondarySelected)
                {
                    secondaryWeaponsFire(hit, screenPosition);
                }
                else if (specialSelected)
                {
                    specialWeaponsFire(hit, screenPosition);
                }
                else if (bombSelected)
                {
                    bombardMent(hit, screenPosition);
                }
            }

        }
        public void primaryWeaponsFire(RaycastHit2D hit, UnityEngine.Vector2 screenPosition)
        {
            // if magazine empty
            if (globalVar.PrimaryWeaponSelected.totalMagezine == 0 && globalVar.currentBulletIndex == 0)
            {
                if (Hero)
                {
                    Hero.FireStop();
                }
                EmptyGun.Play();
                PlayManagerScript.emptyBuletMsgShow();
                return;
            }
            //on Reload time fire not happen
            if (globalVar.isReloading)
            {
                if (Hero)
                {
                    Hero.FireStop();
                }
                EmptyGun.Play();
                return;
            }
            // Collider2D[] affectedEnemy = Physics2D.OverlapCircleAll(cameraMain.ScreenToWorldPoint(screenPosition), 1.00f);
            // for (int i = 0; i < affectedEnemy.Length; i++)
            // {
            //     GameObject touchedObject = affectedEnemy[i].transform.gameObject;
            //     if (touchedObject.tag == "Enemy")
            //     {
            //         Enemy = touchedObject.GetComponent<EnemyEntity>();
            //         if (Enemy)
            //         {
            //             Enemy.GetHit(10);
            //         }
            //     }
            // }

            if ((hit.collider != null && hit.transform.gameObject.transform.name != "TopMenu") || !hit.collider)
            {

                PlayManagerScript.fire(globalVar.selectedWeapon, screenPosition);
                Hero.fire("Primary");
                globalVar.currentIndex++;
                globalVar.currentBulletIndex--;
            }

            if (globalVar.currentIndex == globalVar.PrimaryWeaponSelected.bulletPerMagazine || globalVar.currentBulletIndex == 0)
            {
                globalVar.currentIndex = 0;
                if (globalVar.PrimaryWeaponSelected.totalMagezine != 0)
                {
                    globalVar.currentBulletIndex = globalVar.PrimaryWeaponSelected.bulletPerMagazine;
                    globalVar.isReloading = true;
                    globalVar.PrimaryWeaponSelected.totalMagezine--;
                }
                PlayManagerScript.setBulletZero();
            }
        }

        public void bombardMent(RaycastHit2D hit, UnityEngine.Vector2 screenPosition)
        {
            if (globalVar.BombWeaponSelected.totalMagezine == 0)
            {
                EmptyGun.Play();
                return;
            }
            GameObject bomb = GameObject.Instantiate(bombObject, HeroObject.transform);
            bomb.transform.SetParent(HeroObject.transform);
            BombEntity bombEntity = bomb.GetComponent<BombEntity>();
            bombEntity.globalVar = globalVar;
            bombEntity.sourceObject = HeroObject;
            bombEntity.targetPosition = cameraMain.ScreenToWorldPoint(screenPosition);
            bombEntity.luncherBlast = luncherBlast;
            globalVar.BombWeaponSelected.totalMagezine--;
            PlayManagerScript.fire("Bomb", new Vector2(0, 0));
        }
        public void secondaryWeaponsFire(RaycastHit2D hit, UnityEngine.Vector2 screenPosition)
        {
            // if magazine empty
            if (globalVar.SecondaryWeaponSelected.totalMagezine == 0 && globalVar.currentBulletIndexSecondery == 0)
            {
                if (Hero)
                {
                    Hero.FireStop();
                }
                EmptyGun.Play();
                PlayManagerScript.emptyBuletMsgShow();
                return;
            }
            //on Reload time fire not happen
            if (globalVar.isReloading)
            {
                if (Hero)
                {
                    Hero.FireStop();
                }
                EmptyGun.Play();
                return;
            }
            if (globalVar.currentIndexSecondary < globalVar.SecondaryWeaponSelected.bulletPerMagazine && hit.collider != null
                && hit.transform.gameObject.transform.name != "TopMenu" || !hit.collider)
            {
                Hero.fire("Secondary");
                if (hit.collider != null && hit.transform.gameObject.tag == "Enemy")
                {
                    GameObject touchedObject = hit.transform.gameObject;
                    //IgnoreRaycast(touchedObject);
                    int LayerDefault = LayerMask.NameToLayer("Default");
                    touchedObject.layer = LayerDefault;
                    Vector3 ScrennCoordinates = new Vector3(screenPosition.x, screenPosition.y, cameraMain.nearClipPlane);
                    Vector3 WorldCoordinates = cameraMain.ScreenToWorldPoint(ScrennCoordinates);
                    WorldCoordinates.z = 0;
                    // RaycastHit2D HitObject = Physics2D.Raycast(Hero.FirePoint.transform.position, WorldCoordinates - Hero.FirePoint.transform.position);
                    // // Debug.DrawRay(Hero.FirePoint.transform.position, Hero.FirePoint.transform.position - WorldCoordinates, Color.green);
                    // if (HitObject.collider != null)
                    // {
                    //     GameObject touchedObject_2 = HitObject.transform.gameObject;
                    //     if (touchedObject_2.tag == "Enemy")
                    //     {

                    //         Enemy = touchedObject_2.GetComponent<EnemyEntity>();
                    //         if (Enemy)
                    //         {
                    //             Enemy.GetHit(10);
                    //         }
                    //     }
                    // }
                }
            }

            if ((hit.collider != null && hit.transform.gameObject.transform.name != "TopMenu") || !hit.collider)
            {

                PlayManagerScript.fire(globalVar.selectedWeapon, screenPosition);
                globalVar.currentIndexSecondary++;
                globalVar.currentBulletIndexSecondery--;
            }

            if (globalVar.currentIndexSecondary == globalVar.SecondaryWeaponSelected.bulletPerMagazine ||
                globalVar.currentBulletIndexSecondery == 0)
            {
                globalVar.currentIndexSecondary = 0;
                if (globalVar.SecondaryWeaponSelected.totalMagezine != 0)
                {
                    globalVar.currentBulletIndexSecondery = globalVar.SecondaryWeaponSelected.bulletPerMagazine;
                    globalVar.isReloading = true;
                    globalVar.SecondaryWeaponSelected.totalMagezine--;
                }
                PlayManagerScript.setBulletZero();
            }
        }
        public void specialWeaponsFire(RaycastHit2D hit, UnityEngine.Vector2 screenPosition)
        {
            // if magazine empty
            if (globalVar.SpecialWeaponSelected.totalMagezine == 0 && globalVar.currentBulletIndexSpecial == 0)
            {
                if (Hero)
                {
                    Hero.FireStop();
                }
                EmptyGun.Play();
                PlayManagerScript.emptyBuletMsgShow();
                return;
            }
            //on Reload time fire not happen
            if (globalVar.isReloading)
            {
                if (Hero)
                {
                    Hero.FireStop();
                }
                EmptyGun.Play();
                return;
            }
            Hero.fire("Special");

            if ((hit.collider != null && hit.transform.gameObject.transform.name != "TopMenu") || !hit.collider)
            {

                PlayManagerScript.fire(globalVar.selectedWeapon, screenPosition);
                globalVar.currentIndexSpecial++;
                globalVar.currentBulletIndexSpecial--;
            }

            if (globalVar.currentIndexSpecial == globalVar.SpecialWeaponSelected.bulletPerMagazine ||
                globalVar.currentBulletIndexSpecial == 0)
            {
                globalVar.currentIndexSpecial = 0;
                if (globalVar.SpecialWeaponSelected.totalMagezine != 0)
                {
                    globalVar.currentBulletIndexSpecial = globalVar.SpecialWeaponSelected.bulletPerMagazine;
                    globalVar.isReloading = true;
                    globalVar.SpecialWeaponSelected.totalMagezine--;
                }
                PlayManagerScript.setBulletZero();
            }
        }
        public void CoinCollection(GameObject coin)
        {
            coinCollection.Play();
            CoinEntity coinEntity = coin.GetComponent<CoinEntity>();
            coinEntity.activeRoundNumber = activeRound.roundGroupNumber;
            coinEntity.init();
            coinEntity.go(CoinCollector.transform.position);
            int coinss = 100 * (activeRound.roundGroupNumber);
            globalVar.totalCoin += coinss;
            coinCollectionTextComponent.text = globalVar.totalCoin.ToString();
            savedRoundGroupClass.TotalCoins = globalVar.totalCoin;
            if (!savedRoundGroupClass.firstTimeGamePlaying){
                tutorialText.SetActive(false);
            }
        }
        // void setWeaponsSprite()
        // {
        //     primarySprite = GameObject.Find("primaryGunSprite");
        //     primarySprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalVar.primaryGunSprite) as Sprite;

        //     secondarySprite = GameObject.Find("secondaryGunSprite");
        //     secondarySprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalVar.secondaryGunSprite) as Sprite;

        //     specialSprite = GameObject.Find("specialGunSprite");
        //     specialSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalVar.specialGunSprite) as Sprite;

        // }
        public void TouchRelease(UnityEngine.Vector2 screenPosition)
        {
            FollowRaycast();
            if (Hero)
            {
                Hero.FireStop();
            }
        }

        public void IgnoreRaycast(GameObject touchedObject)
        {
            foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
            {
                if (gameObj.name == "Enemy(Clone)" && !GameObject.ReferenceEquals(gameObj, touchedObject))
                {
                    int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
                    gameObj.layer = LayerIgnoreRaycast;
                }
            }
        }
        public void FollowRaycast()
        {
            foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
            {
                if (gameObj.name == "Enemy(Clone)")
                {
                    int LayerDefault = LayerMask.NameToLayer("Default");
                    gameObj.layer = LayerDefault;
                }
            }
        }

        void OnEnable()
        {

        }

        private static bool deleteAll(string i)
        {
            return (i != null);
        }

        void pause()
        {

        }

        void resume()
        {

        }
        void openPauseModal()
        {
            manager.saveCurrentGameState(savedRoundGroupClass);
            PauseModalPopUp.pop();
            globalVar.isPauseModalOn = true;
            // Time.timeScale = 0;
        }
        void closePauseModal()
        {
            globalVar.isPauseModalOn = false;
            PauseCanvas.SetActive(false);
            Time.timeScale = 1;
        }
        void gotoMenu()
        {
            DetachedEvent();
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "Menu"));
        }


        public void getData()
        {
            GunList = GameObject.Find("Gun List");
            Hand = GameObject.Find("Hand");
            TutorialCanvas = GameObject.Find("Tutorial Canvas");
            DownMenu = GameObject.Find("Down Menu");
            TopMenu = GameObject.Find("TopMenu");
            TutorialNextButton = GameObject.Find("TutorialNextButton");
            HandSecondPosition = GameObject.Find("HandSecondPosition");
            HandThirdPosition = GameObject.Find("HandThirdPosition");
            HandFourthPosition = GameObject.Find("HandFourthPosition");

            reloadingMsg = GameObject.Find("reloadingMsg");
            reloadingMsg.SetActive(false);

            tutorialText = GameObject.Find("tutorialText");
            tutorialText.SetActive(false);

            FirstText = GameObject.Find("First Text");
            FirstText.SetActive(false);
            SecondText = GameObject.Find("Second Text");
            SecondText.SetActive(false);
            ThirdText = GameObject.Find("Third Text");
            ThirdText.SetActive(false);
            FourthText = GameObject.Find("Fourth Text");
            FourthText.SetActive(false);

            Button tutoriakBtn = TutorialNextButton.GetComponent<Button>();
            tutoriakBtn.onClick.AddListener(nextTuTorial);
            TutorialCanvas.SetActive(false);

            shopBtn = GameObject.Find("shopBtn");
            Button shopBtnComp = shopBtn.GetComponent<Button>();
            shopBtnComp.onClick.AddListener(this.gotoShop);

            shopBtn2 = GameObject.Find("shopBtn2");
            Button shopBtnComp2 = shopBtn2.GetComponent<Button>();
            shopBtnComp2.onClick.AddListener(this.gotoShop);

            DiamondText = GameObject.Find("DiamondText");
            DiamondText.GetComponent<TextMeshProUGUI>().text = globalVar.totalDiamond.ToString();
            UniqWeaponButton2 = GameObject.Find("ArtiliaryButton").GetComponent<Button>();
            luncherBlast = GameObject.Find("luncherBlast").GetComponent<AudioSource>();
            coinCollection = GameObject.Find("coinCollection").GetComponent<AudioSource>();
            EmptyGun = GameObject.Find("EmptyGun").GetComponent<AudioSource>();

            savedRoundGroupClass = manager.getSavedGameState();
            if (savedRoundGroupClass != null)
            {
                globalVar.totalCoin = savedRoundGroupClass.TotalCoins;
                globalVar.totalCoin = savedRoundGroupClass.TotalCoins;
                globalVar.totalDiamond = savedRoundGroupClass.TotalDiamond;
                ExistingGuardList = globalVar.ExistingGuardList = savedRoundGroupClass.Guards;
                globalVar.WeaponListForShopPage = savedRoundGroupClass.AllWeapon;
                setWeaponsDataFromStore(savedRoundGroupClass.AllWeapon);
                globalVar.currentBulletIndex = (globalVar.PrimaryWeaponSelected != null && globalVar.PrimaryWeaponSelected.totalMagezine > 0) ? globalVar.PrimaryWeaponSelected.bulletPerMagazine : 0;
                globalVar.currentBulletIndexSecondery = (globalVar.SecondaryWeaponSelected != null && globalVar.SecondaryWeaponSelected.totalMagezine > 0) ?
                                                        globalVar.SecondaryWeaponSelected.bulletPerMagazine : 0;
                globalVar.currentBulletIndexSpecial = (globalVar.SpecialWeaponSelected != null && globalVar.SpecialWeaponSelected.totalMagezine > 0) ?
                                                        globalVar.SpecialWeaponSelected.bulletPerMagazine : 0;
            }
            AtomMagazine = GameObject.Find("AtomBombButton").transform.Find("MagazineHolder").transform.Find("AtomMagazine").gameObject;
            AtomMagazineText = AtomMagazine.GetComponent<TextMeshProUGUI>();
            ArtiliaryMagazine = GameObject.Find("ArtiliaryButton").transform.Find("MagazineHolder").transform.Find("ArtiliaryMagazine").GetComponent<TextMeshProUGUI>();
            ThunderMagazineText = GameObject.Find("ThunderButton").transform.Find("MagazineHolder").transform.Find("thunderMagazine").GetComponent<TextMeshProUGUI>();
            UniqWeaponButtonObject = GameObject.FindGameObjectsWithTag("UniqWeaponButton");
            if (savedRoundGroupClass.AllWeapon.Count > 0)
            {
                for (int a = 0; a < savedRoundGroupClass.AllWeapon.Count; a++)
                {
                    for (int x = 0; x < UniqWeaponButtonObject.Length; x++)
                    {
                        if (savedRoundGroupClass.AllWeapon[a].category == "unique")
                        {
                            if (UniqWeaponButtonObject[x].name == "AtomBombButton" && savedRoundGroupClass.AllWeapon[a].name == "Atom")
                            {
                                globalVar.AtomBombWeaponSelected = savedRoundGroupClass.AllWeapon[a];
                                UniqWeaponButton = UniqWeaponButtonObject[x].GetComponent<Button>();
                                UniqWeaponButton.onClick.AddListener(delegate
                                {
                                    KABOOM(PlayManagerScript);
                                });

                                AtomMagazineText.text = savedRoundGroupClass.AllWeapon[a].totalMagezine.ToString();
                                if (savedRoundGroupClass.AllWeapon[a].totalMagezine == 0)
                                {
                                    UniqWeaponButton.interactable = false;
                                }
                                else
                                {
                                    UniqWeaponButton.interactable = true;
                                }
                            }
                            if (UniqWeaponButtonObject[x].name == "ArtiliaryButton" && savedRoundGroupClass.AllWeapon[a].name == "Artiliary")
                            {
                                globalVar.ArtiliaryWeaponSelected = savedRoundGroupClass.AllWeapon[a];
                                UniqWeaponButton = UniqWeaponButtonObject[x].GetComponent<Button>();
                                UniqWeaponButton.onClick.AddListener(delegate
                                {
                                    ATRANDAS(PlayManagerScript, UniqWeaponButton);
                                });

                                ArtiliaryMagazine.text = savedRoundGroupClass.AllWeapon[a].totalMagezine.ToString();
                                if (savedRoundGroupClass.AllWeapon[a].totalMagezine == 0)
                                {
                                    UniqWeaponButton.interactable = false;
                                }
                                else
                                {
                                    UniqWeaponButton.interactable = true;
                                }
                            }
                            if (UniqWeaponButtonObject[x].name == "ThunderButton" && savedRoundGroupClass.AllWeapon[a].name == "Thunder")
                            {
                                globalVar.ThunderWeaponSelected = savedRoundGroupClass.AllWeapon[a];
                                UniqWeaponButton = UniqWeaponButtonObject[x].GetComponent<Button>();
                                UniqWeaponButton.onClick.AddListener(delegate
                                {
                                    isThunder = true;
                                    Gurum(PlayManagerScript);
                                });
                                ThunderMagazineText.text = savedRoundGroupClass.AllWeapon[a].totalMagezine.ToString();
                                if (savedRoundGroupClass.AllWeapon[a].totalMagezine == 0)
                                {
                                    UniqWeaponButton.interactable = false;
                                }
                                else
                                {
                                    UniqWeaponButton.interactable = true;
                                }
                            }
                        }
                    }
                }
            }
            cameraMain = GameObject.Find("Main Camera").GetComponent<Camera>();
            PauseCanvas = GameObject.Find("Pause Canvas");
            PauseModalPopUp = PauseCanvas.GetComponent<PopUpEntity>();
            ExitButtonObject = GameObject.Find("Exit Button");
            if (ExitButtonObject)
            {
                ExitButton = ExitButtonObject.GetComponent<Button>();
                ExitButton.onClick.AddListener(this.gotoMenu);
            }
            ResumeButtonObject = GameObject.Find("Resume Button");
            if (ResumeButtonObject)
            {
                ResumeButton = ResumeButtonObject.GetComponent<Button>();
                ResumeButton.onClick.AddListener(this.closePauseModal);
            }
            RestartButtonObject = GameObject.Find("Restart Button");
            if (RestartButtonObject)
            {
                RestartButton = RestartButtonObject.GetComponent<Button>();
                RestartButton.onClick.AddListener(delegate { this.gotoPlay(activeRound); });
            }
            PauseCanvas.SetActive(false);
            string folder = globalVar.BombWeaponSelected.name;
            bombObject = Resources.Load("Prefabs/bomb/" + folder + "/bomb") as GameObject;
            CoinCollector = GameObject.Find("CoinBag");
            coinCollectionTextObject = GameObject.Find("coinCollectionText");
            if (coinCollectionTextObject)
            {
                coinCollectionTextComponent = coinCollectionTextObject.GetComponent<TextMeshProUGUI>();
                coinCollectionTextComponent.text = globalVar.totalCoin.ToString();
            }
            MenuButtonObject = GameObject.Find("MenuButton");
            if (MenuButtonObject)
            {
                MenuButton = MenuButtonObject.GetComponent<Button>();
                MenuButton.onClick.AddListener(this.openPauseModal);
            }
            HeroObject = GameObject.Find("Hero");
            if (HeroObject)
            {
                Hero = HeroObject.GetComponent<HeroEntity>();
                Hero.GlobalVar = globalVar;
                manager.OnStartTouch += Fire;
                manager.OnEndTouch += TouchRelease;

                PlayManagerObject = GameObject.Find("PlayManager");
                if (PlayManagerObject)
                {
                    PlayManagerScript = PlayManagerObject.GetComponent<PlayManager>();
                    PlayManagerScript.globalVar = globalVar;
                    PlayManagerScript.enemyRemain = globalVar.totalIncomingEnemy;
                    PlayManagerScript.HeroAfterLoad = HeroObject;
                    PlayManagerScript.Hero = Hero;
                    PlayManagerScript.activeRound = activeRound;
                    PlayManagerScript.cameraMain = cameraMain;
                    PlayManagerScript.ExistingGuardList = ExistingGuardList;
                    PlayManagerScript.savedRoundGroupClass = savedRoundGroupClass;
                    PlayManagerScript.coinCollectionTextComponent = coinCollectionTextComponent;
                    PlayManagerScript.ArtialiaryUniqButton = UniqWeaponButton2;

                    PlayManagerScript.manager = manager;
                    PlayManagerScript.instantiateEnemy();

                    // ADD GUARD IF ANY GUARD IS SELECTED
                    if (globalVar.ExistingGuardList.Count > 0)
                    {
                        for (int x = 0; x < globalVar.ExistingGuardList.Count; x++)
                        {
                            if (globalVar.ExistingGuardList[x].selected)
                            {
                                AddGuard(PlayManagerScript, globalVar.ExistingGuardList[x]);
                            }
                        }
                    }
                }
                PrimaryGun = GameObject.Find("PrimaryGun").GetComponent<Toggle>();
                SecondaryGun = GameObject.Find("SecondaryGun").GetComponent<Toggle>();
                specialGun = GameObject.Find("Special Gun").GetComponent<Toggle>();
                BombGun = GameObject.Find("bomb").GetComponent<Toggle>();
                if (PrimaryGun)
                {
                    PrimaryGun.onValueChanged.AddListener(delegate
                    {
                        ToggleGuns(PrimaryGun);
                    });
                }
                if (SecondaryGun)
                {
                    SecondaryGun.onValueChanged.AddListener(delegate
                    {
                        ToggleGuns(SecondaryGun);
                    });

                }
                if (specialGun)
                {
                    specialGun.onValueChanged.AddListener(delegate
                    {
                        ToggleGuns(specialGun);
                    });
                }
                if (BombGun)
                {
                    BombGun.onValueChanged.AddListener(delegate
                    {
                        ToggleGuns(BombGun);
                    });
                }
                PrimaryGun.enabled = true;
                primarySelected = true;
                secondarySelected = false;
                specialSelected = false;
                bombSelected = false;
                globalVar.selectedWeapon = "Primary";
                PlayManagerScript.GunSpriteChange("Primary");
            }
            if (savedRoundGroupClass.firstTimeGamePlaying){
                StartTutorial();
            }
               
        }
        void StartTutorial()
        {
            Time.timeScale = 0;
            TutorialCanvas.SetActive(true);
            gunListTutorial = GameObject.Instantiate(GunList, TutorialCanvas.transform);
            FirstText.SetActive(true);
        }
        void nextTuTorial()
        {
            if (tutorialCounter == 0)
            {
                FirstText.SetActive(false);
                SecondText.SetActive(true);
                gunListTutorial.SetActive(false);
                DownMenuDuplicate = GameObject.Instantiate(DownMenu, TutorialCanvas.transform);
                Hand.transform.position = HandSecondPosition.transform.position;
                Hand.transform.rotation = HandSecondPosition.transform.rotation;
            }
            else if (tutorialCounter == 1)
            {
                FirstText.SetActive(false);
                SecondText.SetActive(false);
                ThirdText.SetActive(true);
                TopMenuDuplicate = GameObject.Instantiate(TopMenu, TutorialCanvas.transform);
                if (DownMenuDuplicate != null)
                    DownMenuDuplicate.SetActive(false);
                Hand.transform.position = HandThirdPosition.transform.position;
                Hand.transform.rotation = HandThirdPosition.transform.rotation;
            }
            else if (tutorialCounter == 2)
            {
                FirstText.SetActive(false);
                SecondText.SetActive(false);
                ThirdText.SetActive(false);
                FourthText.SetActive(true);
                Hand.transform.position = HandFourthPosition.transform.position;
            }
            else {
                Time.timeScale = 1;
                TutorialCanvas.SetActive(false);
                savedRoundGroupClass.firstTimeGamePlaying = false;
                manager.saveCurrentGameState(savedRoundGroupClass);
                tutorialText.SetActive(true);
                return;
            }
            tutorialCounter++;
        }
        public void setWeaponsDataFromStore(List<Weapon> weaponList)
        {
            int totalWeaponCount = weaponList.Count;
            if (totalWeaponCount != 0)
            {
                globalVar.WeaponListForShopPage = weaponList;
            }
            for (int ind = 0; ind < totalWeaponCount; ind++)
            {
                if (globalVar.WeaponListForShopPage[ind].category == "primary")
                {
                    if (globalVar.WeaponListForShopPage[ind].isSelect)
                    {
                        globalVar.PrimaryWeaponSelected = globalVar.WeaponListForShopPage[ind];
                    }
                    else if (globalVar.WeaponListForShopPage[ind].name == "primary1")
                    {
                        globalVar.PrimaryWeaponSelected = globalVar.WeaponListForShopPage[ind];
                    }
                }
                else if (globalVar.WeaponListForShopPage[ind].category == "secondary")
                {
                    if (globalVar.WeaponListForShopPage[ind].isSelect)
                    {
                        globalVar.SecondaryWeaponSelected = globalVar.WeaponListForShopPage[ind];
                    }
                    else if (globalVar.WeaponListForShopPage[ind].name == "secondary1")
                    {
                        globalVar.SecondaryWeaponSelected = globalVar.WeaponListForShopPage[ind];
                    }
                }
                else if (globalVar.WeaponListForShopPage[ind].category == "special")
                {
                    if (globalVar.WeaponListForShopPage[ind].isSelect)
                    {
                        globalVar.SpecialWeaponSelected = globalVar.WeaponListForShopPage[ind];
                    }
                    else if (globalVar.WeaponListForShopPage[ind].name == "special1")
                    {
                        globalVar.SpecialWeaponSelected = globalVar.WeaponListForShopPage[ind];
                    }

                }
                else if (globalVar.WeaponListForShopPage[ind].category == "bomb")
                {
                    if (globalVar.WeaponListForShopPage[ind].isSelect)
                    {
                        globalVar.BombWeaponSelected = globalVar.WeaponListForShopPage[ind];
                    }
                    else if (globalVar.WeaponListForShopPage[ind].name == "bomb1")
                    {
                        globalVar.BombWeaponSelected = globalVar.WeaponListForShopPage[ind];
                    }
                }
            }
        }
        void gotoPlay(Round round)
        {
            Time.timeScale = 1;
            globalVar.setActiveRound(round);
            globalVar.setTotalIncomingEnemy(round.totalIncomingEnemy);
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "Play"));
        }
        void gotoShop()
        {
            manager.saveCurrentGameState(savedRoundGroupClass);
            DetachedEvent();
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "Shop"));
        }
        void AddGuard(PlayManager player, Guard SelectedGuard)
        {
            player.AddGuard(SelectedGuard);
        }
        void KABOOM(PlayManager player)
        {
            if (globalVar.AtomBombWeaponSelected.totalMagezine == 0)
            {
                return;
            }
            player.AtomBombing();
            globalVar.AtomBombWeaponSelected.totalMagezine--;
            AtomMagazineText.text = globalVar.AtomBombWeaponSelected.totalMagezine.ToString();
        }
        void ATRANDAS(PlayManager player, Button btn)
        {
            if (globalVar.ArtiliaryWeaponSelected.totalMagezine == 0)
            {
                return;
            }
            player.ArtiliaryFireClick(btn);
            globalVar.ArtiliaryWeaponSelected.totalMagezine--;
            ArtiliaryMagazine.text = globalVar.ArtiliaryWeaponSelected.totalMagezine.ToString();
        }
        void Gurum(PlayManager player)
        {
            if (globalVar.ThunderWeaponSelected.totalMagezine == 0)
            {
                return;
            }
            //player.ArtiliaryFireClick(btn);
            player.Gurum(true);
            globalVar.ThunderWeaponSelected.totalMagezine--;
            ThunderMagazineText.text = globalVar.ThunderWeaponSelected.totalMagezine.ToString();
        }



        void ToggleGuns(Toggle toggleData)
        {
            if (toggleData.name == "PrimaryGun")
            {
                delayLoadMagaZine = globalVar.PrimaryWeaponSelected.magazineDelay;
                if (toggleData.isOn)
                {
                    primarySelected = true;
                    secondarySelected = false;
                    specialSelected = false;
                    bombSelected = false;
                    globalVar.selectedWeapon = "Primary";
                }
                else
                {
                    primarySelected = false;
                }
                PlayManagerScript.GunSpriteChange("Primary");
            }
            else if (toggleData.name == "SecondaryGun")
            {
                delayLoadMagaZine = globalVar.SecondaryWeaponSelected.magazineDelay;
                if (toggleData.isOn)
                {
                    secondarySelected = true;
                    primarySelected = false;
                    specialSelected = false;
                    bombSelected = false;
                    globalVar.selectedWeapon = "Secondary";
                }
                else
                {
                    secondarySelected = false;
                }
                PlayManagerScript.GunSpriteChange("Secondary");
            }
            else if (toggleData.name == "Special Gun")
            {
                delayLoadMagaZine = globalVar.SpecialWeaponSelected.magazineDelay;
                if (toggleData.isOn)
                {
                    specialSelected = true;
                    primarySelected = false;
                    secondarySelected = false;
                    bombSelected = false;
                    globalVar.selectedWeapon = "Special";
                }
                else
                {
                    specialSelected = false;
                }
                PlayManagerScript.GunSpriteChange("Special");
            }
            else if (toggleData.name == "bomb")
            {
                if (toggleData.isOn)
                {
                    specialSelected = false;
                    primarySelected = false;
                    secondarySelected = false;
                    bombSelected = true;
                    globalVar.selectedWeapon = "bomb";
                }
                else
                {
                    specialSelected = false;
                }
            }
            if (!primarySelected && !secondarySelected && !specialSelected && !bombSelected)
            {
                primarySelected = true;
                PrimaryGun.enabled = true;
            }
            PlayManagerScript.showHideBulletsOnGunSelection(globalVar.selectedWeapon);
        }

        void decorateRound()
        {


        }
        void heroForward()
        {

        }
        void heroBackward()
        {

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
            if (globalVar.isWon && globalVar.bornEnemy.Count == 0)
            {
                delay += Time.deltaTime;
                if (delay * 10 > 30)
                {
                    wonTheRound();
                }
            }
            if (globalVar.heroLife <= 0)
            {
                Time.timeScale = 0;
                lostRound();
            }
            if (globalVar.isReloading)
            {
                reloadingMsg.SetActive(true);
                timer += Time.deltaTime;
                if (timer > delayLoadMagaZine)
                {
                    PlayManagerScript.ReloadMagazine(globalVar.selectedWeapon);
                    timer = 0;
                    reloadingMsg.SetActive(false);
                }
            }
            if (isThunder)
            {
                timerThunder += Time.deltaTime;
                if (timerThunder > 1f)
                {
                    PlayManagerScript.Gurum(false);
                    timerThunder = 0;
                }
            }
            if (PrimaryGun)
                PrimaryGun.interactable = !globalVar.isReloading;
            if (SecondaryGun)
                SecondaryGun.interactable = !globalVar.isReloading;
            if (specialGun)
                specialGun.interactable = !globalVar.isReloading;
        }

        void wonTheRound()
        {
            DetachedEvent();
            globalVar.isWon = false;
            SceneManager.LoadScene("Win");
            manager.switchState(new WonState(manager, globalVar));
            // yield return null;
        }

        void lostRound()
        {
            DetachedEvent();
            SceneManager.LoadScene("Lost");
            manager.switchState(new LostState(manager, globalVar));
            // yield return null;
        }

        public void switchState()
        {
            SceneManager.LoadScene("loader");
            manager.switchState(new LoaderState(manager, globalVar, "MenuState"));

            // SceneManager.LoadScene("menuState");
            // manager.switchState(new menuState(manager,globalVar));
        }
        public string getStateName()
        {
            return this.stateName;
        }
        public void action(string action)
        {

        }
        public void DetachedEvent()
        {
            manager.OnEndTouch -= TouchRelease;
            manager.OnStartTouch -= Fire;
        }
        ~PlayState()
        {
            DetachedEvent();
        }
    }
}