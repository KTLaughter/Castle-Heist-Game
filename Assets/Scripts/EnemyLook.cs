using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyLook : MonoBehaviour
{
    public Camera mainCam;
    public EnemyBehavior enemyBehavior;

    void Update()
    {
        if (enemyBehavior.EnemyLives < 100)
        {
            transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.back,
              mainCam.transform.rotation * Vector3.up);
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.x = 0;
            transform.eulerAngles = eulerAngles;
        }
    }
}
