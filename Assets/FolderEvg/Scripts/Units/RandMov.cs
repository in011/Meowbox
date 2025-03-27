using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RandMov : MonoBehaviour
{
    public float moveRadius = 10f; // Defines the range enemy can move
    public float waitTime = 2f; // Time to wait before moving to the next point
    private NavMeshAgent agent;
    private Vector3 targetPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(MoveToRandomPoints());
    }

    IEnumerator MoveToRandomPoints()
    {
        while (true)
        {
            targetPoint = GetRandomPoint();
            agent.SetDestination(targetPoint);

            // Wait until the enemy reaches the point
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // Wait for a moment before picking a new destination
            yield return new WaitForSeconds(waitTime);
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
}
