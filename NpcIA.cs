using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcIA : MonoBehaviour
{
    [Header("Components")]
    public List<Transform> waypoints;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator animator;
    public LayerMask playerLayer;
    [Header("Variables")]
    public int currentWaypointIndex = 0;
    public float speed = 2.5f;
    private bool isPlayerDetected = false;
    private bool onRadious;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = speed;
    }

    void Update()
    {
        if (!isPlayerDetected)
        {
            Walking();
        }
        else
        {
            StopWalking();
            animator.SetTrigger("Attack");
        }
    }
    private void Walking()
    {
        if (waypoints.Count == 0) return;
        float distanceToWaypoint = Vector3.Distance(
            waypoints[currentWaypointIndex].position, transform.position);
        if (distanceToWaypoint <= 2)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        animator.SetBool("Move", true);
        onRadious = false;
    }
    private void StopWalking()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("Move", false);
        onRadious = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
            navMeshAgent.isStopped = false;
        }
    }
}
