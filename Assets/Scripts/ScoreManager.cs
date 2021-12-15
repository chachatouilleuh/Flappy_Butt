using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    int score;
    int bestScore;

    public int Score { get { return score; } }
    public int BestScore { get { return bestScore; } }

    public static UnityAction OnScoreUpdate;

    private void Start()
    {
        ResetScore();
        //layerPrefs.DeleteKey("BestScore");
        bestScore = PlayerPrefs.GetInt("BestScore", bestScore);
    }

    private void OnEnable()
    {
        GameManager.OnRetry += ResetScore;
    }

    private void OnDisable()
    {
        GameManager.OnRetry -= ResetScore;
    }

    void ResetScore()
    {
        score = 0;
    }

    public void IncrementScore()
    {
        SetScore(score + 1);  
    }

    void SetScore(int newScore)
    {
        

        score = newScore;

        if(score > bestScore){
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        if (OnScoreUpdate != null){
            OnScoreUpdate();    
        }

        
        

    }
}
