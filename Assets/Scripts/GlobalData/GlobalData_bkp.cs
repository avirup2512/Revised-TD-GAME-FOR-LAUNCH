// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using UnityEngine.Events;

// namespace Assets.Script.globalVar
// {
//     public class GlobalData
//     {
//         public int totalCoin = 0;
//         public int totalDiamond = 0;

//         public int currentIndex = 0;
//         public int bulletlimit = 0;
//         public int totalBullet = 0;
//         public int totalMagaZinePrimary = 0;
//         public int currentBulletIndex = 0;
//         public int primaryGunPower = 0;

//         public int currentIndexSecondary = 0;
//         public int bulletlimitSecondery = 0;
//         public int totalBulletSecondery = 0;
//         public int totalMagaZineSecondery = 0;
//         public int currentBulletIndexSecondery = 0;
//         public int secondaryGunPower = 0;

//         public int currentIndexSpecial = 0;
//         public int bulletlimitSpecial = 0; //special for scecial gun
//         public int totalBulletSpecial = 0;
//         public int totalMagaZineSpecial = 0;
//         public int currentBulletIndexSpecial = 0;
//         public int specialGunPower = 0;
//         public int currentIndexBomb = 0;
//         public int bulletlimitBomb = 0;
//         public int totalBulletBomb = 0;
//         public int totalMagaZineBomb = 0;
//         public int currentBulletIndexBomb = 0;
//         public int bombGunPower = 0;

//         public string primaryGunSprite = "Sprites/guns/selection/primary_1";
//         public string secondaryGunSprite = "Sprites/guns/selection/sec_1";
//         public string specialGunSprite = "Sprites/guns/selection/spec_1";
//         public string bombGunSprite = "Sprites/guns/selection/spec_1";

//         public string heroSpritePrimary = "Sprites/guns/herogun/handprimary_1";
//         public string heroSpriteSecondary = "Sprites/guns/herogun/handprimary_1";
//         public string heroSpriteSpecial = "Sprites/guns/herogun/handprimary_1";
//         public string heroSpriteBomb = "Sprites/guns/herogun/handprimary_1";

//         public string primaryMagazineSprite;
//         public string secondaryMagazineSprite;
//         public string specialMagazineSprite;
//         public string bombMagazineSprite;
//         public bool isUniqueWeaponFire = false;

//         public List<string> uniqueWeaponsLists = new List<string>();
//         public int uniqueWeaponsPower;
//         public int uniqueWeapon1BulletQuantity = 0;
//         public int uniqueWeapon2BulletQuantity = 0;
//         public int uniqueWeapon3BulletQuantity = 0;

//         public string uniqueWeapon1GunSprite = "Sprites/guns/selection/unique_1";
//         public string uniqueWeapon2GunSprite = "Sprites/guns/selection/unique_2";
//         public string uniqueWeapon3GunSprite = "Sprites/guns/selection/unique_3";

//         public bool isReloading = false;
//         public bool loaded = false;

//         public float currentAlpha = 0;

//         public bool isMute;

//         public bool instantiateEnemy = true;

//         public bool heroIsHitting = false;
//         public bool guardIsHitting = false;
//         public bool heroIsHittingByBoss = false;

//         public List<string> bornEnemy = new List<string>();

//         public Round activeRound;

//         public bool isPause = false;

//         public int totalIncomingEnemy = 15;
//         public int enemyRemain;

//         public bool isWon = false;

//         public List<string> round = new List<string>();

//         public string firstEnemy;

//         public float heroLife = 100f;
//         public bool isFiring = false;

//         public int CurrentLevel;
//         public int NextLevel;

//         public string selectedWeapon;

//         public List<Round> RoundList = new List<Round>();

//         public List<RoundGroup> RoundGroupList = new List<RoundGroup>();

//         [SerializeField] RoundGroupClass roundClass;

//         [SerializeField] public List<Enemy> Enemies = new List<Enemy>();

//         [SerializeField] public List<Weapon> WeaponList = new List<Weapon>();
//         [SerializeField] public List<Guard> GuardList = new List<Guard>();
//         [SerializeField] public List<Weapon> WeaponListForShopPage = new List<Weapon>();

//         [SerializeField] public RoundGroupClass MainRoundGroupClass;
//         public bool floorBossIntantiated = false;
//         public bool floorBossDied = false;

//         public SelectedWeapon selectedWeapons;
//         public UnityEvent bombExplosion = new UnityEvent();
//         public bool GuardInitiated = false;

//         public List<Guard> ExistingGuardList;
//         public bool GuardIsAdded = false;

//         public Weapon PrimaryWeaponSelected;
//         public Weapon SpecialWeaponSelected;
//         public Weapon SecondaryWeaponSelected;
//         public Weapon BombWeaponSelected;
//         public Weapon AtomBombWeaponSelected;
//         public Weapon ArtiliaryWeaponSelected;
//         public Weapon ThunderWeaponSelected;
//         public RoundGroupClass initializeAllRound()
//         {
//             WeaponList.Clear();
//             RoundGroupList = new List<RoundGroup>();
//             int WeaponInitiationCounter = 0;
//             for (int i = 0; i < 50; i++)
//             {
//                 if (i == 0 || i % 5 == 0)
//                 {
//                     int roundGroupCounter = (int)Mathf.Floor((i + 1) / 5) + 1;
//                     RoundGroup r = new RoundGroup(roundGroupCounter, new List<Round>(), new List<Enemy>(), new List<Weapon>(), 3 + (roundGroupCounter - 1));
//                     // RoundGroup r = new RoundGroup(roundGroupCounter, new List<Round>(), new List<Enemy>(), new List<Weapon>(), 1);
//                     List<int> unlockedEnemyNumber = new List<int>();
//                     if (roundGroupCounter == 1)
//                     {
//                         unlockedEnemyNumber.Add(1);
//                         unlockedEnemyNumber.Add(2);
//                     }
//                     // ADDING THREE ENEMIES IN EACH ROUND GROUP
//                     unlockedEnemyNumber.Add((roundGroupCounter * 2) + 1);
//                     unlockedEnemyNumber.Add((roundGroupCounter * 2) + 2);
//                     if (roundGroupCounter != 1)
//                     {
//                         unlockedEnemyNumber.Add((roundGroupCounter * 2) + 3);
//                     }
//                     for (int x = 0; x < unlockedEnemyNumber.Count; x++)
//                     {
//                         Enemy enemy = new Enemy("Enemy" + unlockedEnemyNumber[x], unlockedEnemyNumber[x], (110 * (i + 1)) * roundGroupCounter, roundGroupCounter, unlockedEnemyNumber[x] == 8 || unlockedEnemyNumber[x] == 11 || unlockedEnemyNumber[x] == 13 ? true : false, unlockedEnemyNumber[x] == 1 || unlockedEnemyNumber[x] == 11 || unlockedEnemyNumber[x] == 23 ? true : false);
//                         Enemies.Add(enemy);
//                         r.unlockedEnemies.Add(enemy);
//                     }
//                     if ((roundGroupCounter - 1) % 2 == 0)
//                     {
//                         WeaponInitiationCounter++;
//                     }
//                     int level = 1;
//                     int bulletPrice = WeaponInitiationCounter == 1 ? 0 : 350 * WeaponInitiationCounter;
//                     int bulletPerMag = 30;
//                     float magDelay = 2f;
//                     if(WeaponInitiationCounter == 2){
//                         bulletPerMag = 40;
//                         magDelay = 1.5f;
//                     }else if(WeaponInitiationCounter > 2){
//                         bulletPerMag = 60;
//                         magDelay = 1f;
//                     }
//                     Weapon primaryWeapon = new Weapon("primary" + WeaponInitiationCounter, "primary" + WeaponInitiationCounter,
//                                         500 * WeaponInitiationCounter,magDelay,
//                                         roundGroupCounter, "primary", "primary_" + WeaponInitiationCounter, 3, 2, 10, "NA", "NA",
//                                         "Sprites/guns/selection/primary_" + WeaponInitiationCounter,
//                                         "Prefabs/Bullets/bulletPrimary", "NA", false, bulletPrice, i == 0 ? true : false, WeaponInitiationCounter, level, 150 * WeaponInitiationCounter, 0);
//                     Weapon item = WeaponList.Find(x => x.name == "primary" + WeaponInitiationCounter);
//                     if (item == null)
//                     {
//                         WeaponList.Add(primaryWeapon);
//                     }
//                     r.unlockedWeapon.Add(primaryWeapon);
                    
//                     Weapon secondaryWeapon = new Weapon("secondary" + WeaponInitiationCounter, "secondary" + WeaponInitiationCounter,
//                                         800 * WeaponInitiationCounter,magDelay,
//                                         roundGroupCounter, "secondary", "secondary_" + WeaponInitiationCounter, 2, 2, bulletPerMag, "NA", "NA",
//                                         "Sprites/guns/selection/sec_" + WeaponInitiationCounter,
//                                         "Prefabs/Bullets/bulletPrimary", "NA", false, 900 * WeaponInitiationCounter, i == 0 ? true : false, WeaponInitiationCounter, level, 300 * WeaponInitiationCounter, 0);
//                     Weapon item2 = WeaponList.Find(x => x.name == "secondary" + WeaponInitiationCounter);
//                     if (item2 == null)
//                     {
//                         WeaponList.Add(secondaryWeapon);
//                     }
//                     r.unlockedWeapon.Add(secondaryWeapon);
//                     Weapon specialWeapon = new Weapon("special" + WeaponInitiationCounter, "special" + WeaponInitiationCounter,
//                                         1200 * WeaponInitiationCounter,magDelay,
//                                         roundGroupCounter, "special", "special_" + WeaponInitiationCounter, 0, 2, 8, "NA", "NA",
//                                         "Sprites/guns/selection/spec_" + WeaponInitiationCounter,
//                                         "Prefabs/Bullets/luncherBulets", "NA", false, 1200 * WeaponInitiationCounter, i == 0 ? true : false, WeaponInitiationCounter, level, 500 * WeaponInitiationCounter, 0);
//                     Weapon item3 = WeaponList.Find(x => x.name == "special" + WeaponInitiationCounter);
//                     if (item3 == null)
//                     {
//                         WeaponList.Add(specialWeapon);
//                     }

//                     r.unlockedWeapon.Add(specialWeapon);
//                     Weapon bombWeapon = new Weapon("bomb" + WeaponInitiationCounter, "bomb",
//                                         2500 * WeaponInitiationCounter,2,
//                                         roundGroupCounter, "bomb", "bomb_" + WeaponInitiationCounter, 0, 2, 0, "NA", "NA",
//                                         "Sprites/guns/selection/bomb_" + WeaponInitiationCounter,
//                                         "Prefabs/Bullets/bulletBomb", "NA", false, 345 * WeaponInitiationCounter, i == 0 ? true : false, WeaponInitiationCounter, level, 300 * WeaponInitiationCounter, 0);
//                     Weapon item4 = WeaponList.Find(x => x.name == "bomb" + WeaponInitiationCounter);
//                     if (item4 == null)
//                     {
//                         WeaponList.Add(bombWeapon);
//                     }
//                     r.unlockedWeapon.Add(bombWeapon);
//                     RoundGroupList.Add(r);
//                 }
//                 // CREATING MAIN ENEMY
//                 Enemy mainEnemy = new Enemy("mainEnemy" + RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber, RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber, 9000 * (i + 1) * RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber, RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber, (RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber == 6 || RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber == 8) ? true : false, false);
//                 bool isMainRound = i != 0 && (i + 1) % 5 == 0 ? true : false;
//                 Round rr = new Round("Round" + (i + 1).ToString(), i + 1, isMainRound, i == 0 ? false : true, 40 + (RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber * RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber), RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies, new List<Enemy>(), RoundGroupList[RoundGroupList.Count - 1].howManyTimesEnemyComes, RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber, mainEnemy);
//                 // Round rr = new Round("Round" + (i + 1).ToString(), i + 1, isMainRound, i == 0 ? false : true, 5, RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies, new List<Enemy>(), RoundGroupList[RoundGroupList.Count - 1].howManyTimesEnemyComes, RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber, mainEnemy);
//                 // RoundGroupList[RoundGroupList.Count - 1].howManyTimesEnemyComes
//                 // +(RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber * RoundGroupList[RoundGroupList.Count - 1].roundGroupNumber)
//                 RoundList.Add(rr);
//                 // FOR ROUND GROUP 1
//                 if (i < 5 && (i + 1) % 5 == 1)
//                 {
//                     rr.comingEnemy.Add(rr.unLockedEnemy[0]);
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies[1]);
//                 }
//                 if (i < 5 && (i + 1) % 5 == 2)
//                 {
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies[0]);
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies[1]);
//                 }
//                 else if (i < 5 && (i + 1) % 5 == 3)
//                 {
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies[1]);
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies[2]);
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies[3]);
//                 }
//                 else if (i < 5 && (i + 1) % 5 == 4)
//                 {
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies[1]);
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies[2]);
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 1].unlockedEnemies[3]);
//                 }
//                 else if (i < 5 && (i + 1) % 5 == 0)
//                 {
//                     rr.comingEnemy.Add(rr.unLockedEnemy[1]);
//                     rr.comingEnemy.Add(rr.unLockedEnemy[2]);
//                     rr.comingEnemy.Add(rr.unLockedEnemy[3]);
//                 }
//                 // FOR ROUND GROUP 1 ENDS
//                 if (i >= 5 && (i + 1) % 5 == 1)
//                 {
//                     // FIRST UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[0]);
//                     // LAST UNLOCK ENEMY OF PREVIOUS ROUND GROUP
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 2].unlockedEnemies[RoundGroupList[RoundGroupList.Count - 2].unlockedEnemies.Count - 1]);
//                     // PREVIOUS UNLOCKED ENEMY OF THE LAST UNLOCK ENEMY OF PREVIOUS ROUND GROUP
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 2].unlockedEnemies[RoundGroupList[RoundGroupList.Count - 2].unlockedEnemies.Count - 2]);
//                 }
//                 else if (i > 5 && (i + 1) % 5 == 2)
//                 {
//                     // FIRST UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[0]);
//                     // SECOND UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[1]);
//                     // LAST UNLOCK ENEMY OF PREVIOUS ROUND GROUP
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 2].unlockedEnemies[RoundGroupList[RoundGroupList.Count - 2].unlockedEnemies.Count - 1]);
//                 }
//                 else if (i > 5 && (i + 1) % 5 == 3)
//                 {
//                     // THIRD UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[2]);
//                     // SECOND UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[1]);
//                     // LAST UNLOCK ENEMY OF PREVIOUS ROUND GROUP
//                     rr.comingEnemy.Add(RoundGroupList[RoundGroupList.Count - 2].unlockedEnemies[RoundGroupList[RoundGroupList.Count - 2].unlockedEnemies.Count - 1]);
//                 }
//                 else if (i > 5 && (i + 1) % 5 == 4)
//                 {
//                     // FIRST UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[0]);
//                     // SECOND UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[1]);
//                     // THIRD UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[2]);
//                 }
//                 else if (i > 5 && (i + 1) % 5 == 0)
//                 {
//                     // FIRST UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[0]);
//                     // SECOND UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[1]);
//                     // THIRD UNLOCK ENEMY OF THIS ROUND GROUP
//                     rr.comingEnemy.Add(rr.unLockedEnemy[2]);
//                 }
//                 if (RoundGroupList[RoundGroupList.Count - 1] != null)
//                 {
//                     RoundGroupList[RoundGroupList.Count - 1].totalRounds.Add(RoundList[i]);
//                 }
//             }
//             // ADDING THREE TYPES OF GUARD STARTS
//             GuardList.Clear();
//             for (int x = 0; x < 3; x++)
//             {
//                 int price = 1000 * (x + 1);
//                 GuardType guardType = new GuardType("Wooden Guard", 100, "Prefabs/Guard/WoodenGuard/guardSprite", "Prefabs/Guard/WoodenGuard/Guard", true);
//                 if (x == 1)
//                 {
//                     guardType = new GuardType("Iron Guard", 500, "Prefabs/Guard/IronGuard/guardSprite", "Prefabs/Guard/IronGuard/Guard", true);
//                 }
//                 else if (x == 2)
//                 {
//                     guardType = new GuardType("Land Mine", 1000, "Prefabs/Guard/LandMine/guardSprite", "Prefabs/Guard/WoodenGuard/Guard", false);
//                 }
//                 Guard guard = new Guard(guardType, 2, price, false);
//                 GuardList.Add(guard);
//             }

//             // ADDING UNIQ WEAPON
//             Weapon uniqWeaponAtom = new Weapon("Atom", "Atom",
//                                     50 * 1,2,
//                                     0, "unique", "bomb_" + 1, 0, 2, 20, "NA", "NA",
//                                     "Sprites/guns/selection/unique_" + 1,
//                                     "Prefabs/Bullets/bulletPrimary", "NA", false, 5000, true, 1, 1, 209, 0);
//             WeaponList.Add(uniqWeaponAtom);
//             Weapon uniqWeaponArtiliary = new Weapon("Artiliary", "Artiliary",
//                                     50 * 1,2,
//                                     0, "unique", "bomb_" + 1, 0, 2, 60, "NA", "NA",
//                                     "Sprites/guns/selection/unique_" + 2,
//                                     "Prefabs/Bullets/bulletPrimary", "NA", false, 7000, true, 1, 1, 209, 0);
//             WeaponList.Add(uniqWeaponArtiliary);
//             Weapon uniqWeaponThunder = new Weapon("Thunder", "Thunder",
//                                     50 * 1,2,
//                                     0, "unique", "bomb_" + 1, 0, 2, 5, "NA", "NA",
//                                     "Sprites/guns/selection/unique_" + 3,
//                                     "Prefabs/Bullets/bulletPrimary", "NA", false, 10000, true, 1, 1, 209, 0);
//             WeaponList.Add(uniqWeaponThunder);
//             // ADDING THREE TYPES OF GUARDS ENDS
//             MainRoundGroupClass = new RoundGroupClass(RoundGroupList, 1000, 0, GuardList, WeaponList);
//             // string saveFilePath = Application.persistentDataPath + "/SavedGameData.json";
//             // string savePlayerData = JsonUtility.ToJson(MainRoundGroupClass);
//             // File.WriteAllText(saveFilePath, savePlayerData);
//             return MainRoundGroupClass;
//         }
//         public void setActiveRound(Round round)
//         {
//             this.activeRound = round;
//         }

//         public void setTotalIncomingEnemy(int enemyNum)
//         {
//             this.totalIncomingEnemy = enemyNum;
//         }

//     }

// }


