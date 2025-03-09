using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGunEntity : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    public bool Firing = false;
    public bool fireStarted = false;
    Rigidbody2D rb;
    public int counter = 0;
    Vector2 oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x;
        max = transform.position.x + 0.3f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        oldPosition = gameObject.GetComponent<Transform>().position;
        //rb.velocity = new Vector2(-max, transform.position.y);
    }
    void FireSecondary()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 10, max - min) + min, transform.position.y, transform.position.z);
        fireStarted = true;
    }

    public void FirePrimary()
    {
        transform.position = new Vector3(oldPosition.x, oldPosition.y, 99);
        rb.velocity = new Vector2(-max, transform.position.y);
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Firing)
            FireSecondary();

        if (transform.position.x > max)
        {
            if (counter == 2)
            {

                rb.velocity = new Vector2(max, transform.position.y) * 0.5f;
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
            counter++;
        }
        if (transform.position.x < max)
        {
            rb.velocity = new Vector2(-max, transform.position.y) * 0.5f;
        }
    }
}
