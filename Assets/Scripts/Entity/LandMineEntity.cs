using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using Assets.Script.globalVar;
using UnityEngine.UI;

public class LandMineEntity : MonoBehaviour
{
    private Animator anim;
    public SpriteRenderer renderer;
    public bool fadeOutStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        Sprite s = Resources.Load("Prefabs/Guard/LandMine/GuardSprite", typeof(Sprite)) as Sprite;
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = s;
    }
    private void deActivate()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D Obj)
    {
        if (Obj.gameObject.tag == "Enemy")
        {
            anim.SetTrigger("isBlast");
            if (!fadeOutStarted)
                startFadingOut();

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
    // Update is called once per frame
    public void startFadingOut()
    {
        StartCoroutine("fadeOut");
        Collider2D[] affectedEnemy = Physics2D.OverlapCircleAll(transform.position, 2.00f);
        // Hero.fire(targetPosition);
        for (int i = 0; i < affectedEnemy.Length; i++)
        {
            GameObject touchedObject = affectedEnemy[i].transform.gameObject;
            if (touchedObject.tag == "Enemy")
            {

                EnemyEntity Enemy = touchedObject.GetComponent<EnemyEntity>();
                if (Enemy)
                {
                    Enemy.GetHit(100);
                }
            }
        }
        fadeOutStarted = true;
    }
    void Update()
    {
        if (renderer.color.a < .1)
        {
            StopCoroutine("fadeOut");
            deActivate();
        }
    }
}
