using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.globalVar;
public class TextIndicatorEntity : MonoBehaviour
{
    Rigidbody2D rb;
    private SpriteRenderer renderer;
    public int roundNumber;
    void Start()
    {
        
    }
    public void init(){
        Sprite numbers = Resources.Load("Prefabs/Number/number"+roundNumber, typeof(Sprite)) as Sprite;
        rb = gameObject.GetComponent<Rigidbody2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = numbers;
        rb.velocity = new Vector2(0, 1) * 1.5f;
        StartCoroutine("fadeOut");
    }
    void OnBecameInvisible()
    {

        // gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(renderer)
            if (renderer.color.a < .1)
            {
                StopCoroutine("fadeOut");
                deActivate();
            }
    }
    private void deActivate()
    {
        gameObject.SetActive(false);
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

}
