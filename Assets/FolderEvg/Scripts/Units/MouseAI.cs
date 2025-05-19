using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MouseAI : MonoBehaviour
{
    [Header("Movement")]
    public float roamRadius = 10f;
    public float waitTimeMin = 1f;
    public float waitTimeMax = 3f;

    [Header("Detection")]
    public Transform player1;
    public Transform player2;
    public float detectionRadius = 5f;
    public float escapeSpeedMultiplier = 2f;

    [Header("Despawn")]
    public float catchDistance = 1f;
    public GameObject despawnEffect;

    private NavMeshAgent agent;
    private Vector3 originalPosition;
    private bool escaping = false;
    private bool caught = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        StartCoroutine(Roam());
    }

    void Update()
    {
        if (caught) return;

        Transform closestPlayer = GetClosestPlayer();
        float distance = Vector3.Distance(transform.position, closestPlayer.position);

        if (distance <= catchDistance)
        {
            StartCoroutine(Despawn());
        }
        else if (distance <= detectionRadius && !escaping)
        {
            escaping = true;
            StopAllCoroutines();
            StartCoroutine(EscapeFrom(closestPlayer));
        }
    }

    IEnumerator Roam()
    {
        while (!caught)
        {
            Vector3 target = RandomNavSphere(originalPosition, roamRadius);
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(target, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(target);
                while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                    yield return null;
            }

            float waitTime = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator EscapeFrom(Transform player)
    {
        float originalSpeed = agent.speed;
        agent.speed *= escapeSpeedMultiplier;

        while (!caught && Vector3.Distance(transform.position, player.position) <= detectionRadius * 1.5f)
        {
            Vector3 direction = (transform.position - player.position).normalized;
            Vector3 escapeTarget = transform.position + direction * roamRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(escapeTarget, out hit, roamRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }

            yield return new WaitForSeconds(1f);
        }

        agent.speed = originalSpeed;
        escaping = false;
        StartCoroutine(Roam());
    }

    IEnumerator Despawn()
    {
        caught = true;
        agent.isStopped = true;

        if (despawnEffect)
            Instantiate(despawnEffect, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);
        return navHit.position;
    }

    Transform GetClosestPlayer()
    {
        float dist1 = Vector3.Distance(transform.position, player1.position);
        float dist2 = Vector3.Distance(transform.position, player2.position);
        return dist1 < dist2 ? player1 : player2;
    }
}
