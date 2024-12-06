using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform playerObject;


    private void Start()
    {
        // отключаем видимость курсора
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
