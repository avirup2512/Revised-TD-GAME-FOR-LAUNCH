using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    int LifeAmount;
    public Text LifeText;
    public Slider LifeSlider;
    public GameObject LifeSliderObject;

    // Start is called before the first frame update
    void Start()
    {
        LifeSliderObject = transform.GetChild(0).GetChild(0).gameObject;
        if (LifeSliderObject)
        {
            LifeSlider = LifeSliderObject.GetComponent<Slider>();
        }

    }

    public void GetHit(int impact)
    {
        if (LifeSlider)
        {
            LifeSlider.value = LifeSlider.value - impact;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // LifeText.text = "Life: " + LifeSlider.value;
    }
}
