using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ENGINE : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text healthText;
    TMP_Text sparkText;
    TMP_Text sparkGText;
    void Start()
    {
        healthText = GameObject.Find("CounterH").GetComponent<TMP_Text>();
        sparkText = GameObject.Find("CounterS").GetComponent<TMP_Text>();
        sparkGText = GameObject.Find("CounterSG").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthText.text = "x" + MAINGame.Health;
        sparkText.text = "x" + MAINGame.Stars;
        sparkGText.text = "x" + MAINGame.GoldStars;
    }
}
