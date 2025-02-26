using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public bool player1;
    public Transform playerObject;
    public Transform secondPlayer;
    private Vector3 cameraPosition;
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        cameraPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(Vector3.Distance(secondPlayer.position, playerObject.position));

        if (Vector3.Distance(secondPlayer.position, playerObject.position) < 25f)
        {
            Debug.Log("Single");
            camera.rect = new Rect(0f, 0f, 1f, 1f);
                //.Set(0f, 0f, 1f, 1f);
        }
        else
        {
            Debug.Log("Half");
            if (player1)
            {
                camera.rect = new Rect(0f, 0f, 0.5f, 1f);

                //camera.rect.Set(0f, 0f, 0.5f, 1f);
            }
            else
            {
                camera.rect = new Rect(0.5f, 0f, 0.5f, 1f);

                //.rect.Set(0.5f, 0f, 0.5f, 1f);
            }
        }

        transform.position = new Vector3(playerObject.position.x + cameraPosition.x, playerObject.position.y + cameraPosition.y, playerObject.position.z + cameraPosition.z);
    }
}
