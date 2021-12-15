using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassOnTriggerEnter : MonoBehaviour
{
    [SerializeField]
    AudioClip passAudioClip = default;
    [SerializeField]
    Door door = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(passAudioClip, Vector3.zero);
        door.Pass();
        ScoreManager.Instance.IncrementScore();
    }
}
