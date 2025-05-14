using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraOffsetZone : MonoBehaviour
{
    public Vector3 targetOffset = new Vector3(0, 10, -5); // Новое значение Follow Offset
    public float transitionDuration = 1f; // Время плавного перехода

    private CinemachineFollow virtualCamera;
    private Vector3 originalOffset;
    private Coroutine currentTransition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Получаем ссылку на камеру через скрипт "Moving"
            Moving movingScript = other.GetComponent<Moving>();
            if (movingScript != null && movingScript.virtualCamera != null)
            {
                virtualCamera = movingScript.virtualCamera;
                originalOffset = virtualCamera.FollowOffset;

                if (currentTransition != null) StopCoroutine(currentTransition);
                currentTransition = StartCoroutine(ChangeOffsetSmoothly(originalOffset, targetOffset));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (currentTransition != null) StopCoroutine(currentTransition);
            currentTransition = StartCoroutine(ChangeOffsetSmoothly(virtualCamera.FollowOffset, originalOffset));
        }
    }

    private IEnumerator ChangeOffsetSmoothly(Vector3 fromOffset, Vector3 toOffset)
    {
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;
            virtualCamera.FollowOffset = Vector3.Lerp(fromOffset, toOffset, t);
            yield return null;
        }

        virtualCamera.FollowOffset = toOffset;
    }
}