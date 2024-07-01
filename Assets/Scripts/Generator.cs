using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Generator : MonoBehaviour
{
    public EnemyBehavior enemy;
    public EnemyBehavior enemy2;
    public EnemyBehavior enemy3;
    public Transform player;
    public Slider slider;
    public ShieldScript shield;
    private float _lives = 100f;

    //new
    AudioSource source;

    public void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    //n
    public float GeneratorHealth
    {
        get
        { return _lives; }
        private set
        {
            _lives = value;

            if (_lives <= 0)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDamage");
                shield.life --;
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    void Start()
    {
        slider.value = CalcLife();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        slider.value = CalcLife();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            source.Play();
            //FindObjectOfType<AudioManager>().Play("GeneratorImpact");
            GeneratorHealth -= 10;
            Debug.Log("Critical hit!");
            enemy.agent.speed = 10;
            enemy.agent.destination = player.position;
            enemy2.agent.speed = 10;
            enemy2.agent.destination = player.position;
            enemy3.agent.speed = 10;
            enemy3.agent.destination = player.position;
        }
    }

    public float CalcLife()
    {
        float temp;
        temp = GeneratorHealth / 100;
        return temp;
    }

}
