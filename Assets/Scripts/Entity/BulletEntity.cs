using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.globalVar;
using Assets.Script.entity;
public class BulletEntity : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    private SpriteRenderer renderer;
    public bool fadeOutStarted = false;
    public Vector3 endPosition;
    public Vector2 startPosition;
    private bool blasted = false;
    private bool repositioned = false;
    private bool startCheckTime = false;
     float angle;

    public string Weapon;
    float time;
    public bool FROMENEMY = false;
    public AudioSource luncherBlast;
    void Start()
    {
       
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void go(Vector2 postion)
    {
        gameObject.SetActive(true);
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        repositioned = false;
        blasted = false;
        time = 0.0f;
        rb.velocity = new Vector3(postion.x - transform.position.x, postion.y - transform.position.y) * 5.5f;
       
    }

    void OnBecameInvisible()
    {

    }
    void reposition()
    {
        gameObject.transform.position = new Vector3(startPosition.x, startPosition.y, transform.position.z);
        anim.SetBool("Blast", false);
        repositioned = true;
        gameObject.SetActive(false);
    }
    IEnumerator Blast()
    {

        yield return new WaitForSeconds(1f);
    }
    void EnemyHit(Vector2 targetPosition)
    {
        if (Weapon == "Special")
        {
            Collider2D[] affectedEnemy = Physics2D.OverlapCircleAll(targetPosition, 2.00f);
            for (int i = 0; i < affectedEnemy.Length; i++)
            {
                GameObject touchedObject = affectedEnemy[i].transform.gameObject;
                if (touchedObject.tag == "Enemy")
                {

                    EnemyEntity enemy = touchedObject.GetComponent<EnemyEntity>();
                    if (enemy)
                    {
                        enemy.GetHit(100);
                    }
                }
                if (touchedObject.tag == "LifeSaver")
                {

                    LifeSaverEntity lifesaver = touchedObject.GetComponent<LifeSaverEntity>();
                    if (lifesaver)
                    {
                        lifesaver.GetHit();
                    }
                }

            }
        }
        else
        {
            Collider2D[] affectedEnemy = Physics2D.OverlapCircleAll(targetPosition, 1.00f);
            for (int i = 0; i < affectedEnemy.Length; i++)
            {
                GameObject touchedObject = affectedEnemy[i].transform.gameObject;
                if (touchedObject.tag == "Enemy")
                {
                    EnemyEntity enemy = touchedObject.GetComponent<EnemyEntity>();
                    if (enemy)
                    {
                        enemy.GetHit(10);
                    }
                }
                if (touchedObject.tag == "LifeSaver")
                {
                    LifeSaverEntity lifesaver = touchedObject.GetComponent<LifeSaverEntity>();
                    if (lifesaver)
                    {
                        lifesaver.GetHit();
                    }
                }
                if (touchedObject.tag == "Hero")
                {
                    HeroEntity hero = touchedObject.GetComponent<HeroEntity>();
                    if (hero)
                    {
                        hero.GetHit(0.2f);
                    }
                }
            }
        }
    }
    void blastBullet()
    {
        StartCoroutine("Blast");
        blasted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.rotation = Quaternion.LookRotation(rb.velocity);
        if (startCheckTime)
        {
            time += Time.deltaTime;
        }
        if (!FROMENEMY)
        {
            if (transform.position.x > endPosition.x && !blasted)
            {
                anim.SetBool("Blast", true);
                if (Weapon == "Special")
                    luncherBlast.Play();
                rb.velocity = new Vector2(0, 0);
                blasted = true;
                startCheckTime = true;
                EnemyHit(endPosition);
            }
        }
        else
        {
            if (transform.position.x < endPosition.x && !blasted)
            {
                anim.SetBool("Blast", true);
                rb.velocity = new Vector2(0, 0);
                blasted = true;
                startCheckTime = true;
                EnemyHit(endPosition);
            }
        }


        if (time > 0.5f && !repositioned)
        {
            reposition();
            startCheckTime = false;
        }
    }
}
