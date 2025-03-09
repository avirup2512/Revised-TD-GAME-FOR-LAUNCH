using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.globalVar;
public class CoinEntity : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject CoinnumberIndicator;
    Animator anim;
    float time;
    private SpriteRenderer renderer;
    public int activeRoundNumber;
    public TextIndicatorEntity script;
    public bool fadeOutStarted = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Spin", true);
        //gameObject.transform.parent = GameObject.Find("Canvas").transform;
    }
    public void init (){
        renderer = gameObject.GetComponent<SpriteRenderer>();
        CoinnumberIndicator = Resources.Load("Prefabs/Number/TextIndicatorSprite") as GameObject;
    }
    public void go(Vector2 postion)
    {
        GameObject number = Instantiate(CoinnumberIndicator);
        script = number.GetComponent<TextIndicatorEntity>();
        script.roundNumber = activeRoundNumber;
        script.init();
        number.transform.position = gameObject.transform.position;
        rb.velocity = new Vector2(postion.x - transform.position.x, postion.y - transform.position.y) * 2f;
    }
    void OnBecameInvisible()
    {
        rb.velocity = new Vector2(0, 0);
        deactivate();
    }
    void deactivate()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
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
        fadeOutStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(renderer){
             time += Time.deltaTime;

            if (time > 2.5 && !fadeOutStarted)
                startFadingOut();
            
            if (renderer.color.a < .1)
            {
                StopCoroutine("fadeOut");
                deactivate();
            }
        }
    }

}
