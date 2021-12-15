using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnCollision : MonoBehaviour
{
    [SerializeField]
    LayerMask deathLayerMask = default;
    [SerializeField]
    AudioClip hitAudioClip = default;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & deathLayerMask) != 0)
        {
            AudioSource.PlayClipAtPoint(hitAudioClip, Vector3.zero);
            GameManager.Instance.Die();
        }
    }
}
