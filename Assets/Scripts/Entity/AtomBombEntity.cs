using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomBombEntity : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer renderer;
    public Rigidbody2D rb;
    public GameObject BasePosition;
    private bool blasted = false;
    private Transform oldPosition;
    public AudioSource artilerySound;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        oldPosition = gameObject.transform;
    }

    private void Blast()
    {
        anim.SetBool("isBlast", true);
        if (artilerySound != null)
            artilerySound.Stop();
        rb.velocity = new Vector2(0, 0);
        blasted = true;
        // gameObject.transform.position = oldPosition.position;
        Collider2D[] affectedEnemy = Physics2D.OverlapCircleAll(gameObject.transform.position, 10.00f);
        // Hero.fire(targetPosition);
        for (int i = 0; i < affectedEnemy.Length; i++)
        {
            GameObject touchedObject = affectedEnemy[i].transform.gameObject;
            if (touchedObject.tag == "Enemy")
            {

                EnemyEntity Enemy = touchedObject.GetComponent<EnemyEntity>();
                if (Enemy)
                {
                    Enemy.GetHit(4000);
                }
            }
        }
    }

    public void go()
    {
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
        rb.velocity = Vector2.down * 10f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!blasted && transform.position.y < BasePosition.transform.position.y)
        {
            Blast();
        }
    }
}
