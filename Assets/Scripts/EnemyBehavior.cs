using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform spawn;
    public GameObject enemyBullet;
    private float timer = 5;
    public float projectileTime;
    public float skeletonSpeed;
    Vector3 relativePos;
    Rigidbody _rb;
    bool grounded;
    public GameObject skelly;
    public float test;
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;
    public Slider slider;
    public int speed = 5;
    public Camera mainCam;

    private int locationIndex = 0;
    public NavMeshAgent agent;
    private float _lives = 100f;

    //new
    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();   
    }
    //n

    public float EnemyLives
    {
        get
        { return _lives; }
        private set
        {
            _lives = value;

            if (_lives <= 0)
            {
                FindObjectOfType<AudioManager>().Play("DeathSound");
                GetComponent<Rigidbody>().detectCollisions = false;
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                StartCoroutine(DeathWait());  
            }
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        slider.value = CalcLife();

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void FixedUpdate()
    {
        relativePos = player.position;
        slider.value = CalcLife();
      
        
        if (agent.remainingDistance < test && !agent.pathPending)
        {
            MoveToNextPatrolLocation();

            if (EnemyLives < 100)
            {
               skelly.GetComponent<Animator>().Play("Run");
            }
        }

        if (EnemyLives < 100)
        {
            transform.LookAt(player);
            agent.destination = player.position;
            agent.speed = 10;
        } 
    }

    void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        agent.speed = 4;
        skelly.GetComponent<Animator>().Play("Walk");
        if (locations.Count == 0)
           return;

        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        agent.destination = locations[locationIndex].position;
        StartCoroutine(patrolPause());

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    StartCoroutine(patrolPause());
                }
            }
        }
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            transform.LookAt(player);
            agent.speed = 10;
            agent.destination = relativePos;
            skelly.GetComponent<Animator>().Play("Run");
            Debug.Log("Player detected - EXTERMINATE");
        }

    }
    
    
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            transform.LookAt(player);
            Shoot();
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            source.Play();
           // FindObjectOfType<AudioManager>().Play("EnemyGrunt");

            EnemyLives -= 10;
            Debug.Log("Critical hit!");

            skelly.GetComponent<Animator>().Play("Damage");
            agent.speed = 10;
            transform.LookAt(player);
            StartCoroutine(Wait());
            skelly.GetComponent<Animator>().Play("Run");
            agent.destination = player.position;
        }

        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(DamageWait());
            StartCoroutine(AttackWait());
        }
    }

    public float CalcLife()
    {
        float temp;
        temp = EnemyLives / 100;
        return temp;
    }

    void Shoot()
    {
        projectileTime -= Time.deltaTime;

        if (projectileTime < 0) return;

        projectileTime = timer;
        skelly.GetComponent<Animator>().Play("Attack");
        GameObject projectile = Instantiate(enemyBullet, spawn.transform.position, spawn.transform.rotation) as GameObject;
        Rigidbody bulletRB = projectile.GetComponent<Rigidbody>();
        bulletRB.AddForce(bulletRB.transform.forward * skeletonSpeed);
    }

    IEnumerator patrolPause()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        skelly.GetComponent<Animator>().Play("Idle");
        yield return new WaitForSeconds(2f);
        skelly.GetComponent<Animator>().Play("Walk");
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    IEnumerator DeathWait()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(.2f);
        skelly.GetComponent<Animator>().Play("Death");
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        
    }

    IEnumerator AttackWait()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        skelly.GetComponent<Animator>().Play("Attack");
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    IEnumerator Wait()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(1.5f);
        skelly.GetComponent<Animator>().Play("Run");
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    IEnumerator DamageWait()
    {
        GetComponent<Rigidbody>().detectCollisions = false;
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody>().detectCollisions = true;
    }
}
