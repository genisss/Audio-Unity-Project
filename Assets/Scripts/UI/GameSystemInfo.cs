using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystemInfo : MonoBehaviour
{
    public static GameSystemInfo Instance { get; private set; }
    
    public Text TimerText;
    public Text ScoreText;
    public GameObject FinalUI;
    public Text FinalScore;
    
    void Awake()
    {
        Instance = this;
    }

    public void UpdateTimer(float time)
    {
        TimerText.text = ((int)(GameManager.instance.timeLimit - time)).ToString("D2");
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
