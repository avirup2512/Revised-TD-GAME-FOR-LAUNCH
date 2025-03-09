// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Assets.Script.interfaces;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
// using UnityEngine.Tilemaps;
// using Assets.Script.entity;
// using Assets.Script.globalVar;
// using TMPro;

// public class PlayManager : MonoBehaviour
// {
//     private GameObject heroObject;
//     public GameObject HeroAfterLoad;
//     private GameObject heroStartPos;
//     private GameObject bulletStartPos;
//     public HeroEntity Hero;

//     private GameObject bulletObject;
//     private GameObject[] bullet;

//     private GameObject enemyObject;
//     private GameObject enemy;

//     private GameObject enemyStartPositionObject;

//     //private Khoka[] bulletComponent;
//     private EnemyEntity enemyComponent;
//     private EnemyEntity enemyComponent2;

//     private float startPositionX;
//     private float startPositionY;


//     private GameObject bulletQuantityObject;

//     private GameObject bulletMagzinePrimary;
//     private GameObject bulletMagzineSecondery;
//     private GameObject bulletMagzineSpecial;

//     private TextMeshProUGUI bulletQuantityText;
//     private TextMeshProUGUI magazineQuantityText;

//     private GameObject enemyPos;
//     private GameObject enemyObject2;
//     private GameObject[] enemy2;

//     private bool invokeCalled = false;

//     private GameObject TopMenu;

//     // private GameObject reloadButtonObject;
//     // private Button reloadButton;

//     private GameObject dragObject;
//     // private dragAndFire dragCS;


//     public GameObject incomingSliderObject;
//     public Slider incomingSlider;
//     private int hasCame = 0;
//     public int enemyRemain;

//     private GameObject bulletGraphics;
//     private GameObject[] bulletGraphicsAfterLoaded;

//     private GameObject bulletGraphicsStartPos;
//     private SpriteRenderer sprite;

//     private GameObject coinEndPosition;

//     private GameObject upperHeightLimit;
//     private GameObject lowerHeightLimit;

//     public Vector3[] enemyPositionArray;
//     private int buletIndx;

//     public GlobalData globalVar;
//     // Start is called before the first frame update
//     void Start()
//     {
//         TopMenu = GameObject.Find("TopMenu");
//         HeroAfterLoad = GameObject.Find("Hero") as GameObject;
//         Hero = HeroAfterLoad.GetComponent<HeroEntity>();

//         incomingSliderObject = GameObject.Find("EnemyInComing");
//         incomingSlider = incomingSliderObject.GetComponent<Slider>();
//         LoadMagazine(globalVar.selectedWeapon);
//     }

//     public void LoadMagazine(string weaponType){
//         if(weaponType == "Primary"){
//             buletIndx = globalVar.bulletlimit;
//             bulletGraphics = Resources.Load("Prefabs/Bullets/bulletPrimary") as GameObject;
//         }
//         else if(weaponType == "Secondery"){
//             buletIndx = globalVar.bulletlimitSecondery;
//             bulletGraphics = Resources.Load("Prefabs/Bullets/bulletSecondery") as GameObject;
//         }
//         else if(weaponType == "Special"){
//             buletIndx = globalVar.bulletlimitSpecial; //Special means for special weapons
//             bulletGraphics = Resources.Load("Prefabs/Bullets/bulletSpecial") as GameObject;
//         }
//         bulletGraphicsStartPos = GameObject.Find("bulletGraphicsStartPos");
//         //bullet = new GameObject[buletIndx];
//         //bulletComponent = new Khoka[buletIndx];
//         bulletGraphicsAfterLoaded = new GameObject[buletIndx];

//         for (int i = 0; i < buletIndx; i++)
//         {
//             // INSTANTIATE BULLET GRAPHICS //
//             sprite = bulletGraphics.GetComponent<SpriteRenderer>();
//             float bulletWidth = sprite.bounds.size.x + .3f;
//             bulletGraphics.transform.position = new Vector3(bulletGraphicsStartPos.transform.position.x + (bulletWidth * i), 
//                         bulletGraphicsStartPos.transform.position.y,2);
//             bulletGraphicsAfterLoaded[i] = GameObject.Instantiate(bulletGraphics) as GameObject;
//         }
        
//         bulletQuantityObject = GameObject.Find("bulletQuantityIndication");
//         bulletQuantityText = bulletQuantityObject.GetComponent<TextMeshProUGUI>();
//         bulletQuantityText.text = globalVar.currentBulletIndex.ToString();

//         bulletMagzinePrimary = GameObject.Find("primaryMagazine");
//         magazineQuantityText = bulletMagzinePrimary.GetComponent<TextMeshProUGUI>();
//         magazineQuantityText.text = globalVar.totalMagaZinePrimary.ToString();
        
//     }

//     Vector3 SetZ(Vector3 vector, float z)
//     {
//         vector.z = z;
//         return vector;
//     } 

//     private Vector3 createEnemyPosition()
//     {
//         var randomY = Random.Range(upperHeightLimit.transform.position.y, lowerHeightLimit.transform.position.y);
//         var randomX = Random.Range(enemyPos.transform.position.x, enemyPos.transform.position.x + 5);
//         Vector3 vecna = new Vector3(randomX, randomY, 0);
//         if (enemyPositionArray.Length > 0)
//         {
//             for (int i = 0; i < enemyPositionArray.Length; i++)
//             {
//                 if (enemyPositionArray[i].y == randomY || enemyPositionArray[i].x == randomX)
//                 {
//                     vecna = createEnemyPosition();
//                 }
//             }
//         }
//         return vecna;
//     }

//     public void fire()
//     {
//         invokeCalled = false;
//         bulletGraphicsAfterLoaded[globalVar.currentIndex].SetActive(false);
//     }

//     public void ReloadMagazinePrimary(){
//         magazineQuantityText.text = globalVar.totalMagaZinePrimary.ToString();
//         for (int i = 0; i < buletIndx; i++)
//         {
//             bulletGraphicsAfterLoaded[i].SetActive(true);
//         }
//     }

//     public void stopFire()
//     {
//         // anim.SetBool("shoot", false);
//         // anim.SetBool("dontShoot", true);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (globalVar.instantiateEnemy && enemyRemain > 0)
//         {
//             instantiateEnemy();
//         }
//         if (enemyRemain <= 0)
//         {
//             globalVar.isWon = true;
//         }
//         bulletGraphicsStartPos = GameObject.Find("bulletGraphicsStartPos");
//         bulletQuantityText.text =  globalVar.currentBulletIndex.ToString();

//         // if (globalVar.isReloading && globalVar.totalMagaZinePrimary > 0)
//         // {
//         //     //stopFire();
//         //     if (!invokeCalled)
//         //         InvokeRepeating("reload", 0f, .2f);
//         // }
//         // if (globalVar.currentBulletIndex == globalVar.bulletlimit)
//         // {
//         //     globalVar.isReloading = false;
//         //     CancelInvoke("reload");
//         // }

//         // if (globalVar.isFiring)
//         // {
//         //     fire(globalVar.lastMousePosition);
//         // }
//         // else
//         // {
//         //     stopFire();
//         // }

//     }

//     // void reload()
//     // {
//     //     globalVar.reload();
//     //     if (bulletGraphicsAfterLoaded[globalVar.currentBulletIndex - 1] != null)
//     //     {
//     //         bulletGraphicsAfterLoaded[globalVar.currentBulletIndex - 1].SetActive(true);
//     //     }
//     //     invokeCalled = true;
//     // }
//     private int createRandomNumber(int min, int max)
//     {
//         int randomNumberEnemy = 0;
//         randomNumberEnemy = Random.Range(min, max);
//         if (hasCame + randomNumberEnemy > enemyRemain)
//         {
//             randomNumberEnemy = enemyRemain;
//         }
//         else if (hasCame + randomNumberEnemy == globalVar.totalIncomingEnemy)
//         {
//             Debug.Log("DEBUG GAYA");
//         }
//         return randomNumberEnemy;
//     }
//     public void countEnemyandSetSliderValue(int enemyCount)
//     {
//         hasCame += enemyCount;
//         enemyRemain -= enemyCount;
//         // if (enemyRemain <= 0)
//         // {
//         //     globalVar.isWon = true;
//         // }
//         globalVar.enemyRemain -= enemyCount;
//         if (incomingSlider)
//             incomingSlider.value = hasCame;
//     }

//     public void instantiateEnemy()
//     {
//         Debug.Log(enemyRemain);
//         globalVar.instantiateEnemy = false;
//         Debug.Log(globalVar.totalIncomingEnemy);
//         int randomNumberEnemy = (int)Mathf.Ceil(globalVar.totalIncomingEnemy / 3);
//         if (randomNumberEnemy > enemyRemain)
//         {
//             randomNumberEnemy = enemyRemain;
//         }
//         countEnemyandSetSliderValue(randomNumberEnemy);
//         enemy2 = new GameObject[randomNumberEnemy];
//         // globalVar.bornEnemy = new List<string>[randomNumberEnemy];
//         enemyPositionArray = new Vector3[randomNumberEnemy];
//         for (int x = 0; x < randomNumberEnemy; x++)
//         {
//             upperHeightLimit = GameObject.Find("BaseUpperHeightLimit");
//             lowerHeightLimit = GameObject.Find("BaseLowerHeightLimit");
//             enemyPos = GameObject.Find("EnemyInitialPosition");
//             enemyPositionArray[x] = createEnemyPosition();
//         }
//         for (int x = 0; x < enemyPositionArray.Length; x++)
//         {
//             enemyPos = GameObject.Find("EnemyInitialPosition");
//             string enemyName = "Enemy" + globalVar.activeRound.roundNumber;
//             enemyObject2 = Resources.Load("Prefabs/Demo Enemies/" + enemyName) as GameObject;
//             enemy2[x] = GameObject.Instantiate(enemyObject2, enemyPos.transform);
//             enemyComponent2 = enemy2[x].GetComponent<EnemyEntity>();
//             enemyComponent2.enemytag = "enemy" + x;
//             // enemyComponent2.heroLifePos = coinEndPosition.transform.position;
//             globalVar.bornEnemy.Add(enemyComponent2.enemytag);
//             enemyComponent2.heroPositionX = HeroAfterLoad.transform.position.x;
//             enemy2[x].transform.position = new Vector3(enemyPositionArray[x].x, enemyPositionArray[x].y, 0);
//             enemyComponent2.globalVar = globalVar;
//         }
//         // }
//     }

// }
