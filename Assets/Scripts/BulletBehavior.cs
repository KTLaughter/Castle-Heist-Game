using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;

    void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
    }

    void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<AudioManager>().Play("BulletHit");
        if (collision.gameObject.name == "Skeleton")
        {
            Destroy(this.gameObject, 0.25f);
        }
    }
}