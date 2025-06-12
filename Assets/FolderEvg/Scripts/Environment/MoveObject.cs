using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public Vector3 targetOffset = new Vector3(0f, 5f, 0f); // Насколько вверх двигать
    public float moveDuration = 2f;

    private Vector3 initialPosition;
    private Vector3 originalPosition;
    private Coroutine moveCoroutine;
    private Coroutine spawnCoroutine;
    private Coroutine pingpongCoroutine;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void GoUp()
    {
        initialPosition = transform.position;

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToPosition(initialPosition + targetOffset, moveDuration));
    }

    public void GoDown()
    {
        StopAllCoroutines();
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
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
    }
}
