using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 100;
    public Transform Target;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float rotSpeed = 5;
    private Rigidbody rigidbody;
    [SerializeField] private float stoppingDist = 1;
    [SerializeField] private GameObject deathPS;
    public bool dead;
    public bool shouldFollow = true;
    [SerializeField] private Transform bodyPoint;
    public AudioSource hitAudio;
    private PlayerScript PScript;
    private GameEventsManagerScript GEManager;
    private void Awake()
    {
        GEManager = GameObject.Find("GameEventsManager").GetComponent<GameEventsManagerScript>();
    }
    private void OnEnable()
    {
        GEManager.OnGameOver += GameOver;
    }
    private void OnDisable()
    {
        GEManager.OnGameOver -= GameOver;
    }
    void GameOver()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Target = GameObject.Find("Target").transform;
        PScript = Target.root.GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if(!dead)
        {
            if (health <= 0)
                Die();

            if (shouldFollow)
            {
                Quaternion lookRotation = Quaternion.LookRotation((Target.position - transform.position).normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotSpeed * Time.deltaTime);
            }
        }

        
    }

    void Die()
    {
        rigidbody.useGravity = true;
        rigidbody.constraints = RigidbodyConstraints.None;
        GetComponent<Animation>().enabled = false;
        dead = true;
        Invoke("KillFromScene", Random.Range(2, 3));    
    }

    void KillFromScene()
    {
        Instantiate(deathPS, bodyPoint.position, Quaternion.identity);
        Destroy(gameObject);
    }
    float attackTimer;
    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 dir = (Target.position - transform.position).normalized;
        if (!dead)
        {
            if (shouldFollow)
            {
                if (Vector3.Distance(Target.position, transform.position) > stoppingDist)
                    rigidbody.AddForce(transform.forward * moveSpeed, ForceMode.VelocityChange);
                else
                {
                    attackTimer += Time.fixedDeltaTime;
                    if(attackTimer > Random.Range(2,4))
                    {
                        Attack();
                        attackTimer = 0;
                    }
                }
            }
        }
        
    }

    void Attack()
    {
        PScript.Damage(10);
    }

    public void Damage( int val)
    {
        health -= val;
    }
}
