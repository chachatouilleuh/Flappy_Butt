using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator = default;

    private void OnEnable()
    {
        GameManager.OnGameOver += TriggerGameOver;
        GameManager.OnRetry += TriggerRetry;
    }
    private void OnDisable()
    {
        GameManager.OnGameOver -= TriggerGameOver;
        GameManager.OnRetry -= TriggerRetry;
    }

    void TriggerGameOver()
    {
        animator.SetTrigger("gameOver");
    }

    void TriggerRetry()
    {
        animator.SetTrigger("retry");
    }
}
