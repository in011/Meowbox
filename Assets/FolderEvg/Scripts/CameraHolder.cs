using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    private Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        cameraPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(playerObject.position.x + cameraPosition.x, playerObject.position.y + cameraPosition.y, playerObject.position.z + cameraPosition.z);
    }
}
