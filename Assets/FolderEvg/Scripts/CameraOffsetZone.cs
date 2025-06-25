using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraOffsetZone : MonoBehaviour
{
    public Vector3 targetOffset = new Vector3(0, 10, -5); // Новое значение Follow Offset
    public float transitionDuration = 1f; // Время плавного перехода

    private bool firstTime1 = true;
    private bool firstTime2 = true;
    private CinemachineFollow virtualCamera1;
    private CinemachineFollow virtualCamera2;
    private Vector3 originalOffset1;
    private Vector3 originalOffset2;
    private Coroutine currentTransition1;
    private Coroutine currentTransition2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            // Получаем ссылку на камеру через скрипт "Moving"
            Moving movingScript1 = other.GetComponent<Moving>();
            if (movingScript1 != null && movingScript1.virtualCamera != null)
            {
                virtualCamera1 = movingScript1.virtualCamera;
                if (firstTime1)
                {
                    originalOffset1 = virtualCamera1.FollowOffset;
                    firstTime1 = false;
                }                

                if (currentTransition1 != null) StopCoroutine(currentTransition1);
                currentTransition1 = StartCoroutine(ChangeOffsetSmoothly1(originalOffset1, targetOffset));
            }
        }
        if (other.CompareTag("Player2"))
        {
            // Получаем ссылку на камеру через скрипт "Moving"
            Moving movingScript2 = other.GetComponent<Moving>();
            if (movingScript2 != null && movingScript2.virtualCamera != null)
            {
                virtualCamera2 = movingScript2.virtualCamera;
                if (firstTime2)
                {
                    originalOffset2 = virtualCamera2.FollowOffset;
                    firstTime2 = false;
                }                

                if (currentTransition2 != null) StopCoroutine(currentTransition2);
                currentTransition2 = StartCoroutine(ChangeOffsetSmoothly2(originalOffset2, targetOffset));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            if (currentTransition1 != null) StopCoroutine(currentTransition1);
            currentTransition1 = StartCoroutine(ChangeOffsetSmoothly1(virtualCamera1.FollowOffset, originalOffset1));
        }
        if (other.CompareTag("Player2"))
        {
            if (currentTransition2 != null) StopCoroutine(currentTransition2);
            currentTransition2 = StartCoroutine(ChangeOffsetSmoothly2(virtualCamera2.FollowOffset, originalOffset2));
        }
    }

    private IEnumerator ChangeOffsetSmoothly1(Vector3 fromOffset, Vector3 toOffset)
    {
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;
            virtualCamera1.FollowOffset = Vector3.Lerp(fromOffset, toOffset, t);
            yield return null;
        }

        virtualCamera1.FollowOffset = toOffset;
    }
    private IEnumerator ChangeOffsetSmoothly2(Vector3 fromOffset, Vector3 toOffset)
    {
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;
            virtualCamera2.FollowOffset = Vector3.Lerp(fromOffset, toOffset, t);
            yield return null;
        }

        virtualCamera2.FollowOffset = toOffset;
    }
}