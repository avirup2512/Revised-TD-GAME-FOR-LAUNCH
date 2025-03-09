using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using Assets.Script.state;
using UnityEngine.SceneManagement;
using Assets.Script.interfaces;
using TMPro;
using Assets.Script.globalVar;
public class RoundSelectionButton : MonoBehaviour
{

    public TextMeshProUGUI text;
    public Transform textObject;
    public string name = "";
    // Start is called before the first frame update
    void Start()
    {
        // textObject = GameObject.Find("Text");
        textObject = gameObject.transform.GetChild(0);
        text = textObject.GetComponent<TextMeshProUGUI>();
    }

    public void setName(Round round)
    {
        textObject = gameObject.transform.GetChild(0);
        text = textObject.GetComponent<TextMeshProUGUI>();
        if (text)
        {
            text.text = round.roundNumber.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

