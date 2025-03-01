using UnityEngine;
using UnityEngine.Splines.Interpolators;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera cameraRight;
    [SerializeField] private Camera cameraLeft;
    [SerializeField] private float splitCameraDistance = 12.5f;
    [SerializeField] private float splitSpeed = 1f;
    private float transitionValue;
    private float currentValue = 1f;
    private float end;
    private bool fullscreen = false;

    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    private void Update()
    {
        if(transitionValue <= 1)
        {
            currentValue = Mathf.Lerp(currentValue, end, transitionValue);
            cameraRight.rect = new Rect(0f, 0f, currentValue, 1f);
            cameraLeft.rect = new Rect(currentValue, 0f, 0.5f, 1f);

            transitionValue += splitSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(player1.position, player2.position) <= splitCameraDistance && !fullscreen)
        {
            fullscreen = true;
            transitionValue = 0f;
            //start = 0.5f;
            end = 1f;
        }
        if (Vector3.Distance(player1.position, player2.position) > splitCameraDistance && fullscreen)
        {
            fullscreen = false;
            transitionValue = 0f;
            //start = 1f;
            end = 0.5f;

            /*
            // 0 0 1 1
            if (player1)
            {
                camera.rect = new Rect(0f, 0f, 0.5f, 1f);
            }
            else
            {
                camera.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            }*/
        }
    }
}
