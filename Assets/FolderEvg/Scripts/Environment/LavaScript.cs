using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    [SerializeField] public float riseSpeed = 1f;
    GameManager gameManager;

    private float currentYPos;

    void Start()
    {
        currentYPos = transform.position.y;
        gameManager = FindObjectOfType<GameManager>();
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
            Debug.Log("Lava!");
        }
        if (other.tag == "Player2")
        {
            gameManager.Player2Death();
            Debug.Log("Lava!");
        }
        if (other.tag == "Sacrifice")
        {
            gameManager.score += 1;
            Debug.Log("+1!");
        }

        if (other.tag == "Block")
        {
            Block block;
            if(other.TryGetComponent<Block>(out block))
            {
                block.Respawn();
            }
        }
    }
}
