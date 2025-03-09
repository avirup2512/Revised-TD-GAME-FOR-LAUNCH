using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Assets.Script.globalVar;
public class EnemyEntity : MonoBehaviour
{

    public Vector3 startPos;
    public float heroPositionX;
    private Rigidbody2D rb;
    private Animator anim;
    private float Life;

    private GameObject LifeSliderObject;
    private Slider LifeSlider;

    private Transform coinObject;


    private Transform lifeCanvas;
    private LifeBar life;

    private bool dead = false;

    public GlobalData globalVar;

    private SpriteRenderer renderer;

    private bool invisible = true;

    private bool heroIsHitting = false;
    private bool guardIsHitting = false;
    private bool heroIsHittingByBoss = false;

    public Vector3 heroLifePos;
    private Coin Coin;

    private GameObject baseGround;
    private RectTransform baseRect;

    private GameObject upperHeightLimit;
    private GameObject lowerHeightLimit;

    public string enemytag;

    private string enemyName;

    public bool mainEnemy;
    public GameObject floorBossStopPosition;

    public GameObject sourceOfFire;

    public Enemy enemyClass;
    public GameObject Hero;

    public GameObject coin;
    public GameObject coinInitiated;

    public GameObject CoinCanvas;
    private float enemyHeight;
    public GameObject[] EnemyBullet;
    public GameObject bulletsPrefab;
    public AudioSource enimyDie;
    public AudioSource EnemyISHIT;
    // Start is called before the first frame update
    void Start()
    {
        enemyHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        enemyName = gameObject.name;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);

        lifeCanvas = gameObject.transform.GetChild(0);
        life = lifeCanvas.gameObject.GetComponent<LifeBar>();

        renderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (enemyClass != null)
            Life = enemyClass.power;
        else
            Life = 200;
        if (Life == 0)
            Life = 1;

        anim = gameObject.GetComponent<Animator>();
        LifeSliderObject = transform.GetChild(0).GetChild(0).gameObject;
        LifeSlider = LifeSliderObject.GetComponent<Slider>();
        if (LifeSlider != null)
            LifeSlider.value = Life;

        coinObject = gameObject.transform.Find("coin");
        if (enemyClass.canFly)
        {
            StartCoroutine("MyUpdateForFlying");
        }
        else
        {
            go();
        }
        CoinCanvas = GameObject.Find("CoinCanvas");

        // if (enemyClass.canFire)
        // {
        //     bulletsPrefab = Resources.Load("Prefabs/Bullets/PrimaryBullets/Bullets") as GameObject;
        //     sourceOfFire = GameObject.Find("FireSource");
        //     EnemyBullet = new GameObject[30];
        //     for (int x = 0; x < 30; x++)
        //     {
        //         EnemyBullet[x] = GameObject.Instantiate(bulletsPrefab, sourceOfFire.transform) as GameObject;
        //         EnemyBullet[x].transform.localScale = new Vector3(.008f, .008f, .008f);
        //         EnemyBullet[x].SetActive(false);
        //     }

        // }
    }
    IEnumerator MyUpdateForFlying()
    {
        Vector2 oldPosition = new Vector2(transform.parent.gameObject.transform.position.x, transform.parent.gameObject.transform.position.y);
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            float speed = Random.Range(-1, 10);
            Vector2 direction = createFlyingEnemyDirection();
            rb.velocity = speed * direction;
        }
    }
    private Vector2 createFlyingEnemyDirection()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        if (direction.x > 0 || direction.y > 0)
        {
            direction = createFlyingEnemyDirection();
        }
        return direction;
    }
    public void GetHit(int impact)
    {
        if (!invisible)
        {
            int LifeImpact = impact;
            if (globalVar.selectedWeapon == "Primary" && !globalVar.isUniqueWeaponFire)
            {
                LifeImpact = globalVar.PrimaryWeaponSelected.power * (globalVar.PrimaryWeaponSelected.level);
            }
            else if (globalVar.selectedWeapon == "Secondary" && !globalVar.isUniqueWeaponFire)
            {
                LifeImpact = globalVar.SecondaryWeaponSelected.power * (globalVar.SecondaryWeaponSelected.level);
            }
            else if (globalVar.selectedWeapon == "Special" && !globalVar.isUniqueWeaponFire)
            {
                LifeImpact = globalVar.SpecialWeaponSelected.power * (globalVar.SpecialWeaponSelected.level);
            }
            else if (globalVar.selectedWeapon == "Bomb" && !globalVar.isUniqueWeaponFire)
            {
                LifeImpact = globalVar.BombWeaponSelected.power * (globalVar.BombWeaponSelected.level);
            }
            EnemyISHIT.Play();
            if (LifeSlider)
            {
                // anim.SetBool("isHurt", true);
                life.fadeIn();
                life.hitting = true;
                LifeSlider.value = LifeSlider.value - ((LifeImpact / Life) * 100);
                Life = Life - LifeImpact;
            }
        }

    }

    public void go()
    {
        rb.velocity = new Vector2(heroPositionX, transform.position.y) * 0.5f;
    }

    public void stop()
    {
        StopCoroutine("MyUpdateForFlying");
        rb.velocity = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D Obj)
    {
        if (Obj.gameObject.name == "bullet(Clone)" && globalVar.firstEnemy == null)
        {
            // Life -= 20;
            // LifeSlider.value = Life;
            // anim.SetBool("isHurt", true);
            // life.fadeIn();
            // life.hitting = true;
            // life.hitting = false;
            // anim.SetBool("isHurt", false);
        }
        else if (Obj.gameObject.name == "Hero")
        {
            heroIsHitting = true;
            anim.SetBool("Attacking", true);
            stop();
        }
        else if (Obj.name == "FloorBossStopPosition" && mainEnemy)
        {
            stop();
            heroIsHittingByBoss = true;
            anim.SetBool("Attacking", true);
            sourceOfFire = GameObject.Find("FireSource");
            // if (sourceOfFire)
            //     Bombing(sourceOfFire, Hero);

        }
        else if (Obj.name == "Guard(Clone)")
        {
            stop();
            guardIsHitting = true;
            anim.SetBool("Attacking", true);
            // sourceOfFire = GameObject.Find("FireSource");
            // if (sourceOfFire)
            //     Bombing(sourceOfFire, Hero);

        }
        else if (Obj.name == "CanFireEnemyFirePosition" && enemyClass.canFire)
        {
            // stop();
            // heroIsHittingByBoss = true;
            // anim.SetBool("Attacking", true);
            // StartCoroutine("EnemyFire");

            // if (sourceOfFire)
            //     Bombing(sourceOfFire, Hero);

        }
        // if (Obj.name == "BaseLowerHeightLimit")
        // {
        //     Debug.Log("OYE OYE");
        //     gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + enemyHeight, gameObject.transform.position.z);
        // }
    }
    IEnumerator EnemyFire()
    {
        for (int x = 0; x < EnemyBullet.Length; x++)
        {
            BulletEntity bulEntity = EnemyBullet[x].GetComponent<BulletEntity>();
            bulEntity.endPosition = Hero.transform.position;
            bulEntity.startPosition = sourceOfFire.transform.position;
            bulEntity.FROMENEMY = true;
            bulEntity.go(Hero.transform.position);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerExit2D(Collider2D Obj)
    {
        if (Obj.name == "Guard(Clone)")
        {
            go();
            guardIsHitting = false;
            anim.SetBool("Attacking", false);
        }
    }

    public void Bombing(GameObject source, GameObject target)
    {
        GameObject bombObject = Resources.Load("Prefabs/bomb/bomb") as GameObject;
        GameObject bomb = GameObject.Instantiate(bombObject, source.transform);
        bomb.transform.SetParent(transform);
        BombEntity bombEntity = bomb.GetComponent<BombEntity>();
        bombEntity.sourceObject = source;
        bombEntity.targetObject = target;
    }


    void OnBecameVisible()
    {
        // heroLifePos = GameObject.Find("coinEndPosition");
        invisible = false;
        if (mainEnemy)
        {
            rb.velocity = new Vector2(heroPositionX, transform.position.y) * 0.1f;
        }

    }

    // private void OnTriggerExit2D(Collider2D bullet)
    // {
    // if(bullet.gameObject.name == "bullet(Clone)")
    // {
    // 	life.hitting = false;
    // }
    // }


    void die()
    {
        if (enimyDie != null)
        {
            enimyDie.Play();
        }
        if (mainEnemy)
        {
            globalVar.floorBossDied = true;
        }
        dead = true;
        stop();
        anim.SetTrigger("isDie");
        startFadingOut();
        globalVar.bornEnemy.Remove(enemytag);
        if (globalVar.bornEnemy.Count == 0)
        {
            globalVar.instantiateEnemy = true;
        }
        // CHECKING GLOBAl VAR'S heroIsHitting TRUE OR FALSE IF TRUE MAKE iT FALSE

        if (heroIsHitting)
        {
            heroIsHitting = false;
            globalVar.heroIsHitting = false;
        }

        heroIsHittingByBoss = false;
        globalVar.heroIsHittingByBoss = false;

        if (guardIsHitting)
        {
            guardIsHitting = false;
            globalVar.guardIsHitting = false;
        }

    }


    IEnumerator fadeOut()
    {
        for (float f = 1f; f >= -1f; f -= 0.01f)
        {
            Color c = renderer.color;
            c.a = f;
            renderer.color = c;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void startFadingOut()
    {
        StartCoroutine("fadeOut");
        if (coin != null)
            coinInitiated = Instantiate(coin) as GameObject;
        // Transform coinLayer = CoinCanvas.transform.Find("CoinLayer");
        // if (coinLayer != null)
        //     coinInitiated.transform.SetParent(coinLayer.transform);
        coinInitiated.transform.position = gameObject.transform.position;
    }

    private void deActivate()
    {
        // Debug.Log(gameObject.activeSelf);
        //if(!coinObject.gameObject.activeSelf){
        gameObject.SetActive(false);
        // gameObject.transform.position = startPos;
        GameObject coinObject = Resources.Load("attackPrefab/coin") as GameObject;
        // coinObject.transform.position = gameObject.transform.position;
        // GameObject coin = GameObject.Instantiate(coinObject);
        // Coin = coin.GetComponent<Coin>();
        // Coin.setGlobalVar(globalVar);
        // Coin.lifeBarPos = heroLifePos;
        //}
    }



    // Update is called once per frame
    void Update()
    {
        // CHECKING HERO IS HITTING OR NOT 
        if (heroIsHitting)
        {
            globalVar.heroIsHitting = heroIsHitting;
        }
        if (heroIsHittingByBoss)
        {
            globalVar.heroIsHittingByBoss = heroIsHittingByBoss;
        }
        if (guardIsHitting)
        {
            globalVar.guardIsHitting = guardIsHitting;
        }
        // // Debug.Log(dead);
        if (renderer.color.a < .1)
        {
            StopCoroutine("fadeOut");
            deActivate();
        }

        // if (transform.position.x < heroPositionX + 3)
        // {
        //     rb.velocity = new Vector2(0, 0);
        //     if (!dead)
        //     {
        //         heroIsHitting = true;
        //     }
        // }
        // if (gameObject.transform.position.y < lowerHeightLimit.transform.position.y || gameObject.transform.position.y > upperHeightLimit.transform.position.y)
        // {
        //     var randomYPosition = Random.Range(-2.00f, 2.00f);
        //     gameObject.transform.position = new Vector3(gameObject.transform.position.x, randomYPosition, gameObject.transform.position.z);
        // }

        if (Life == 0 && !dead || Life < 0 && !dead)
        {
            die();
        }
    }

}

