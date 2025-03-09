using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextIndicationEntity : MonoBehaviour
{
    RectTransform rect;
    private CanvasGroup textGroup;
    private CanvasGroup group;
    private float currentAlpha = 0f;
    private bool fadeOutRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        textGroup = gameObject.transform.GetChild(0).gameObject.GetComponent<CanvasGroup>();
        group = gameObject.GetComponent<CanvasGroup>();
        textGroup.alpha = 0;
        group.alpha = 0;
    }
    public void pop()
    {
        textGroup.alpha = 1;
        StartCoroutine("popUpCoRoutine");
        StartCoroutine("overlayFadeInCoRoutine");
    }
    IEnumerator popUpCoRoutine()
    {
        for (float f = 0f; f < 2.5f; f += 0.20f)
        {
            rect.localScale = new Vector3(f, f, 900);
            yield return new WaitForSeconds(0.03f);
        }

    }
    IEnumerator overlayFadeInCoRoutine()
    {
        for (float f = 0f; f <= 1; f += 0.20f)
        {
            group.alpha = f;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public IEnumerator fadeOutCoroutine()
    {
        fadeOutRunning = true;
        // currentAlpha = 0;
        for (float f = 1f; f >= 0f; f -= 0.008f)
        {
            textGroup.alpha = f;
            group.alpha = f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rect.localScale.x > 2.3 && !fadeOutRunning)
        {
            StopCoroutine("popUpCoRoutine");
            StartCoroutine("fadeOutCoroutine");
        }
        if (textGroup.alpha == 0)
        {
            StopCoroutine("fadeOutCoroutine");
        }
        if (group.alpha > .7)
        {
            StopCoroutine("overlayFadeInCoRoutine");
        }
    }
}
