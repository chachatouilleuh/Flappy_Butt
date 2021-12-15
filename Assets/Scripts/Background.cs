using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    private float length, startpos;
    private float speed = -0.5f; 
    private float distFromMiddle = 0;
    public float parallax;

    void Start(){
        startpos = transform.position.x;
        length = GetComponent<Collider>().bounds.size.x;
    }

    void Update(){
        distFromMiddle = distFromMiddle + speed;
        float dist = (distFromMiddle * parallax);

        transform.position = new Vector2(startpos + dist, transform.position.y);

        if (dist < startpos - length)
        {
            transform.position = new Vector2(startpos - length , transform.position.y);
            distFromMiddle = 0f;
        }
    }
}
