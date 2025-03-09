using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RadialProgressEntity : MonoBehaviour
{
    public Image image;
    float speed = 5;
    float currentValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }
    public void startLoader()
    {
        currentValue = 20;
    }
    public void resetCurrentvalue(float value)
    {
        currentValue = value;
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = currentValue / 20;
    }
}
