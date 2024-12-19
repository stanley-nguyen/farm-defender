using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance {  get; private set; }

    [HideInInspector]
    public int highScore;

    [HideInInspector]
    public int currentScore;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void UpdateHighScore()
    {
        highScore = Mathf.Max(currentScore, highScore);
    }
}
