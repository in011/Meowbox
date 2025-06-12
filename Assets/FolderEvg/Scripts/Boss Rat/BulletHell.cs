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
        ratManager.StageB();
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
        StartCoroutine(SpawnDelayed(spawnPosition));
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
