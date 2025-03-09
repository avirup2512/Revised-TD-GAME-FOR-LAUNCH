// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Assets.Script.interfaces;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
// using UnityEngine.Tilemaps;
// using Assets.Script.entity;
// using Assets.Script.globalVar;

// namespace Assets.Script.state
// {
//     public class PlayState : GameState
//     {
//         private GameManager manager;
//         public string stateName;

//         public GameObject HeroObject;
//         public GameObject PlayManagerObject;
//         public PlayManager PlayManagerScript;
//         public HeroEntity Hero;

//         public GlobalData globalVar;

//         private Camera cameraMain;

//         private EnemyEntity Enemy;

//         GameObject MenuButtonObject;
//         Button MenuButton;
//         private float delay = 0;
//         private float heroLifeDelay = 0;

//         private bool Bool = false;

//         private Toggle machineGun;
//         private Toggle shotGun;
//         private Toggle specialGun;
//         private ToggleGroup GunSelection;

//         public string selectedWeapon;

//         public bool primarySelected;
//         public bool secondarySelected;
//         public bool specialSelected;

//         GameObject Bullets;

//         public PlayState(GameManager managerRef, GlobalData globalRef)
//         {
//             manager = managerRef;
//             globalVar = globalRef;
//             stateName = "attackState";
//             Time.timeScale = 1;
//             globalVar.currentIndex = 0;
//             globalVar.bulletlimit = 30;
//             globalVar.bulletlimitSecondery = 30;
//             globalVar.bulletlimitSpecial = 30;
//             globalVar.currentBulletIndex = globalVar.bulletlimit;
//             globalVar.isReloading = false;
//             globalVar.loaded = false;
//             globalVar.currentAlpha = 0;
//             globalVar.instantiateEnemy = true;
//             globalVar.heroIsHitting = false;
//             globalVar.isPause = false;
//             globalVar.enemyRemain = globalVar.totalIncomingEnemy;
//             globalVar.heroLife = 100f;
//             globalVar.totalMagaZine = 5;
//             globalVar.bornEnemy.RemoveAll(deleteAll);
//             // globalVar.bulletGraphicsPosX = 0;
//         }

//         public void Fire(UnityEngine.Vector2 screenPosition)
//         {
//             Bool = true;
//             RaycastHit2D hit = Physics2D.Raycast(cameraMain.ScreenToWorldPoint(screenPosition), Vector2.zero);
//             if (primarySelected)
//             {
//                 Collider2D[] affectedEnemy = Physics2D.OverlapCircleAll(cameraMain.ScreenToWorldPoint(screenPosition), 1.00f);
//                 Hero.fire(cameraMain.ScreenToWorldPoint(screenPosition));
    
//                 for (int i = 0; i < affectedEnemy.Length; i++)
//                 {
//                     GameObject touchedObject = affectedEnemy[i].transform.gameObject;
//                     if (touchedObject.tag == "Enemy")
//                     {

//                         Enemy = touchedObject.GetComponent<EnemyEntity>();
//                         if (Enemy)
//                         {
//                             Enemy.GetHit(10);
//                         }
//                     }
//                 }
//             }
//             else if (secondarySelected)
//             {
//                 if (globalVar.currentIndex < globalVar.bulletlimit && hit.collider != null && hit.transform.gameObject.transform.name != "TopMenu" || !hit.collider)
//                 {
//                     Hero.fire(cameraMain.ScreenToWorldPoint(screenPosition));
//                     if (hit.collider != null && hit.transform.gameObject.tag == "Enemy")
//                     {
//                         GameObject touchedObject = hit.transform.gameObject;
//                         //IgnoreRaycast(touchedObject);
//                         int LayerDefault = LayerMask.NameToLayer("Default");
//                         touchedObject.layer = LayerDefault;
//                         Vector3 ScrennCoordinates = new Vector3(screenPosition.x, screenPosition.y, cameraMain.nearClipPlane);
//                         Vector3 WorldCoordinates = cameraMain.ScreenToWorldPoint(ScrennCoordinates);
//                         WorldCoordinates.z = 0;
//                         RaycastHit2D HitObject = Physics2D.Raycast(Hero.FirePoint.transform.position, WorldCoordinates - Hero.FirePoint.transform.position);
//                         // Debug.DrawRay(Hero.FirePoint.transform.position, Hero.FirePoint.transform.position - WorldCoordinates, Color.green);
//                         if (HitObject.collider != null)
//                         {
//                             GameObject touchedObject_2 = HitObject.transform.gameObject;
//                             if (touchedObject_2.tag == "Enemy")
//                             {

//                                 Enemy = touchedObject_2.GetComponent<EnemyEntity>();
//                                 if (Enemy)
//                                 {
//                                     Enemy.GetHit(10);
//                                 }
//                             }
//                         }
//                     }
//                 }
//             }
//             else if (specialSelected)
//             {
//                 Collider2D[] affectedEnemy = Physics2D.OverlapCircleAll(cameraMain.ScreenToWorldPoint(screenPosition), 2.00f);
//                 Hero.fire(cameraMain.ScreenToWorldPoint(screenPosition));
//                 for (int i = 0; i < affectedEnemy.Length; i++)
//                 {
//                     GameObject touchedObject = affectedEnemy[i].transform.gameObject;
//                     if (touchedObject.tag == "Enemy")
//                     {

//                         Enemy = touchedObject.GetComponent<EnemyEntity>();
//                         if (Enemy)
//                         {
//                             Enemy.GetHit(100);
//                         }
//                     }
//                 }
//             }

//             if ((hit.collider != null && hit.transform.gameObject.transform.name != "TopMenu") || !hit.collider)
//             {

//                 PlayManagerScript.fire();
//                 globalVar.currentIndex++;
//                 globalVar.currentBulletIndex--;
//             }

//             if (globalVar.currentIndex == globalVar.bulletlimit || globalVar.currentBulletIndex == 0)
//             {
//                 globalVar.currentIndex = 0;
//                 globalVar.currentBulletIndex = globalVar.bulletlimit;
//                 globalVar.isReloading = true;
//             }
//         }

//         public void TouchRelease(UnityEngine.Vector2 screenPosition)
//         {
//             FollowRaycast();
//         }

//         public void IgnoreRaycast(GameObject touchedObject)
//         {
//             foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
//             {
//                 if (gameObj.name == "Enemy(Clone)" && !GameObject.ReferenceEquals(gameObj, touchedObject))
//                 {
//                     int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
//                     gameObj.layer = LayerIgnoreRaycast;
//                 }
//             }
//         }
//         public void FollowRaycast()
//         {
//             foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
//             {
//                 if (gameObj.name == "Enemy(Clone)")
//                 {
//                     int LayerDefault = LayerMask.NameToLayer("Default");
//                     gameObj.layer = LayerDefault;
//                 }
//             }
//         }

//         void OnEnable()
//         {

//         }

//         private static bool deleteAll(string i)
//         {
//             return (i != null);
//         }

//         void pause()
//         {

//         }

//         void resume()
//         {

//         }
//         void gotoMenu()
//         {
//             DetachedEvent();
//             SceneManager.LoadScene("loader");
//             manager.switchState(new LoaderState(manager, globalVar, "Menu"));
//         }


//         public void getData()
//         {
//             MenuButtonObject = GameObject.Find("MenuButton");
//             if (MenuButtonObject)
//             {
//                 MenuButton = MenuButtonObject.GetComponent<Button>();
//                 MenuButton.onClick.AddListener(this.gotoMenu);
//             }
//             cameraMain = Camera.main;
//             HeroObject = GameObject.Find("Hero");
//             if (HeroObject)
//             {
//                 Hero = HeroObject.GetComponent<HeroEntity>();
//                 Hero.GlobalVar = globalVar;
//                 manager.OnStartTouch += Fire;
//                 manager.OnEndTouch += TouchRelease;

//                 PlayManagerObject = GameObject.Find("PlayManager");
//                 if (PlayManagerObject)
//                 {
//                     PlayManagerScript = PlayManagerObject.GetComponent<PlayManager>();
//                     PlayManagerScript.globalVar = globalVar;
//                     PlayManagerScript.enemyRemain = globalVar.totalIncomingEnemy;
//                     PlayManagerScript.HeroAfterLoad = HeroObject;
//                     PlayManagerScript.Hero = Hero;
//                     PlayManagerScript.instantiateEnemy();
//                 }
//             }
//             GunSelection = GameObject.Find("GunSelectionGroup").GetComponent<ToggleGroup>();
//             machineGun = GameObject.Find("Machine Gun").GetComponent<Toggle>();
//             shotGun = GameObject.Find("Shot Gun").GetComponent<Toggle>();
//             specialGun = GameObject.Find("Special Gun").GetComponent<Toggle>();
//             if (machineGun)
//             {
//                 machineGun.onValueChanged.AddListener(delegate
//                 {
//                     ToggleGuns(machineGun);
//                 });
//             }
//             if (shotGun)
//             {
//                 shotGun.onValueChanged.AddListener(delegate
//                 {
//                     ToggleGuns(shotGun);
//                 });

//             }
//             if (specialGun)
//             {
//                 specialGun.onValueChanged.AddListener(delegate
//                 {
//                     ToggleGuns(specialGun);
//                 });
//             }
//             machineGun.enabled = true;
//             primarySelected = true;
//             secondarySelected = false;
//             specialSelected = false;
//             globalVar.selectedWeapon = "Primary";
//         }

//         void ToggleGuns(Toggle toggleData)
//         {
//             Debug.Log(toggleData.name);
//             if (toggleData.name == "Machine Gun")
//             {
//                 if (toggleData.isOn)
//                 {
//                     primarySelected = true;
//                     secondarySelected = false;
//                     specialSelected = false;
//                     globalVar.selectedWeapon = "Primary";
//                 }
//                 else
//                 {
//                     primarySelected = false;
//                 }
//             }
//             else if (toggleData.name == "Shot Gun")
//             {
//                 if (toggleData.isOn)
//                 {
//                     secondarySelected = true;
//                     primarySelected = false;
//                     specialSelected = false;
//                     globalVar.selectedWeapon = "Secondary";
//                 }
//                 else
//                 {
//                     secondarySelected = false;
//                 }
//             }
//             else if (toggleData.name == "Special Gun")
//             {
//                 if (toggleData.isOn)
//                 {
//                     specialSelected = true;
//                     primarySelected = false;
//                     secondarySelected = false;
//                     globalVar.selectedWeapon = "Special";
//                 }
//                 else
//                 {
//                     specialSelected = false;
//                 }
//             }
//             if (!primarySelected && !secondarySelected && !specialSelected)
//             {
//                 primarySelected = true;
//                 machineGun.enabled = true;
//             }
//         }

//         void decorateRound()
//         {


//         }
//         void heroForward()
//         {

//         }
//         void heroBackward()
//         {

//         }

//         GameObject FindInActiveObjectByName(string name)
//         {
//             Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
//             for (int i = 0; i < objs.Length; i++)
//             {
//                 if (objs[i].hideFlags == HideFlags.None)
//                 {
//                     if (objs[i].name == name)
//                     {
//                         return objs[i].gameObject;
//                     }
//                 }
//             }
//             return null;
//         }
//         public void stateUpdate()
//         {
//             if (globalVar.enemyRemain == 0 && globalVar.isWon && globalVar.bornEnemy.Count == 0)
//             {
//                 delay += Time.deltaTime;
//                 if (delay * 10 > 30)
//                 {
//                     wonTheRound();
//                 }
//             }
//             if (globalVar.heroLife <= 0)
//             {
//                 Time.timeScale = 0;
//                 lostRound();
//             }
//         }

//         void wonTheRound()
//         {
//             DetachedEvent();
//             globalVar.isWon = false;
//             SceneManager.LoadScene("Win");
//             manager.switchState(new WonState(manager, globalVar));
//             // yield return null;
//         }

//         void lostRound()
//         {
//             DetachedEvent();
//             SceneManager.LoadScene("Lost");
//             manager.switchState(new LostState(manager, globalVar));
//             // yield return null;
//         }

//         public void switchState()
//         {
//             SceneManager.LoadScene("loader");
//             manager.switchState(new LoaderState(manager, globalVar, "MenuState"));

//             // SceneManager.LoadScene("menuState");
//             // manager.switchState(new menuState(manager,globalVar));
//         }
//         public string getStateName()
//         {
//             return this.stateName;
//         }
//         public void action(string action)
//         {

//         }
//         public void DetachedEvent()
//         {
//             manager.OnEndTouch -= TouchRelease;
//             manager.OnStartTouch -= Fire;
//         }
//         ~PlayState()
//         {
//             DetachedEvent();
//         }
//     }
// }


