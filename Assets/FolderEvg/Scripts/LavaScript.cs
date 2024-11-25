using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 1f;
    GameManager gameManager;

    private float currentYPos;

    void Start()
    {
        currentYPos = transform.position.y;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentYPos += riseSpeed * Time.deltaTime;

        transform.position = new Vector3(currentPosition.x, currentYPos, currentPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            gameManager.Player1Death();
            Debug.Log("Burned Cat!");
        }
    }

}
