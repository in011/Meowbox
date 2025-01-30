using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    [SerializeField] public float riseSpeed = 1f;
    GameManager gameManager;
    AudioManager audioManager;

    private float currentYPos;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();

        currentYPos = transform.position.y;
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentYPos += riseSpeed * Time.deltaTime;

        transform.position = new Vector3(currentPosition.x, currentYPos, currentPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        audioManager.PlaySFX(audioManager.fallInLava);

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
