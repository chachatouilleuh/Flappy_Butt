using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnJump : MonoBehaviour
{
    [SerializeField]
    Animator animator = default;

    private void OnEnable()
    {
        GameManager.OnJump += TriggerJump;
    }

    private void OnDisable()
    {
        GameManager.OnJump -= TriggerJump;
    }

    void TriggerJump()
    {
        animator.SetTrigger("jump");
    }
}
