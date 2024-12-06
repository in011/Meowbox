using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform playerObject;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(currentPosition.x + currentPosition.x, playerObject.position.y + currentPosition.y, playerObject.position.z + currentPosition.z);
    }
}
