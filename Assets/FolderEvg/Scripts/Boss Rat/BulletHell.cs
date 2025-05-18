using UnityEngine;
using System.Collections;

public class BulletHell : MonoBehaviour
{
    [SerializeField] private Spewer[] spewerList;
    public bool flag = false;

    [Header("Players")]
    public Transform player1;
    public Transform player2;

    [Header("Spawning")]
    public GameObject shadowObject;
    public GameObject bottleObject;
    public float heightAbovePlayer = 15f;
    public float delayBeforeSecondSpawn = 1f;
    public bool flagBottle = false;

    void Start()
    {
        foreach(Spewer spewer in spewerList)
        {
            spewer.GoUp();
        }
    }

    void Update()
    {
        if (flag)
        {
            foreach (Spewer spewer in spewerList)
            {
                spewer.GoDown();
            }
            flag = false;
        }

        if (flagBottle)
        {
            foreach (Spewer spewer in spewerList)
            {
                spewer.GoDown();
            }
            flagBottle = false;
        }
    }

    public void SpawnBottle()
    {
        // Найти ближайшего игрока
        Transform nearest = GetNearestPlayer();

        // Позиция над игроком
        Vector3 spawnPosition = nearest.position + Vector3.up * heightAbovePlayer;

        // Спавн первого объекта
        Instantiate(shadowObject, spawnPosition, Quaternion.identity);

        // Запустить отложенный спавн второго объекта
        StartCoroutine(SpawnDelayed(spawnPosition));
    }

    private Transform GetNearestPlayer()
    {
        float dist1 = Vector3.Distance(transform.position, player1.position);
        float dist2 = Vector3.Distance(transform.position, player2.position);

        return dist1 <= dist2 ? player1 : player2;
    }

    private IEnumerator SpawnDelayed(Vector3 position)
    {
        yield return new WaitForSeconds(delayBeforeSecondSpawn);
        Instantiate(bottleObject, position, Quaternion.identity);
    }
}
