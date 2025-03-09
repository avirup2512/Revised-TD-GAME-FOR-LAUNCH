using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpEntity : MonoBehaviour
{
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.transform.Find("Modal Overlay").transform.Find("Modal Background").GetComponent<RectTransform>();
    }
    public void pop()
    {
        gameObject.SetActive(true);
        rect = gameObject.transform.Find("Modal Overlay").transform.Find("Modal Background").GetComponent<RectTransform>();
        StartCoroutine("popUpCoRoutine");
    }
    IEnumerator popUpCoRoutine()
    {
        for (float f = 0f; f < 1.5f; f += 0.20f)
        {
            rect.localScale = new Vector3(f, f, 900);
            yield return new WaitForSeconds(0.01f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (rect.localScale.x > 1.3)
        {
            StopCoroutine("popUpCoRoutine");
            Time.timeScale = 0;
        }
    }
}


