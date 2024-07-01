using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior gameManager;

    void Start()
    {                 
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && gameManager._playerHP <= 99)
        {
            FindObjectOfType<AudioManager>().Play("HealthSound");
            FindObjectOfType<AudioManager>().Play("PlayerDamage");
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item collected!");
            gameManager.Items += 30;
            if (gameManager._playerHP >= 100)
                gameManager._playerHP = 100;
        }
       
    }
}
