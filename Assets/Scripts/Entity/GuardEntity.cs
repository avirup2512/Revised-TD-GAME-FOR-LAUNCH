using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using Assets.Script.globalVar;
using UnityEngine.UI;

public class GuardEntity : MonoBehaviour
{
    public GameObject GuardLifeBar;
    private Slider LifeBar;
    private float GuardLife = 100f;
    public GlobalData globalVar;
    public Guard selectedGuard;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (GuardLifeBar)
        {
            GuardLifeBar.SetActive(true);
            LifeBar = GuardLifeBar.GetComponent<Slider>();
            LifeBar.maxValue = 100f;
            LifeBar.value = GuardLife;
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (globalVar != null && globalVar.guardIsHitting)
        {
            GuardLife = GuardLife -= .2f;
            LifeBar.value = GuardLife;
        }

        if (GuardLife < 0)
        {
            globalVar.GuardIsAdded = false;
            globalVar.guardIsHitting = false;
            gameObject.SetActive(false);
        }
    }
}
