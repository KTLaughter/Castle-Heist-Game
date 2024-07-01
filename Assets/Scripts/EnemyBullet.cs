using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float onscreenDelay = 3f;

    void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.gameObject, 0.25f);
        }
    }
}
