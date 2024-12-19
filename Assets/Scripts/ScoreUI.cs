using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textScore;

    // Update is called once per frame
    void Update()
    {
        textScore.text = "SCORE: " + ScoreManager.instance.currentScore.ToString();
    }
}
