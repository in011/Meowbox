using UnityEngine;
using System.Collections;

public class CauldronFloor : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 targetPosition;         // Конечная позиция
    public float moveDuration = 2f;        // Время движения в секундах

    private Vector3 initialPosition;       // Начальная позиция
    private Coroutine moveCoroutine;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void GoUp()
    {
        StartMove(transform.position + targetPosition);
    }

    public void GoDown()
    {
        StartMove(initialPosition);
    }

    private void StartMove(Vector3 destination)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveToPosition(destination, moveDuration));
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target; // Обеспечить точную установку позиции
    }
}
