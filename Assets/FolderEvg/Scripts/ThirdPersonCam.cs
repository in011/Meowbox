using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform body;
    public Rigidbody rb;

    public float rotationSpeed;

    private void Start()
    {
        // отключаем видимость курсора
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        /*
        // Куда смотрит камера
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        // Поворачиваем персонажа в сторону камеры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.forward * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            body.forward = Vector3.Slerp(body.forward, inputDir.normalized, Time.deltaTime*rotationSpeed);
        }
        */
    }
}
