using UnityEngine;
using System.Collections;

public class BulletHell : MonoBehaviour
{
    public GameManager gameManager;
    public BossRatManager ratManager;

    [SerializeField] private Spewer[] spewerList;

    [Header("Players")]
    public Transform player1;
    public Transform player2;

    [Header("Spawning")]
    public GameObject shadowObject;
    public GameObject bottleObject;
    public float heightAbovePlayer = 15f;
    public float delayBeforeSecondSpawn = 1f;

    public float radius = 5f;
    public int spawnCount = 8;

    public void StartSpew()
    {
        foreach (Spewer spewer in spewerList)
        {
            spewer.GoUp();
        }
        Invoke("StopSpew", 10f);
    }
    public void StopSpew()
    {
        foreach (Spewer spewer in spewerList)
        {
            spewer.GoDown();
        }
        Invoke("StageB", 1f);
    }
    private void StageB()
    {
        ratManager.StageB();

    }

    public void MassThrows()
    {
        Invoke("Throws", 3f);
        Invoke("Throws", 6f);
        Invoke("Throws", 9f);
        Invoke("StageB", 13f);
    }
    private void Throws()
    {
        int spawned = 0;
        int attempts = 0;
        Destroy(Instantiate(shadowObject, player1.position + Vector3.up * heightAbovePlayer, Quaternion.identity), 2);
        while (spawned < spawnCount && attempts < spawnCount * 2)
        {
            attempts++;

            float angle = Random.Range(0f, 360f);
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Vector3 spawnPos1 = player1.position + direction.normalized * radius;

            if (IsDirectionExcluded(direction, player1)) continue;

            // Позиция над игроком
            Vector3 spawnPosition1 = spawnPos1 + Vector3.up * heightAbovePlayer;

            // Спавн первого объекта
            Destroy(Instantiate(shadowObject, spawnPosition1, Quaternion.identity), 2);

            spawned++;
        }

        spawned = 0;
        attempts = 0;
        Destroy(Instantiate(shadowObject, player2.position + Vector3.up * heightAbovePlayer, Quaternion.identity), 2);
        while (spawned < spawnCount && attempts < spawnCount * 2)
        {
            attempts++;

            float angle = Random.Range(0f, 360f);
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Vector3 spawnPos2 = player2.position + direction.normalized * radius;

            if (IsDirectionExcluded(direction, player2)) continue;

            // Позиция над игроком
            Vector3 spawnPosition2 = spawnPos2 + Vector3.up * heightAbovePlayer;

            // Спавн первого объекта
            Destroy(Instantiate(shadowObject, spawnPosition2, Quaternion.identity), 2);

            spawned++;
        }
    }
    private bool IsDirectionExcluded(Vector3 dir, Transform player)
    {
        dir.Normalize();
        Vector3 forward = player.forward;
        Vector3 right = player.right;

        
        switch (Random.Range(1, 4))
        {
            case 1:
                return Vector3.Dot(dir, -right) > 0.7f;
            case 2:
                return Vector3.Dot(dir, right) > 0.7f;
            case 3:
                return Vector3.Dot(dir, forward) > 0.7f;
            case 4:
                return Vector3.Dot(dir, -forward) > 0.7f;
            default:
                return false;
        }
    }




    public void SpawnBottle()
    {
        // Найти ближайшего игрока
        Transform nearest = GetNearestPlayer();

        // Позиция над игроком
        Vector3 spawnPosition = nearest.position + Vector3.up * heightAbovePlayer;

        // Спавн первого объекта
        Destroy(Instantiate(shadowObject, spawnPosition, Quaternion.identity), 2);

        // Запустить отложенный спавн второго объекта
        // StartCoroutine(SpawnDelayed(spawnPosition));
    }

    private Transform GetNearestPlayer()
    {
        if (gameManager.player1Dead)
        {
            if (gameManager.player2Dead)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }
        else
        {
            if (gameManager.player2Dead)
            {
                return player1;
            }
            else
            {
                float dist1 = Vector3.Distance(transform.position, player1.position);
                float dist2 = Vector3.Distance(transform.position, player2.position);

                return dist1 <= dist2 ? player1 : player2;

            }
        }
    }

    private IEnumerator SpawnDelayed(Vector3 position)
    {
        yield return new WaitForSeconds(delayBeforeSecondSpawn);
        Instantiate(bottleObject, position, Quaternion.identity);
    }
}
