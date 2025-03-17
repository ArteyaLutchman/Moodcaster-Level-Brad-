using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 60f;
    public float patrolRadius = 15f;
    public float waitTime = 2f;
    public Transform eyesPosition;
    public LayerMask obstacleMask;

    private NavMeshAgent agent;
    private bool playerInSight = false;
    private float patrolTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNewPatrolPoint();
    }

    void Update()
    {
        DetectPlayer();

        if (playerInSight)
        {
            agent.SetDestination(player.position); // Chase player
        }
        else
        {
            Patrol(); // Wander around randomly
        }
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within detection range
        if (distanceToPlayer > detectionRange)
        {
            playerInSight = false;
            return;
        }

        // Check if player is within field of view
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > fieldOfViewAngle / 2)
        {
            playerInSight = false;
            return;
        }

        // Check for obstacles between NPC and player
        if (!Physics.Linecast(eyesPosition.position, player.position, obstacleMask))
        {
            playerInSight = true;
        }
        else
        {
            playerInSight = false;
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= waitTime)
            {
                MoveToNewPatrolPoint();
                patrolTimer = 0f;
            }
        }
    }

    void MoveToNewPatrolPoint()
    {
        Vector3 randomPoint = GetRandomNavMeshPoint(transform.position, patrolRadius);
        agent.SetDestination(randomPoint);
    }

    Vector3 GetRandomNavMeshPoint(Vector3 origin, float range)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += origin;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return origin; // If no valid point found, stay in place
    }
}
