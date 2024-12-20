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
            //audioManager.PlaySFX(audioManager.fallInLava);

            gameManager.Player1Death();
            Debug.Log("Lava!");
        }
        if (other.tag == "Player2")
        {
            //audioManager.PlaySFX(audioManager.fallInLava);

            gameManager.Player2Death();
            Debug.Log("Lava!");
        }
        if (other.tag == "Sacrifice")
        {
            audioManager.PlaySFX(audioManager.sheepBurn);

            gameManager.AddScore();
            Debug.Log("+1!");
        }

        if (other.tag == "Block")
        {
            //audioManager.PlaySFX(audioManager.fallInLava);

            Block block;
            if(other.TryGetComponent<Block>(out block))
            {
                block.Respawn();
            }
        }
    }
}
