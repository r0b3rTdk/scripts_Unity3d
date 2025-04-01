using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcIA : MonoBehaviour
{
    [Header("Components")]
    public List<Transform> waypoints;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public LayerMask playerLayer;
    public Transform player;
    public AudioSource audioSource;

    [Header("Variables")]
    public int currentWaypointIndex = 0;
    public float speed = 2.5f;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    private bool isChasing = false;
    private bool wasChasing = false;
    private bool canAttack = true;
    public float attackCooldown = 1.5f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = speed;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        wasChasing = isChasing;
        isChasing = distanceToPlayer < detectionRange;

        if (isChasing && !wasChasing)
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (!isChasing && wasChasing)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        if (isChasing)
        {
            if (distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void ChasePlayer()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(player.position);
        animator.SetBool("Move", true);
    }

    private void AttackPlayer()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("Move", false);

        if (canAttack)
        {
            canAttack = false;
            animator.SetTrigger("Attack");
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void Patrol()
    {
        if (waypoints.Count == 0) return;

        float distanceToWaypoint = Vector3.Distance(
            waypoints[currentWaypointIndex].position, transform.position);

        if (distanceToWaypoint <= 2)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);

        animator.SetBool("Move", true);
    }
}