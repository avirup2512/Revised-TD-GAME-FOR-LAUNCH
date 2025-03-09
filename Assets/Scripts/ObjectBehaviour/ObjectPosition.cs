using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.globalVar;

public class ObjectPosition : MonoBehaviour
{
    Vector3 pos;
    private GameObject[] allBullet;
    // Start is called before the first frame update
    void Start()
    {
        float size = Camera.main.orthographicSize;
        if (gameObject.name == "heroLife")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3(0, -(size), 0));
        }
        else if (gameObject.name == "menuButton")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3((size * Camera.main.aspect - 1.5f), (size - .2f), 0));
        }
        else if (gameObject.name == "inComing")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3((size * Camera.main.aspect - 3f), (size - 1.8f), 0));
        }
        else if (gameObject.name == "bulletQuantityIndication")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3(-(size * Camera.main.aspect - 1.5f), (size - .9f), 0));

        }
        else if (gameObject.name == "loaderGraphics")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3(0, 0, 0));
        }
        else if (gameObject.name == "home")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3(-(size + 3 * Camera.main.aspect), -(size - .5f), 0));
        }
        else if (gameObject.name == "hero" || gameObject.name == "heroStartPos")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3(-(size + .8f * Camera.main.aspect), -(size - 0.5f), 0));

        }
        else if (gameObject.name == "backValley")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3(1.5f, 1, 0));
        }
        else if (gameObject.name == "windmill")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3(6f, -.8f, 0));
        }
        else if (gameObject.name == "sun")
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3(0, size - 2, 0));
        }
        else if (gameObject.name == "bulletIndication(Clone)")
        {
            allBullet = GameObject.FindGameObjectsWithTag("bullet");
            int a = System.Array.IndexOf(allBullet, gameObject);

            float xValue;

            if (a == 0)
            {
                xValue = -(size * Camera.main.aspect);
                pos = Camera.main.WorldToViewportPoint(new Vector3(xValue, size - 1, 10));
            }
            else
            {
                xValue = allBullet[a - 1].transform.position.x;
                pos = Camera.main.WorldToViewportPoint(new Vector3(xValue + .3f, size - 1, 10));
            }

        }
        else
        {
            pos = Camera.main.WorldToViewportPoint(new Vector3(0, -(size - 1), 0));
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
