using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    Rigidbody2D body = default;
    [SerializeField]
    Vector2 jumpForce = Vector2.up;
    [SerializeField]
    float minDelayBetweenJump = 0.120f;
    [SerializeField]
    AudioClip jumpAudioClip = default;

    public enum GameState {  init, flying, gameOver};
    public GameObject Menu, Game;

    static GameState currentState;
    public static GameState CurrentState {  get { return currentState; } }

    public static UnityAction OnGameOver;
    public static UnityAction OnRetry;
    public static UnityAction OnInit;
    public static UnityAction OnJump;

    bool jumpRequest;
    float nextJumpTime;

    void Start()
    {
        jumpRequest = false;
        nextJumpTime = 0;
        currentState = GameState.init;
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        Time.timeScale = 4;
        Menu.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !jumpRequest && Time.time > nextJumpTime)
        {
            nextJumpTime = Time.time + minDelayBetweenJump;
            if (currentState != GameState.flying) {
                if (currentState == GameState.gameOver)
                {
                    if (OnRetry != null)
                    OnRetry();
                } else if (OnInit != null) {
                    OnInit();   
                }

                body.velocity = Vector3.zero;
                body.angularVelocity = 0;
                body.transform.position = Vector3.zero;
                body.transform.rotation = Quaternion.identity;
                body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                currentState = GameState.flying;
            } else
                jumpRequest = true;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            jumpRequest = false;
            AudioSource.PlayClipAtPoint(jumpAudioClip, Vector3.zero);
            body.AddForce(jumpForce, ForceMode2D.Impulse);
            if (OnJump != null)
                OnJump();
        }
    }

    public void Die()
    {
        if (currentState == GameState.gameOver){
            return;
        }

        currentState = GameState.gameOver;
        body.constraints = RigidbodyConstraints2D.None;
        if (OnGameOver != null){
            OnGameOver();  
        }
    }

    public void StartGame()
    {
        Game.SetActive(true);
        Menu.SetActive(false);
    }
}
