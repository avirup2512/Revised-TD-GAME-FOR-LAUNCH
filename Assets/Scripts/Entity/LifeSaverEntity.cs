using System.Collections;
using System.Collections.Generic;
using Assets.Script.entity;
using UnityEngine;
using Assets.Script.globalVar;
public class LifeSaverEntity : MonoBehaviour
{
    public float heroPositionX;
    public GlobalData globalVar;
    private Rigidbody2D rb;
    private Animator anim;
    public HeroEntity hero;
    public SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void GetHit()
    {
        anim.SetTrigger("isDie");
        rb.velocity = new Vector2(0, 0);
        globalVar.heroLife = 100;
        hero.LifeBar.value = globalVar.heroLife;
        startFadingOut();
    }
    public void go()
    {
        if (rb == null)
            rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(heroPositionX, 0) * 0.5f;
    }
    public void startFadingOut()
    {
        StartCoroutine("fadeOut");
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
    private void deActivate()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (renderer.color.a < .1)
        {
            StopCoroutine("fadeOut");
            deActivate();
        }
    }
}
