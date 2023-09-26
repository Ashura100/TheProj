using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    LifeSys lifeSystem;
    /*[SerializeField]
    private AudioClip audioClip = null;*/
    [SerializeField] private Transform _movePosTransform;
    private NavMeshAgent _navMeshAgent;
    public Transform player;
    public LayerMask playerMask, groundMask;
    Rigidbody rb;
    private AudioSource zombie_AudioSource;
    [SerializeField]
    Animator anim;
    //patrouille
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    [SerializeField]
    float patrouilleSpeed, poursuiteSpeed = 1;
    //attaque
    float lastAttackTime;
    bool canAttack
    {
        get
        {
            if (Time.timeSinceLevelLoad - lastAttackTime < timeBetweenAttacks) return false;
            return true;
        }
    }
    public float timeBetweenAttacks;
    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Transform attackPoint;
    public int attackDamage = 20;

    public float health;

    ParticleSystem ps;

    public Transform focusPoint
    {
        get
        {
            return transform;
        }
    }

    public bool isFocusable
    {
        get
        {
            if (this == null) return false;
            return lifeSystem.isAlife;
        }
    }

    void Awake()
    {
        lifeSystem.onDieDel = Die;
        player = GameObject.Find("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        /*zombie_AudioSource = GetComponent<AudioSource>();
        zombie_AudioSource.PlayOneShot(audioClip);*/
    }
    void FixedUpdate()
    {
        if (!lifeSystem.isAlife)
        {
            return;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!playerInSightRange && !playerInAttackRange) Patrouille();
        if (playerInSightRange && !playerInAttackRange) Poursuite();
        if (playerInSightRange && playerInAttackRange) Attaque();

    }
    void Patrouille()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            _navMeshAgent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        anim.SetFloat("Walk", 1);
        _navMeshAgent.speed = patrouilleSpeed;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
            walkPointSet = true;
    }
    void Poursuite()
    {
        anim.SetBool("Run", true);
        _navMeshAgent.SetDestination(player.position);
        _navMeshAgent.speed = poursuiteSpeed;
    }

    void Attaque()
    {
        if (!canAttack) return;
        StartCoroutine(AttackCourout());
        lastAttackTime = Time.timeSinceLevelLoad;
    }

    IEnumerator AttackCourout()
    {
        _navMeshAgent.SetDestination(transform.position);

        transform.LookAt(player);

        anim.SetTrigger("Attack");


        yield return new WaitForSeconds(1f);

        Collider[] hitEnemis = Physics.OverlapSphere(attackPoint.position, attackRange, playerMask);
        foreach (Collider Enemy in hitEnemis)
        {
            player.GetComponent<LifeSys>().TakeDamage(attackDamage);
        }




    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        lifeSystem.healthbar.UpdateHealth();
        if (health < 0) Invoke(nameof(Die), 5f);

    }

    void Die()
    {
        anim.SetTrigger("IsDying");
        StartCoroutine(waitForDeath());
    }

    IEnumerator waitForDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
