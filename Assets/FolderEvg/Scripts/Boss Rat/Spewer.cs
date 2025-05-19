using UnityEngine;
using System.Collections;

public class Spewer : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Target Movement")]
    public Vector3 targetOffset = new Vector3(0f, 5f, 0f); // Насколько вверх двигать
    public float moveDuration = 2f;

    [Header("Pingpong Movement")]
    public Vector3 pingpongOffset = new Vector3(0f, 0f, 5f); // Насколько вверх двигать
    public bool shouldPingPong = false;
    private bool stopPingPong = false;

    [Header("Spawning")]
    public GameObject prefabToSpawn;
    public GameObject spawnLocation;
    public float spawnInterval = 1f;
    public int spawnCount = 5;

    private Coroutine moveCoroutine;
    private Coroutine spawnCoroutine;
    private Coroutine pingpongCoroutine;
    private Vector3 initialPosition;
    private Vector3 originalPosition;


    void Start()
    {
        originalPosition = transform.position;
    }

    public void GoUp()
    {
        SetStartPos();
        stopPingPong = false;

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToPosition(initialPosition + targetOffset, moveDuration));

        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnObjects());
    }

    public void GoDown()
    {
        stopPingPong = true;

        StopAllCoroutines();
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }

        if (pingpongCoroutine != null)
        {
            StopCoroutine(pingpongCoroutine);
            pingpongCoroutine = null;
        }

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        Debug.Log("Go Down");
        moveCoroutine = StartCoroutine(MoveToPosition(originalPosition, moveDuration));
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Debug.Log("Destination " + target);
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;

        if (shouldPingPong && !stopPingPong) 
        {
            pingpongCoroutine = StartCoroutine(PingPongMovement());
        }
    }

    private IEnumerator PingPongMovement()
    {
        SetStartPos();
        Vector3 posA = initialPosition;
        Vector3 posB = initialPosition + pingpongOffset;

        while (!stopPingPong)
        {
            yield return StartCoroutine(MoveToPosition(posB, moveDuration));
            yield return StartCoroutine(MoveToPosition(posA, moveDuration));
        }
    }

    private IEnumerator SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            if (i == 0)
            {
                yield return new WaitForSeconds(moveDuration);
            }
            Bullet bullet = Instantiate(prefabToSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation).GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.gameManager = gameManager;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SetStartPos()
    {
        initialPosition = transform.position;
    }
}