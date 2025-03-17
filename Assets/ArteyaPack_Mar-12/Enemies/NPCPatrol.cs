using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    public float patrolRadius = 10f;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNewPatrolPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                MoveToNewPatrolPoint();
                timer = 0f;
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
