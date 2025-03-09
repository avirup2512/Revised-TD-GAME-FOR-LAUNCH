using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhokaEntity : MonoBehaviour
{
    Rigidbody2D rb;
    float rorationDegree;
    int yValue;
    int xValue;
    // Start is called before the first frame update
    void Start()
    {
        yValue = Random.Range(6, 10);
        xValue = Random.Range(-1, 1);
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(xValue, yValue);
        rorationDegree = Random.Range(-100, 100);
    }
    void OnBecameInvisible()
    {
        rb.velocity = new Vector2(0, 0);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rorationDegree * Time.deltaTime);
    }
}
