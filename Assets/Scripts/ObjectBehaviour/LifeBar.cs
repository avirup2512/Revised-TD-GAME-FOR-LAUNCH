using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.globalVar;
public class LifeBar : MonoBehaviour
{

    private CanvasGroup group;
    private bool fadeInRunning = false;
    public bool hitting = false;
    private int f;
    private float currentAlpha = 0f;
    // Start is called before the first frame update
    void Start()
    {
        group = gameObject.GetComponent<CanvasGroup>();
        group.alpha = 0;
    }

    public IEnumerator fadeInCoroutine()
    {
        currentAlpha = group.alpha;
        // Debug.Log(group.alpha);
        fadeInRunning = true;
        for (float f = currentAlpha; f <= 1; f += 0.01f)
        {
            group.alpha = f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator fadeOutCoroutine()
    {
        fadeInRunning = false;
        // currentAlpha = 0;
        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            group.alpha = f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void fadeIn()
    {
        if (!fadeInRunning)
            StopCoroutine("fadeOutCoroutine");
        StartCoroutine("fadeInCoroutine");
    }

    public IEnumerator waitForFadeOut()
    {
        hitting = false;
        for (int i = 100; i > 0; i--)
        {
            f = i;
            yield return new WaitForSeconds(0.1f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (hitting)
        {
            f = 100;
            StopCoroutine("waitForFadeOut");
            StartCoroutine("waitForFadeOut");
        }
        // Debug.Log(f);
        if (f <= 1)
        {
            StopCoroutine("waitForFadeOut");
        }
        if (group.alpha > .7 && f < 85)
        {
            StopCoroutine("fadeInCoroutine");
            if (fadeInRunning)
            {
                StartCoroutine("fadeOutCoroutine");
            }

        }
        if (group.alpha == 0)
        {
            StopCoroutine("fadeOutCoroutine");
        }
    }
}

