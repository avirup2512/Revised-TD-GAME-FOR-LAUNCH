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

public class PlayManager : MonoBehaviour
{
    private GameObject heroObject;
    public GameObject HeroAfterLoad;
    private GameObject heroStartPos;
    private GameObject bulletStartPos;
    public HeroEntity Hero;
    GameObject noBulletMsg;

    public GameObject thunder_prefab;

    private GameObject bulletObject;
    private GameObject[] bullet;

    private GameObject enemyObject;
    private GameObject enemy;
    private GameObject BackGround;
    private Image BackGroundImage;

    private GameObject enemyStartPositionObject;

    //private Khoka[] bulletComponent;
    private EnemyEntity enemyComponent;
    private EnemyEntity enemyComponent2;

    private float startPositionX;
    private float startPositionY;


    private GameObject[] bulletQuantityObject = new GameObject[4];

    private GameObject bulletMagzinePrimary;
    private GameObject bulletMagzineSecondery;
    private GameObject bulletMagzineSpecial;
    private GameObject bulletMagzineBomb;

    private TextMeshProUGUI bulletQuantityText;
    private TextMeshProUGUI magazineQuantityText;

    private TextMeshProUGUI bulletQuantityTextSEC;
    private TextMeshProUGUI magazineQuantityTextSEC;

    private TextMeshProUGUI bulletQuantityTextSPC;
    private TextMeshProUGUI magazineQuantityTextSPC;

    private TextMeshProUGUI bulletQuantityTextBomb;
    private TextMeshProUGUI magazineQuantityTextBomb;

    private GameObject enemyPos;
    private GameObject flyingEnemyPos;
    private GameObject enemyFloorBossPos;

    private GameObject floorBossStopPosition;
    private GameObject enemyObject2;
    public GameObject[] enemy2;

    private GameObject mainEnemy;

    private GameObject mainEnemyObject;

    private bool invokeCalled = false;

    private GameObject TopMenu;
    private AudioSource fireGun;
    public AudioSource luncherBlast;

    private AudioSource gameSound;
    private AudioSource reload;
    private AudioSource enimyDie;
    public AudioSource artilerySound;
    public AudioSource BossComing;
    public AudioSource EnemyHititing;
    public AudioSource ThunderPlay;

    public AudioSource EnemyISHIT;

    // private GameObject reloadButtonObject;
    // private Button reloadButton;

    private GameObject dragObject;
    // private dragAndFire dragCS;
    private GameObject khokaExtrat;

    public GameObject incomingSliderObject;
    public Slider incomingSlider;
    private int hasCame = 0;
    public int enemyRemain;

    public Camera cameraMain;

    private GameObject bulletGraphicsPR;
    private GameObject bulletGraphicsSEC;
    private GameObject bulletGraphicsSPC;
    private GameObject PRBullets;
    private GameObject SECBullets;
    private GameObject SPCBullets;
    private GameObject[] bulletGraphicsAfterLoadedPR;
    private GameObject[] bulletGraphicsAfterLoadedSEC;
    private GameObject[] bulletGraphicsAfterLoadedSPC;

    private GameObject bulletGraphicsStartPos;
    private SpriteRenderer spritePR;
    private SpriteRenderer spriteSEC;
    private SpriteRenderer spriteSPC;

    private GameObject coinEndPosition;
    private GameObject GunInHand;

    private GameObject upperHeightLimit;
    private GameObject lowerHeightLimit;

    public Vector3[] enemyPositionArray;
    private int buletIndx;

    public Round activeRound;

    public GlobalData globalVar;

    private int enemyComingCounter = 0;

    public GameObject[] Enemies = new GameObject[30];
    public GameObject[] FloorBoss = new GameObject[12];
    public GameObject coin;

    public GameObject bulletsPrefab;
    private GameObject bulletPosition;
    public GameObject[] bulletsSpritePrimaryAndSecondary;
    public GameObject[] bulletsSpriteSpecial;

    public GameObject Firepoint;
    public GameObject Firepoint2;
    public GameObject Khoka;

    private GameObject GuardInitialPositionObject;
    // private GameObject[] GuardInitialPositionResource;
    // private GameObject GuardModalUI;
    private GameObject GuardPanelBackground;
    public GameObject GuardLifeBar;
    public List<Guard> ExistingGuardList;
    public GameObject ModalCanvas;

    public GameObject[] guardUIPrefabOnModal;
    private Guard SelectedGuard;
    public TextMeshProUGUI coinCollectionTextComponent;

    public RoundGroupClass savedRoundGroupClass;
    public GameManager manager;
    public GameObject ThunderAttackPanel;

    public List<GameObject> LandMinePosition;
    public GameObject LandMineResources;
    public GameObject mainEnemySmall;
    public GameObject[] mainEnemySmallChalaChamunda;
    public GameObject TextIndicationObject;
    public GameObject DangerOverlay;
    public TextMeshProUGUI TextIndicationObjectText;
    public TextIndicationEntity textEntity;

    public GameObject Base;
    public CameraEntity BaseShake;
    public GameObject AtomBombPosition;
    public GameObject AtomBomb;
    public AtomBombEntity atmbentity;

    private GameObject primarySprite;
    private GameObject secondarySprite;
    private GameObject specialSprite;
    private GameObject bomgGunSprite;

    public GameObject ArtiliaryBomb;
    public GameObject[] ArtiliaryObjectPosition;
    int ArtiliaryCounter = 0;
    bool ArtiliaryStarted = false;
    GameObject ArtiliaryRadialProgress;
    RadialProgressEntity artiliaryRadial;
    public Button ArtialiaryUniqButton;
    public Button ThunderUniqButton;
    public GameObject ThunderParticleObject;
    public ParticleSystem Thunder;
    public GameObject LifeSaver;
    public GameObject LifeSaverInitialPosition;
    LifeSaverEntity lifeSaverEntity;
    bool LifeSaverAppeard = false;

    private GameObject BaseUI;
    private Image BaseImg;
    AudioSource bombBlast;

    GameObject mainCanvas;
    GameObject gurums;

    // Start is called before the first frame update
    void Start()
    {
        BackGround = GameObject.Find("BackGround");
        BackGroundImage = BackGround.GetComponent<Image>();
        BaseUI = GameObject.Find("Base");
        BaseImg = BaseUI.GetComponent<Image>();

        ThunderUniqButton = GameObject.Find("ThunderButton").GetComponent<Button>();

        mainCanvas = GameObject.Find("Canvas");
        gurums = GameObject.Find("thunder_prefab");

        thunder_prefab = Resources.Load("Prefabs/Thunder/thunder_prefab") as GameObject;
        ThunderAttackPanel = GameObject.Find("ThunderAttackPanel");
        ThunderAttackPanel.SetActive(false);

        khokaExtrat = GameObject.Find("khokaExtrat");
        bombBlast = GameObject.Find("bombBlast").GetComponent<AudioSource>();
        fireGun = GameObject.Find("fireGun").GetComponent<AudioSource>();
        luncherBlast = GameObject.Find("luncherBlast").GetComponent<AudioSource>();
        gameSound = GameObject.Find("gameSound").GetComponent<AudioSource>();
        ThunderPlay = GameObject.Find("ThunderPlay").GetComponent<AudioSource>();
        gameSound.Play();
        reload = GameObject.Find("reload").GetComponent<AudioSource>();

        enimyDie = GameObject.Find("enimyDie").GetComponent<AudioSource>();
        artilerySound = GameObject.Find("artilerySound").GetComponent<AudioSource>();

        BossComing = GameObject.Find("BossComing").GetComponent<AudioSource>();
        EnemyHititing = GameObject.Find("EnemyHititing").GetComponent<AudioSource>();
        EnemyISHIT = GameObject.Find("EnemyISHIT").GetComponent<AudioSource>();

        LifeSaverInitialPosition = GameObject.Find("LifeSaverInitialPosition");
        ThunderParticleObject = GameObject.Find("Thunder");
        if (ThunderParticleObject != null)
        {
            Thunder = ThunderParticleObject.GetComponent<ParticleSystem>();
            Thunder.Stop(true);
        }
        ArtiliaryRadialProgress = GameObject.Find("Radial Loader");
        artiliaryRadial = ArtiliaryRadialProgress.GetComponent<RadialProgressEntity>();
        ArtiliaryObjectPosition = GameObject.FindGameObjectsWithTag("Artiliary");
        AtomBombPosition = GameObject.Find("AtomBomb");
        upperHeightLimit = GameObject.Find("BaseUpperHeightLimit");
        Base = GameObject.Find("Base");
        BaseShake = Base.GetComponent<CameraEntity>();
        DangerOverlay = GameObject.Find("Danger Overlay");
        TextIndicationObject = DangerOverlay.transform.GetChild(0).gameObject;
        TextIndicationObjectText = TextIndicationObject.GetComponent<TextMeshProUGUI>();
        textEntity = DangerOverlay.GetComponent<TextIndicationEntity>();
        TopMenu = GameObject.Find("TopMenu");
        HeroAfterLoad = GameObject.Find("Hero") as GameObject;
        Hero = HeroAfterLoad.GetComponent<HeroEntity>();
        Hero.EnemyHititing = EnemyHititing;
        Firepoint = GameObject.Find("Firepoint");
        Firepoint2 = GameObject.Find("Firepoint2");
        GuardInitialPositionObject = GameObject.Find("GuardInitialPosition");
        GuardLifeBar = GameObject.Find("GuardLifeBar");
        GuardLifeBar.SetActive(false);

        noBulletMsg = GameObject.Find("noBulletMsg");
        noBulletMsg.SetActive(false);

        incomingSliderObject = GameObject.Find("EnemyInComing");
        if (incomingSliderObject)
        {
            incomingSlider = incomingSliderObject.GetComponent<Slider>();
            incomingSlider.value = 1;
        }
        bulletPosition = GameObject.Find("BulletsPosition");
        this.setWeaponsSprite();
        this.LoadMagazinePrimary();
        this.LoadMagazineSecondary();
        this.LoadMagazineSpecial();
        this.LoadBomb();
        showHideBulletsOnGunSelection(globalVar.selectedWeapon);

        coin = Resources.Load("Prefabs/Coin/CoinPrefab") as GameObject;
        Khoka = Resources.Load("Prefabs/KHOKA/KHOKA") as GameObject;
        changeGameBackground();
    }

    public IEnumerator EmptyBulletMsg()
    {
        noBulletMsg.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        noBulletMsg.SetActive(false);
    }

    public void emptyBuletMsgShow()
    {
        StopCoroutine(EmptyBulletMsg());
        StartCoroutine(EmptyBulletMsg());
    }

    public void changeGameBackground()
    {
        if (activeRound.roundGroupNumber <= 3)
        {
            BackGroundImage.sprite =
            Resources.Load<Sprite>("Prefabs/UI/background/background3") as Sprite;
            BaseImg.sprite =
            Resources.Load<Sprite>("Prefabs/UI/background/Base3") as Sprite;
        }
        else if (activeRound.roundGroupNumber > 3 && activeRound.roundGroupNumber <= 6)
        {
            BackGroundImage.sprite =
            Resources.Load<Sprite>("Prefabs/UI/background/backgroundJungle") as Sprite;
            BaseImg.sprite =
            Resources.Load<Sprite>("Prefabs/UI/background/Base3") as Sprite;
        }
        else if (activeRound.roundGroupNumber > 6 && activeRound.roundGroupNumber < 7)
        {
            BackGroundImage.sprite =
            Resources.Load<Sprite>("Prefabs/UI/background/background4") as Sprite;
            BaseImg.sprite =
            Resources.Load<Sprite>("Prefabs/UI/background/Base4") as Sprite;
        }
        else if (activeRound.roundGroupNumber > 7)
        {
            BackGroundImage.sprite =
            Resources.Load<Sprite>("Prefabs/UI/background/background4") as Sprite;
            BaseImg.sprite =
            Resources.Load<Sprite>("Prefabs/UI/background/Base4") as Sprite;
        }

    }

    public void GunSpriteChange(string name)
    {
        GunInHand = GameObject.Find("Gun");
        if (name == "Primary")
        {
            Sprite s = Resources.Load("Sprites/guns/herogun/primary/hand" + globalVar.PrimaryWeaponSelected.name, typeof(Sprite)) as Sprite;
            GunInHand.GetComponent<SpriteRenderer>().sprite = s;
            GunInHand.transform.localScale = new Vector3(0.89f, 0.89f, 0.0f);
        }
        else if (name == "Secondary")
        {
            Sprite s = Resources.Load("Sprites/guns/herogun/secondary/hand" + globalVar.SecondaryWeaponSelected.name, typeof(Sprite)) as Sprite;
            GunInHand.GetComponent<SpriteRenderer>().sprite = s;
            GunInHand.transform.localScale = new Vector3(0.75f, 0.75f, 0.0f);
        }
        else if (name == "Special")
        {
            Sprite s = Resources.Load("Sprites/guns/herogun/special/hand" + globalVar.SpecialWeaponSelected.name, typeof(Sprite)) as Sprite;
            GunInHand.GetComponent<SpriteRenderer>().sprite = s;
            GunInHand.transform.localScale = new Vector3(0.89f, 0.89f, 0.0f);
        }


    }

    public void LifeSaverAppear()
    {
        GameObject LifeSaverObject = Instantiate(LifeSaver, LifeSaverInitialPosition.transform);
        if (LifeSaverObject)
        {
            lifeSaverEntity = LifeSaverObject.GetComponent<LifeSaverEntity>();
            lifeSaverEntity.heroPositionX = HeroAfterLoad.transform.position.x;
            lifeSaverEntity.globalVar = globalVar;
            lifeSaverEntity.hero = Hero;
            lifeSaverEntity.go();
        }
        LifeSaverAppeard = true;
    }

    public void showTextIndication(string Text)
    {
        if (textEntity != null && TextIndicationObjectText != null)
        {
            TextIndicationObjectText.text = Text;
            textEntity.pop();
        }
    }

    public void LoadMagazinePrimary()
    {
        if (globalVar.PrimaryWeaponSelected.bulletPerMagazine == 0)
        {
            globalVar.PrimaryWeaponSelected.bulletPerMagazine = 3;
        }
        if (globalVar.PrimaryWeaponSelected.bulletPerMagazine > 0)
        {
            bulletsPrefab = Resources.Load("Prefabs/Bullets/PrimaryBullets/Bullets") as GameObject;
            bulletsSpritePrimaryAndSecondary = new GameObject[globalVar.PrimaryWeaponSelected.bulletPerMagazine];
            for (int x = 0; x < globalVar.PrimaryWeaponSelected.bulletPerMagazine; x++)
            {
                bulletsSpritePrimaryAndSecondary[x] = GameObject.Instantiate(bulletsPrefab, Firepoint.transform) as GameObject;
                bulletsSpritePrimaryAndSecondary[x].SetActive(false);
            }
        }
        bulletGraphicsPR = Resources.Load(globalVar.PrimaryWeaponSelected.magazineSprite) as GameObject;
        bulletGraphicsStartPos = GameObject.Find("PRBullets");
        bulletGraphicsAfterLoadedPR = new GameObject[globalVar.PrimaryWeaponSelected.bulletPerMagazine];
        PRBullets = GameObject.Find("PRBullets");
        int buletLmt = globalVar.PrimaryWeaponSelected.totalMagezine > 0 ? globalVar.PrimaryWeaponSelected.bulletPerMagazine : 0;
        for (int i = 0; i < buletLmt; i++)
        {
            // INSTANTIATE BULLET GRAPHICS //
            spritePR = bulletGraphicsPR.GetComponent<SpriteRenderer>();
            float bulletWidth = spritePR.bounds.size.x + 8.00f;
            bulletGraphicsPR.transform.position = new Vector3(bulletGraphicsStartPos.transform.position.x + (bulletWidth * i),
                        bulletGraphicsStartPos.transform.position.y);
            bulletGraphicsAfterLoadedPR[i] =
                GameObject.Instantiate(bulletGraphicsPR, PRBullets.transform, worldPositionStays: false) as GameObject;
        }
        bulletQuantityObject[0] = GameObject.Find("bulletQuantityIndication");
        bulletQuantityText = bulletQuantityObject[0].GetComponent<TextMeshProUGUI>();
        bulletQuantityText.text = globalVar.currentBulletIndex.ToString();

        bulletMagzinePrimary = GameObject.Find("GunSelectionGroup").
            transform.Find("PrimaryGun").transform.Find("MagazineHolder").transform.Find("primaryMagazine").gameObject;
        magazineQuantityText = bulletMagzinePrimary.GetComponent<TextMeshProUGUI>();
        magazineQuantityText.text = globalVar.PrimaryWeaponSelected.totalMagezine.ToString();
    }

    public void LoadMagazineSecondary()
    {
        if (globalVar.SecondaryWeaponSelected.bulletPerMagazine > 0)
        {
            bulletsPrefab = Resources.Load("Prefabs/Bullets/PrimaryBullets/Bullets") as GameObject;
            bulletsSpritePrimaryAndSecondary = new GameObject[globalVar.SecondaryWeaponSelected.bulletPerMagazine];
            for (int x = 0; x < globalVar.SecondaryWeaponSelected.bulletPerMagazine; x++)
            {
                bulletsSpritePrimaryAndSecondary[x] = GameObject.Instantiate(bulletsPrefab, Firepoint.transform) as GameObject;
                bulletsSpritePrimaryAndSecondary[x].SetActive(false);
            }
        }
        bulletGraphicsSEC = Resources.Load(globalVar.SecondaryWeaponSelected.magazineSprite) as GameObject;
        bulletGraphicsStartPos = GameObject.Find("SECBullets");
        bulletGraphicsAfterLoadedSEC = new GameObject[globalVar.SecondaryWeaponSelected.bulletPerMagazine];
        SECBullets = GameObject.Find("SECBullets");
        int bultLmt = globalVar.SecondaryWeaponSelected.totalMagezine > 0 ? globalVar.SecondaryWeaponSelected.bulletPerMagazine : 0;
        for (int i = 0; i < bultLmt; i++)
        {
            // INSTANTIATE BULLET GRAPHICS //
            spriteSEC = bulletGraphicsSEC.GetComponent<SpriteRenderer>();
            float bulletWidth = spriteSEC.bounds.size.x + 3.5f;
            bulletGraphicsSEC.transform.position = new Vector3(bulletGraphicsStartPos.transform.position.x + (bulletWidth * i),
                        bulletGraphicsStartPos.transform.position.y, -1);
            bulletGraphicsAfterLoadedSEC[i] =
            GameObject.Instantiate(bulletGraphicsSEC, SECBullets.transform, worldPositionStays: false) as GameObject;
        }

        bulletQuantityObject[1] = GameObject.Find("bulletQuantityIndicationSec");
        bulletQuantityTextSEC = bulletQuantityObject[1].GetComponent<TextMeshProUGUI>();
        bulletQuantityTextSEC.text = globalVar.currentBulletIndexSecondery.ToString();

        bulletMagzineSecondery = GameObject.Find("GunSelectionGroup").
            transform.Find("SecondaryGun").transform.Find("MagazineHolder").transform.Find("secondaryMagazine").gameObject;
        magazineQuantityTextSEC = bulletMagzineSecondery.GetComponent<TextMeshProUGUI>();
        magazineQuantityTextSEC.text = globalVar.SecondaryWeaponSelected.totalMagezine.ToString();

    }
    public void LoadMagazineSpecial()
    {
        if (globalVar.SpecialWeaponSelected.bulletPerMagazine > 0)
        {
            bulletsPrefab = Resources.Load("Prefabs/Bullets/SpecialBullet/Bullet1/Bullet1") as GameObject;
            bulletsSpriteSpecial = new GameObject[globalVar.SpecialWeaponSelected.bulletPerMagazine];
            for (int x = 0; x < globalVar.SpecialWeaponSelected.bulletPerMagazine; x++)
            {
                bulletsSpriteSpecial[x] = GameObject.Instantiate(bulletsPrefab, Firepoint.transform) as GameObject;
                bulletsSpriteSpecial[x].SetActive(false);
            }
        }
        bulletGraphicsSPC = Resources.Load(globalVar.SpecialWeaponSelected.magazineSprite) as GameObject;
        bulletGraphicsStartPos = GameObject.Find("SPCBullets");
        bulletGraphicsAfterLoadedSPC = new GameObject[globalVar.SpecialWeaponSelected.bulletPerMagazine];
        SPCBullets = GameObject.Find("SPCBullets");
        for (int i = 0; i < globalVar.SpecialWeaponSelected.bulletPerMagazine; i++)
        {
            // INSTANTIATE BULLET GRAPHICS //
            spriteSPC = bulletGraphicsSPC.GetComponent<SpriteRenderer>();
            float bulletWidth = spriteSPC.bounds.size.x + 4.00f;
            bulletGraphicsSPC.transform.position = new Vector3(bulletGraphicsStartPos.transform.position.x + (bulletWidth * i),
                        bulletGraphicsStartPos.transform.position.y, -1);
            bulletGraphicsAfterLoadedSPC[i] =
            GameObject.Instantiate(bulletGraphicsSPC, SPCBullets.transform, worldPositionStays: false) as GameObject;
        }
        bulletQuantityObject[2] = GameObject.Find("bulletQuantityIndicationSpc");
        bulletQuantityTextSPC = bulletQuantityObject[2].GetComponent<TextMeshProUGUI>();
        bulletQuantityTextSPC.text = globalVar.currentBulletIndexSecondery.ToString();

        bulletMagzineSpecial = GameObject.Find("GunSelectionGroup").
            transform.Find("Special Gun").transform.Find("MagazineHolder").transform.Find("specialMagazine").gameObject;
        magazineQuantityTextSPC = bulletMagzineSpecial.GetComponent<TextMeshProUGUI>();
        magazineQuantityTextSPC.text = globalVar.SpecialWeaponSelected.totalMagezine.ToString();

    }

    public void LoadBomb()
    {
        bulletMagzineBomb = GameObject.Find("GunSelectionGroup").
            transform.Find("bomb").transform.Find("MagazineHolder").transform.Find("bombMagazine").gameObject;
        magazineQuantityTextBomb = bulletMagzineBomb.GetComponent<TextMeshProUGUI>();
        magazineQuantityTextBomb.text = globalVar.BombWeaponSelected.totalMagezine.ToString();
    }

    void setWeaponsSprite()
    {
        primarySprite = GameObject.Find("GunSelectionGroup").transform.Find("PrimaryGun").transform.Find("primaryGunSprite").gameObject;
        primarySprite.GetComponent<Image>().sprite =
        Resources.Load<Sprite>(globalVar.PrimaryWeaponSelected.gunSprite) as Sprite;

        secondarySprite = GameObject.Find("GunSelectionGroup").transform.Find("SecondaryGun").transform.Find("secondaryGunSprite").gameObject;
        secondarySprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalVar.SecondaryWeaponSelected.gunSprite) as Sprite;

        specialSprite = GameObject.Find("GunSelectionGroup").transform.Find("Special Gun").transform.Find("specialGunSprite").gameObject;
        specialSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalVar.SpecialWeaponSelected.gunSprite) as Sprite;

        bomgGunSprite = GameObject.Find("GunSelectionGroup").transform.Find("bomb").transform.Find("bombSprite").gameObject;
        bomgGunSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalVar.BombWeaponSelected.gunSprite) as Sprite;
    }

    Vector3 SetZ(Vector3 vector, float z)
    {
        vector.z = z;
        return vector;
    }

    private Vector3 createEnemyPosition()
    {
        var randomY = Random.Range(upperHeightLimit.transform.position.y, lowerHeightLimit.transform.position.y);
        var randomX = Random.Range(enemyPos.transform.position.x, enemyPos.transform.position.x + 5);
        Vector3 vecna = new Vector3(enemyPos.transform.position.x, randomY, 0);
        if (enemyPositionArray.Length > 0)
        {
            for (int i = 0; i < enemyPositionArray.Length; i++)
            {
                if (enemyPositionArray[i].y == randomY)
                {
                    vecna = createEnemyPosition();
                }
            }
        }
        return vecna;
    }

    public void fire(string weaponType, Vector2 position)
    {
        if (weaponType == "Primary")
        {
            invokeCalled = false;
            bulletGraphicsAfterLoadedPR[globalVar.currentIndex].GetComponent<SpriteRenderer>().color =
            new Color(1f, 1f, 1f, .4f);
            BulletEntity bulEntity = bulletsSpritePrimaryAndSecondary[globalVar.currentIndex].GetComponent<BulletEntity>();
            bulEntity.endPosition = cameraMain.ScreenToWorldPoint(position);
            bulEntity.startPosition = Firepoint.transform.position;
            bulEntity.go(cameraMain.ScreenToWorldPoint(position));
            bulEntity.Weapon = weaponType;
            GameObject khka = Instantiate(Khoka, khokaExtrat.transform);
            //bulletGraphicsAfterLoadedPR[globalVar.currentIndex].SetActive(false);
            fireGun.Play();
        }
        else if (weaponType == "Secondary")
        {
            invokeCalled = false;
            bulletGraphicsAfterLoadedSEC[globalVar.currentIndexSecondary].GetComponent<SpriteRenderer>().color =
           new Color(1f, 1f, 1f, .4f);

            BulletEntity bulEntity = bulletsSpritePrimaryAndSecondary[globalVar.currentIndexSecondary].GetComponent<BulletEntity>();
            bulEntity.endPosition = cameraMain.ScreenToWorldPoint(position);
            bulEntity.startPosition = Firepoint2.transform.position;
            bulEntity.go(cameraMain.ScreenToWorldPoint(position));
            bulEntity.Weapon = weaponType;
            GameObject khka = Instantiate(Khoka, khokaExtrat.transform);
            fireGun.Play();
        }
        else if (weaponType == "Special")
        {
            invokeCalled = false;
            bulletGraphicsAfterLoadedSPC[globalVar.currentIndexSpecial].GetComponent<SpriteRenderer>().color =
            new Color(1f, 1f, 1f, .4f);

            BulletEntity bulEntity = bulletsSpriteSpecial[globalVar.currentIndexSpecial].GetComponent<BulletEntity>();
            bulEntity.endPosition = cameraMain.ScreenToWorldPoint(position);
            bulEntity.startPosition = Firepoint.transform.position;
            bulEntity.go(cameraMain.ScreenToWorldPoint(position));
            bulEntity.Weapon = weaponType;
            bulEntity.luncherBlast = luncherBlast;
            GameObject khka = Instantiate(Khoka, khokaExtrat.transform);
            fireGun.Play();
        }
        else if (weaponType == "Bomb")
        {
            invokeCalled = false;
            bulletGraphicsAfterLoadedSPC[globalVar.currentIndexSpecial].GetComponent<SpriteRenderer>().color =
            new Color(1f, 1f, 1f, .4f);
            bulletMagzineBomb = GameObject.Find("GunSelectionGroup").
                        transform.Find("bomb").transform.Find("MagazineHolder").transform.Find("bombMagazine").gameObject;
            magazineQuantityTextBomb = bulletMagzineBomb.GetComponent<TextMeshProUGUI>();
            magazineQuantityTextBomb.text = globalVar.BombWeaponSelected.totalMagezine.ToString();
            // bulEntity.luncherBlast = luncherBlast;
            // bombBlast.Play();
        }
    }
    public void setBulletZero()
    {
        bulletQuantityText.text = "0";
        bulletQuantityTextSEC.text = "0";
        bulletQuantityTextSPC.text = "0";
        //bulletQuantityTextBomb.text = "0";
    }

    public void ReloadMagazine(string weaponType)
    {
        reload.Play();
        globalVar.isReloading = false;
        if (weaponType == "Primary")
        {
            magazineQuantityText.text = globalVar.PrimaryWeaponSelected.totalMagezine.ToString();
            for (int i = 0; i < globalVar.PrimaryWeaponSelected.bulletPerMagazine; i++)
            {
                bulletGraphicsAfterLoadedPR[i].GetComponent<SpriteRenderer>().color =
                new Color(1f, 1f, 1f, 1f);
            }
        }
        else if (weaponType == "Secondary")
        {
            magazineQuantityTextSEC.text = globalVar.SecondaryWeaponSelected.totalMagezine.ToString();
            for (int i = 0; i < globalVar.SecondaryWeaponSelected.bulletPerMagazine; i++)
            {
                bulletGraphicsAfterLoadedSEC[i].GetComponent<SpriteRenderer>().color =
                new Color(1f, 1f, 1f, 1f);
            }
        }
        else if (weaponType == "Special")
        {
            magazineQuantityTextSPC.text = globalVar.SpecialWeaponSelected.totalMagezine.ToString();
            for (int i = 0; i < globalVar.SpecialWeaponSelected.bulletPerMagazine; i++)
            {
                bulletGraphicsAfterLoadedSPC[i].GetComponent<SpriteRenderer>().color =
                new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public void showHideBulletsOnGunSelection(string weaponType)
    {
        if (weaponType == "Primary")
        {
            PRBullets.SetActive(true);
            SECBullets.SetActive(false);
            SPCBullets.SetActive(false);
        }
        else if (weaponType == "Secondary")
        {
            PRBullets.SetActive(false);
            SECBullets.SetActive(true);
            SPCBullets.SetActive(false);
        }
        else if (weaponType == "Special")
        {
            PRBullets.SetActive(false);
            SECBullets.SetActive(false);
            SPCBullets.SetActive(true);
        }
    }

    public void stopFire()
    {
        // anim.SetBool("shoot", false);
        // anim.SetBool("dontShoot", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (globalVar.floorBossDied)
        {
            StopCoroutine("Shake");
        }
        if (globalVar.instantiateEnemy && enemyComingCounter < activeRound.howManyTimesEnemyComes)
        {
            StopCoroutine(instantiateEnemy());
            StartCoroutine(instantiateEnemy());
        }
        if (globalVar.instantiateEnemy && enemyComingCounter == activeRound.howManyTimesEnemyComes && activeRound.mainRound && !globalVar.floorBossDied)
        {
            StopCoroutine(instantiateFloorBoss());
            StartCoroutine(instantiateFloorBoss());
        }
        if (enemyComingCounter == activeRound.howManyTimesEnemyComes && !activeRound.mainRound)
        {
            globalVar.isWon = true;
        }
        else if (enemyComingCounter == activeRound.howManyTimesEnemyComes && activeRound.mainRound && globalVar.floorBossIntantiated && globalVar.floorBossDied)
        {
            globalVar.isWon = true;
        }
        if (!globalVar.isReloading)
        {
            bulletQuantityText.text = globalVar.currentBulletIndex.ToString();
            bulletQuantityTextSEC.text = globalVar.currentBulletIndexSecondery.ToString();
            bulletQuantityTextSPC.text = globalVar.currentBulletIndexSpecial.ToString();
        }
        if (ArtiliaryCounter == 20)
        {
            StopCoroutine("ArtiliaryFire");
            artilerySound.Stop();
            ArtialiaryUniqButton.interactable = true;
            ThunderUniqButton.interactable = true;
            artiliaryRadial.resetCurrentvalue(0);
            globalVar.isUniqueWeaponFire = false;
        }
        if (Hero.LifeBar.value < 50 && !LifeSaverAppeard)
        {
            LifeSaverAppear();
        }
    }
    public void AddGuard(Guard selectedGuard)
    {
        GameObject obj = Resources.Load(selectedGuard.guardType.guardPrefabs) as GameObject;
        if (GuardInitialPositionObject == null)
            GuardInitialPositionObject = GameObject.Find("GuardInitialPosition");
        GameObject grd = Instantiate(obj, GuardInitialPositionObject.transform);
        globalVar.GuardInitiated = true;
        if (GuardLifeBar == null)
            GuardLifeBar = GameObject.Find("GuardLifeBar");
        GuardLifeBar.SetActive(true);
        GuardEntity grdEntity = grd.GetComponent<GuardEntity>();
        grdEntity.GuardLifeBar = GuardLifeBar;
        grdEntity.selectedGuard = selectedGuard;
        grdEntity.globalVar = globalVar;
        globalVar.GuardIsAdded = true;

        if (selectedGuard.guardType.name == "Land Mine")
        {
            LandMineResources = Resources.Load("Prefabs/Guard/LandMine/LandMine") as GameObject;
            if (LandMinePosition.Count == 0)
            {
                LandMinePosition = FindObjectByName("LandMine");
                for (int x = 0; x < LandMinePosition.Count; x++)
                {
                    Instantiate(LandMineResources, LandMinePosition[x].transform);
                }
            }
        }
    }
    public void openGuardModal()
    {
        if (CheckPurchasedGuard())
        {
            ModalCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            if (!globalVar.GuardIsAdded)
                AddGuard(globalVar.ExistingGuardList[0]);
        }
    }
    public void AtomBombing()
    {
        globalVar.isUniqueWeaponFire = true;
        GameObject atomBomb = Instantiate(AtomBomb, AtomBombPosition.transform);
        atmbentity = atomBomb.GetComponent<AtomBombEntity>();
        atmbentity.artilerySound = artilerySound;
        atmbentity.BasePosition = upperHeightLimit;
        atmbentity.go();
        artilerySound.Play();
        globalVar.isUniqueWeaponFire = false;
    }
    public void ArtiliaryFireClick(Button btn)
    {
        globalVar.isUniqueWeaponFire = true;
        ArtiliaryStarted = true;
        btn.interactable = false;
        artiliaryRadial.startLoader();
        ArtiliaryCounter = 0;
        StartCoroutine("ArtiliaryFire");
    }
    public IEnumerator ArtiliaryFire()
    {
        if (ArtiliaryObjectPosition.Length > 0)
        {
            for (int x = 0; x < 20; x++)
            {
                for (int i = 0; i < ArtiliaryObjectPosition.Length; i++)
                {
                    GameObject atomBomb = Instantiate(ArtiliaryBomb, ArtiliaryObjectPosition[i].transform);
                    atmbentity = atomBomb.GetComponent<AtomBombEntity>();
                    atmbentity.BasePosition = upperHeightLimit;
                    atmbentity.go();
                }
                ArtiliaryCounter++;
                artiliaryRadial.resetCurrentvalue(x);
                artilerySound.Play();
                yield return new WaitForSeconds(0.8f);
            }
        }
    }
    public void Gurum(bool isFire)
    {
        if (isFire)
        {
            globalVar.isUniqueWeaponFire = true;
            GameObject[] enimys = GameObject.FindGameObjectsWithTag("Enemy");
            ThunderPlay.Play();
            for (int a = 0; a < enimys.Length; a++)
            {
                enimys[a].GetComponent<EnemyEntity>().GetHit(10000000);
            }
            ThunderAttackPanel.SetActive(true);
        }
        else
        {
            globalVar.isUniqueWeaponFire = false;
            ThunderAttackPanel.SetActive(false);
        }
    }
    public void closeGuardModal()
    {
        ModalCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void doneGuardModal()
    {
        ModalCanvas.SetActive(false);
        Time.timeScale = 1;
        if (!globalVar.GuardIsAdded)
            AddGuard(SelectedGuard);

    }
    void addEventListenerToGuardButton(Button btn, Guard selectedGuard)
    {
        btn.onClick.AddListener(() => guardSelection(guardUIPrefabOnModal, selectedGuard));
    }
    void addEventListenerToGuardPurchasedButton(Button btn, Guard selectedGuard)
    {
        btn.onClick.AddListener(() => purChasedGuard(selectedGuard));
    }
    public void guardSelection(GameObject[] guard, Guard selectedGuard)
    {
        SelectedGuard = selectedGuard;
        if (guard.Length > 0)
        {
            for (int x = 0; x < guard.Length; x++)
            {
                Transform selectedObject = guard[x].transform.GetChild(5);
                Transform gurdName = guard[x].transform.GetChild(3);
                TextMeshProUGUI gurdNameText = gurdName.gameObject.GetComponent<TextMeshProUGUI>();
                if (gurdNameText.text == selectedGuard.guardType.name)
                {
                    selectedObject.gameObject.SetActive(true);
                }
                else
                {
                    selectedObject.gameObject.SetActive(false);
                }
            }
        }
    }
    private bool CheckPurchasedGuard()
    {
        int guardCheckCounter = 0;
        bool hasMoreThanOneGuard = false;
        if (ExistingGuardList.Count > 0)
        {
            for (int x = 0; x < ExistingGuardList.Count; x++)
            {
                if (ExistingGuardList[x].purchasedGuard > 0)
                {
                    guardCheckCounter++;
                }
            }
        }
        if (guardCheckCounter > 1)
            hasMoreThanOneGuard = true;
        return hasMoreThanOneGuard;
    }

    private void purChasedGuard(Guard selectedGuard)
    {
        if (globalVar.totalCoin < selectedGuard.price)
        {
            return;
        }
        if (globalVar.ExistingGuardList.Count > 0)
        {
            globalVar.totalCoin = globalVar.totalCoin - selectedGuard.price;
            if (coinCollectionTextComponent != null)
                coinCollectionTextComponent.text = globalVar.totalCoin.ToString();
            for (int x = 0; x < globalVar.ExistingGuardList.Count; x++)
            {
                if (selectedGuard.guardType.name == globalVar.ExistingGuardList[x].guardType.name)
                {
                    globalVar.ExistingGuardList[x].purchasedGuard = globalVar.ExistingGuardList[x].purchasedGuard + 1;
                }
            }
        }

        if (savedRoundGroupClass != null)
        {
            savedRoundGroupClass.Guards = globalVar.ExistingGuardList;
            savedRoundGroupClass.TotalCoins = globalVar.totalCoin;
            manager.saveCurrentGameState(savedRoundGroupClass);
        }
    }
    private int createRandomNumber(int min, int max)
    {
        int randomNumberEnemy = 0;
        randomNumberEnemy = Random.Range(min, max);
        if (hasCame + randomNumberEnemy > enemyRemain)
        {
            randomNumberEnemy = enemyRemain;
        }
        else if (hasCame + randomNumberEnemy == globalVar.totalIncomingEnemy)
        {
        }
        return randomNumberEnemy;
    }
    public void countEnemyandSetSliderValue(int enemyCount)
    {
        hasCame += enemyCount;
        enemyRemain -= enemyCount;
        globalVar.enemyRemain -= enemyCount;
        if (incomingSlider)
            incomingSlider.value = hasCame / 100;
    }

    public IEnumerator instantiateEnemy()
    {
        globalVar.instantiateEnemy = false;
        enemyPos = GameObject.Find("EnemyInitialPosition");
        flyingEnemyPos = GameObject.Find("FlyingEnemyInitialPosition");
        int randomNumberEnemy = (int)Mathf.Ceil(globalVar.totalIncomingEnemy / activeRound.howManyTimesEnemyComes);
        if (randomNumberEnemy > enemyRemain)
        {
            randomNumberEnemy = enemyRemain;
        }
        countEnemyandSetSliderValue(randomNumberEnemy);
        // globalVar.bornEnemy = new List<string>[randomNumberEnemy];
        enemyPositionArray = new Vector3[randomNumberEnemy];
        upperHeightLimit = GameObject.Find("BaseUpperHeightLimit");
        lowerHeightLimit = GameObject.Find("BaseLowerHeightLimit");
        enemyPos = GameObject.Find("EnemyInitialPosition");
        for (int x = 0; x < randomNumberEnemy; x++)
        {
            enemyPositionArray[x] = createEnemyPosition();
        }
        System.Array.Sort(enemyPositionArray, YPositionComparison);
        enemy2 = new GameObject[enemyPositionArray.Length];
        for (int x = 0; x < enemyPositionArray.Length; x++)
        {
            string enemyName = "";
            Enemy enemy = activeRound.comingEnemy[0];
            // FOR ROUND 1 AND 2
            if (activeRound.comingEnemy.Count == 2)
            {
                if (x % 2 == 0)
                {
                    enemy = activeRound.comingEnemy[0];
                    enemyName = enemy.name;
                }
                else
                {
                    enemy = activeRound.comingEnemy[1];
                    enemyName = enemy.name;
                }
            }
            else if (activeRound.comingEnemy.Count == 3)
            {
                if (x % 2 == 0)
                {
                    enemy = activeRound.comingEnemy[0];
                    enemyName = enemy.name;
                }
                else if (x % 3 == 0)
                {
                    enemy = activeRound.comingEnemy[1];
                    enemyName = enemy.name;
                }
                else
                {
                    enemy = activeRound.comingEnemy[2];
                    enemyName = enemy.name;
                }
            }
            int index = 0;
            for (int a = 0; a < Enemies.Length; a++)
            {
                if (Enemies[a] != null && Enemies[a].name == enemyName)
                {
                    index = a;
                }
            }
            // enemyObject2 = Resources.Load("Prefabs/Demo Enemies/" + enemyName + "/" + enemyName) as GameObject;
            enemy2[x] = Instantiate(Enemies[index], enemyPos.transform) as GameObject;
            enemyComponent2 = enemy2[x].GetComponent<EnemyEntity>();
            enemyComponent2.enimyDie = enimyDie;
            enemyComponent2.EnemyISHIT = EnemyISHIT;
            enemyComponent2.coin = coin;
            enemyComponent2.enemyClass = enemy;
            enemyComponent2.enemytag = "enemy" + x;
            globalVar.bornEnemy.Add(enemyComponent2.enemytag);
            enemyComponent2.heroPositionX = HeroAfterLoad.transform.position.x;
            enemyComponent2.Hero = HeroAfterLoad;
            float enemyHeight = enemy2[x].GetComponent<SpriteRenderer>().bounds.size.y / 2;
            enemy2[x].transform.position = new Vector3(enemyPositionArray[x].x, (enemy.canFly ? flyingEnemyPos.transform.position.y : enemyPositionArray[x].y) + enemyHeight, 0);
            enemyComponent2.globalVar = globalVar;
            SpriteRenderer sprite = enemy2[x].GetComponent<SpriteRenderer>();
            if (sprite)
                sprite.sortingOrder = 100 - x;
            yield return new WaitForSeconds(1f);
        }
        enemyComingCounter++;
        // }
    }
    private int YPositionComparison(Vector3 a, Vector3 b)
    {
        //null check, I consider nulls to be less than non-null
        if (a == null) return (b == null) ? 0 : -1;
        if (b == null) return 1;

        var ya = a.y;
        var yb = b.y;
        return ya.CompareTo(yb); //here I use the default comparison of floats
    }
    List<GameObject> FindObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        List<GameObject> obj = new List<GameObject>();
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].name == name)
            {
                obj.Add(objs[i].gameObject);
            }
        }
        return obj;
    }
    public IEnumerator instantiateFloorBoss()
    {
        if (!globalVar.floorBossIntantiated)
        {
            showTextIndication("Boss is Coming");
        }
        globalVar.instantiateEnemy = false;
        enemyPos = GameObject.Find("EnemyInitialPosition");
        enemyFloorBossPos = GameObject.Find("EnemyFloorBossInitialPosition");
        floorBossStopPosition = GameObject.Find("FloorBossStopPosition");

        enemy2 = new GameObject[10];
        mainEnemySmallChalaChamunda = new GameObject[10];
        enemyPositionArray = new Vector3[10];
        upperHeightLimit = GameObject.Find("BaseUpperHeightLimit");
        lowerHeightLimit = GameObject.Find("BaseLowerHeightLimit");
        enemyPos = GameObject.Find("EnemyInitialPosition");
        for (int x = 0; x < 10; x++)
        {
            enemyPositionArray[x] = createEnemyPosition();

        }
        System.Array.Sort(enemyPositionArray, YPositionComparison);
        for (int x = 0; x < enemyPositionArray.Length; x++)
        {

            string enemyName = "";
            Enemy enemy = activeRound.comingEnemy[0];
            // FOR ROUND 1 AND 2
            if (activeRound.comingEnemy.Count == 2)
            {
                if (x % 2 == 0)
                {
                    enemy = activeRound.comingEnemy[0];
                    enemyName = enemy.name;
                }
                else
                {
                    enemy = activeRound.comingEnemy[1];
                    enemyName = enemy.name;
                }
            }
            else if (activeRound.comingEnemy.Count == 3)
            {
                if (x % 2 == 0)
                {
                    enemy = activeRound.comingEnemy[0];
                    enemyName = enemy.name;
                }
                else if (x % 3 == 0)
                {
                    enemy = activeRound.comingEnemy[1];
                    enemyName = enemy.name;
                }
                else
                {
                    enemy = activeRound.comingEnemy[2];
                    enemyName = enemy.name;
                }
            }
            int index = 0;
            for (int a = 0; a < Enemies.Length; a++)
            {
                if (Enemies[a] != null && Enemies[a].name == enemyName)
                {
                    index = a;
                }
            }
            // enemyObject2 = Resources.Load("Prefabs/Demo Enemies/" + enemyName + "/" + enemyName) as GameObject;
            enemy2[x] = GameObject.Instantiate(Enemies[index], enemyPos.transform);
            enemyComponent2 = enemy2[x].GetComponent<EnemyEntity>();
            enemyComponent2.enimyDie = enimyDie;
            enemyComponent2.EnemyISHIT = EnemyISHIT;
            enemyComponent2.coin = coin;
            enemyComponent2.enemyClass = enemy;
            enemyComponent2.enemytag = "enemy" + x;
            // enemyComponent2.heroLifePos = coinEndPosition.transform.position;
            globalVar.bornEnemy.Add(enemyComponent2.enemytag);
            enemyComponent2.heroPositionX = HeroAfterLoad.transform.position.x;
            enemyComponent2.Hero = HeroAfterLoad;
            float enemyHeight = enemy2[x].GetComponent<SpriteRenderer>().bounds.size.y / 2;
            enemy2[x].transform.position = new Vector3(enemyPositionArray[x].x, (enemy.canFly ? flyingEnemyPos.transform.position.y : enemyPositionArray[x].y) + enemyHeight, 0);
            enemyComponent2.globalVar = globalVar;
            SpriteRenderer sprite = enemy2[x].GetComponent<SpriteRenderer>();
            if (sprite)
                sprite.sortingOrder = 100 - x;
            yield return new WaitForSeconds(1f);
        }

        int indexMainEnemy = 0;
        for (int a = 0; a < FloorBoss.Length; a++)
        {
            if (FloorBoss[a] != null && FloorBoss[a].name == "mainEnemy" + activeRound.roundGroupNumber)
            {
                indexMainEnemy = a;
            }
        }
        // SPECIAL ENEMY FOR ROUND STARTS
        for (int x = 0; x < 10; x++)
        {
            enemyPositionArray[x] = createEnemyPosition();
        }
        System.Array.Sort(enemyPositionArray, YPositionComparison);
        for (int x = 0; x < enemyPositionArray.Length; x++)
        {
            mainEnemySmallChalaChamunda[x] = GameObject.Instantiate(FloorBoss[indexMainEnemy], enemyFloorBossPos.transform);
            mainEnemySmallChalaChamunda[x].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            enemyComponent2 = mainEnemySmallChalaChamunda[x].GetComponent<EnemyEntity>();
            enemyComponent2.enimyDie = enimyDie;
            enemyComponent2.EnemyISHIT = EnemyISHIT;
            enemyComponent2.coin = coin;
            // enemyComponent2.enemyClass = enemy;
            enemyComponent2.enemytag = "mainEnemyCompanion" + x;
            // enemyComponent2.heroLifePos = coinEndPosition.transform.position;
            globalVar.bornEnemy.Add(enemyComponent2.enemytag);
            enemyComponent2.heroPositionX = HeroAfterLoad.transform.position.x;
            enemyComponent2.Hero = HeroAfterLoad;
            float enemyHeight = mainEnemySmallChalaChamunda[x].GetComponent<SpriteRenderer>().bounds.size.y / 2;
            mainEnemySmallChalaChamunda[x].transform.position = new Vector3(enemyPositionArray[x].x, enemyPositionArray[x].y + enemyHeight, 0);
            enemyComponent2.globalVar = globalVar;
            SpriteRenderer sprite = mainEnemySmallChalaChamunda[x].GetComponent<SpriteRenderer>();
            if (sprite)
                sprite.sortingOrder = 100 - x;
            yield return new WaitForSeconds(1f);
        }
        // SPECIAL ENEMY FOR ROUND  ENDS
        if (!globalVar.floorBossIntantiated)
        {
            if (activeRound.roundGroupNumber != 6 || activeRound.roundGroupNumber != 8)
            {
                StartCoroutine("Shake");
                BossComing.Play();
            }
            mainEnemy = GameObject.Instantiate(FloorBoss[indexMainEnemy], enemyFloorBossPos.transform);
            EnemyEntity mainEnemyComponent = mainEnemy.GetComponent<EnemyEntity>();
            mainEnemyComponent.enimyDie = enimyDie;
            mainEnemyComponent.EnemyISHIT = EnemyISHIT;
            enemyComponent2.coin = coin;
            mainEnemyComponent.enemyClass = activeRound.mainEnemy;
            mainEnemyComponent.mainEnemy = true;
            mainEnemyComponent.floorBossStopPosition = floorBossStopPosition;
            mainEnemyComponent.heroPositionX = HeroAfterLoad.transform.position.x;
            mainEnemyComponent.Hero = HeroAfterLoad;
            mainEnemyComponent.globalVar = globalVar;
            float enemyHeight = mainEnemyComponent.GetComponent<SpriteRenderer>().bounds.size.y / 3;
            if (activeRound.mainEnemy.canFly)
            {
                mainEnemy.transform.position = new Vector3(mainEnemy.transform.position.x, flyingEnemyPos.transform.position.y, 0);
            }
            else
            {
                // mainEnemy.transform.position = new Vector3(mainEnemy.transform.position.x, mainEnemy.transform.position.y + enemyHeight, 0);
            }
            globalVar.floorBossIntantiated = true;
        }
    }
    private IEnumerator Shake()
    {
        while (!globalVar.floorBossDied)
        {
            BaseShake.setShakeDuration(0.02f);
            yield return new WaitForSeconds(.4f);
            // Do yer stuff
        }
    }
    ~PlayManager()
    {
        StopCoroutine(instantiateEnemy());
        StopCoroutine(instantiateFloorBoss());
    }
}
