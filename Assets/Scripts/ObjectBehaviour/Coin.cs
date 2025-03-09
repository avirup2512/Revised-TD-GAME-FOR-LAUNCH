using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.globalVar;
public class Coin : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector3 lifeBarPos;
    private bool go = false;
    private bool roTate = true;
    public GlobalData globalVar;
    private SpriteRenderer renderer;
    private bool reposFlag = false;

    private int randNum;
    // Start is called before the first frame update
    void Start()
    {
        Sprite spriteImage1 = Resources.Load("dynamicSprite/food", typeof(Sprite)) as Sprite;
        Sprite spriteImage2 = Resources.Load("dynamicSprite/coin", typeof(Sprite)) as Sprite;
        randNum = Random.Range(4, 10);

        renderer = gameObject.GetComponent<SpriteRenderer>();
        if (randNum > 5)
        {
            renderer.sprite = spriteImage1;
        }
        else if (randNum < 5)
        {
            renderer.sprite = spriteImage2;
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void setGlobalVar(GlobalData globalVarRef)
    {
        this.globalVar = globalVarRef;
    }

    void OnMouseDown()
    {
        // // Debug.Log(go);
        // // go = true; 
        // // roTate = false; 
        // if (randNum > 5)
        // {
        //     if (globalVar.heroLife < 100)
        //     {
        //         globalVar.heroLife += 10;
        //     }
        // }
        // else if (randNum < 5)
        // {
        //     globalVar.totalMagaZine++;
        // }
        // goToDestination();
    }

    void goToDestination()
    {
        // Debug.Log(globalVar);
        // go = false;
        // roTate = false;   
        // transform.Rotate(0,0,0);
        rb.velocity = new Vector2(lifeBarPos.x, lifeBarPos.y) * 2f;
    }
    void reposition()
    {
        reposFlag = true;
        rb.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // if(!go && roTate){
        // 	transform.Rotate(0,6*10f*Time.deltaTime,0);		
        // }else if(go && !roTate){
        // 	goToDestination();
        // }    
        if (transform.position.x < lifeBarPos.x && !reposFlag)
        {
            reposition();
        }
    }
}
