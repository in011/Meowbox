using UnityEngine;
using System.Collections;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    [SerializeField] public KeyCode rotateRightKey = KeyCode.Q; // Key to trigger rotation Right
    [SerializeField] public KeyCode rotateLeftKey = KeyCode.E; // Key to trigger rotation Left
    [SerializeField] private float rotationAngle = 45f; // Degrees to rotate
    [SerializeField] private float rotationSpeed = 5f; // Speed of rotation
    private Vector3 cameraPosition;
    private bool isRotating = false;

    // Focus on player
    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        cameraPosition = transform.position;
    }

    // Camera Rotate 
    void Update()
    {
        if (Input.GetKeyDown(rotateRightKey) && !isRotating)
        {
            StartCoroutine(RotateSmoothly(Vector3.up * rotationAngle));
        }
        if (Input.GetKeyDown(rotateLeftKey) && !isRotating)
        {
            StartCoroutine(RotateSmoothly(Vector3.up * (-rotationAngle)));
        }
    }
    // Follow Player
    void FixedUpdate()
    {
        transform.position = new Vector3(playerObject.position.x + cameraPosition.x, playerObject.position.y + cameraPosition.y, playerObject.position.z + cameraPosition.z);
    }

    // Smooth Rotation
    IEnumerator RotateSmoothly(Vector3 rotation)
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + rotation);
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
        }

        transform.rotation = endRotation; // Ensure final rotation is accurate
        isRotating = false;
    }
}
