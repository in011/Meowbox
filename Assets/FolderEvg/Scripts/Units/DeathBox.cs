using System;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    GameManager gameManager;
    AudioManager audioManager;
    [SerializeField] BossFinalStage bossScript;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        audioManager.PlaySFX(audioManager.fallInLava);

        if (other.tag == "Player1")
        {
            gameManager.Player1Death();
            Debug.Log("Stumped!");
        }
        if (other.tag == "Player2")
        {
            gameManager.Player2Death();
            Debug.Log("Stumped!");
        }
        if (other.tag == "Block")
        {
            bossScript.Collision();
            Debug.Log("Stop!");
        }
        if (other.tag == "Trap")
        {
            bossScript.Collision();
            bossScript.Damage();
            Debug.Log("Crash!");
            Destroy(other.gameObject);
        }
    }
}
