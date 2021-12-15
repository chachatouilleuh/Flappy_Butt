using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI bestScoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreUpdate += UpdateScoreText;
    }
    private void OnDisable()
    {
        ScoreManager.OnScoreUpdate -= UpdateScoreText;
    }

    void Start()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if(scoreText != null){
            scoreText.text = ScoreManager.Instance.Score.ToString();
        }
        else if(bestScoreText != null){
           bestScoreText.text = ScoreManager.Instance.BestScore.ToString(); 
        }
    }
}
