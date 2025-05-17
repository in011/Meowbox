using UnityEngine;
using static TriggerObjectMoverOrRotator;

public class TriggerObjectMoverOrRotator : MonoBehaviour
{
    public enum ActionType { MoveUp, Rotate }

    [Header("General Settings")]
    public GameObject targetObject;
    // public ActionType actionType = ActionType.MoveUp;
    public bool press = true;
    public bool rotate = false;
    public float duration = 1f;
    public bool returnOnExit = true; // Возврат при выходе

    [Header("Move Settings")]
    public Vector3 moveDestanation;

    [Header("Rotate Settings")]
    public Vector3 rotationAngles = new Vector3(0f, 90f, 0f); // Например, поворот по Y на 90°

    private bool actionStarted = false;

    private bool isAnimating = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private void Start()
    {
        if (targetObject != null)
        {
            originalPosition = targetObject.transform.position;
            originalRotation = targetObject.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAnimating || targetObject == null) return;

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            isAnimating = true;

            if (press)
            {
                Debug.Log("Press");
                StartCoroutine(MoveCoroutine(originalPosition + moveDestanation));
            }
            if (rotate)
            {
                StartCoroutine(RotateCoroutine(originalRotation * Quaternion.Euler(rotationAngles)));
            }

            /*if (actionType == ActionType.MoveUp)
                StartCoroutine(MoveUpCoroutine());
            else if (actionType == ActionType.Rotate)
                StartCoroutine(RotateCoroutine());*/
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!returnOnExit || targetObject == null) return;
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            StopAllCoroutines(); // Прерываем анимацию входа, если она ещё не завершена

            if (press)
            {
                Debug.Log("Press");
                StartCoroutine(MoveCoroutine(originalPosition));
            }
            if (rotate)
            {
                StartCoroutine(RotateCoroutine(originalRotation));
            }
        }
    }

    private System.Collections.IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        Vector3 start = targetObject.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            targetObject.transform.position = Vector3.Lerp(start, targetPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        targetObject.transform.position = targetPosition;
        isAnimating = false;
    }

    private System.Collections.IEnumerator RotateCoroutine(Quaternion targetRotation)
    {
        Quaternion start = targetObject.transform.rotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            targetObject.transform.rotation = Quaternion.Slerp(start, targetRotation, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        targetObject.transform.rotation = targetRotation;
        isAnimating = false;
    }
}
