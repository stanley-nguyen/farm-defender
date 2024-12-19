using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [HideInInspector]
    public bool gameOver;

    [SerializeField]
    private TextMeshProUGUI hsText;

    [SerializeField]
    private TextMeshProUGUI sText;

    [SerializeField]
    private TextMeshProUGUI newhsText;

    public void ShowGameOver()
    {
        if (ScoreManager.instance.currentScore > ScoreManager.instance.highScore)
            newhsText.gameObject.SetActive(true);
        else
            newhsText.gameObject.SetActive(false);

        ScoreManager.instance.UpdateHighScore();

        sText.text = "SCORE: " + ScoreManager.instance.currentScore.ToString();
        hsText.text = "HIGH SCORE: " + ScoreManager.instance.highScore.ToString();

        gameOver = true;
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        gameOver = false;
        SceneManager.LoadScene("Game");
    }
}
