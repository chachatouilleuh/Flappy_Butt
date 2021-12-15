using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndDie : MonoBehaviour
{
    [SerializeField]
    float speed = 22;
    [SerializeField]
    int minX = -1020;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.CurrentState != GameManager.GameState.flying){
            return;
        }      

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < minX){
            Destroy(gameObject);
        }
    }
}
