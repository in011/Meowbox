using UnityEngine;

public class BossFloor : MonoBehaviour
{
    GameManager gameManager;
    AudioManager audioManager;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        gameManager = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        audioManager.PlaySFX(audioManager.fallInLava);
        if (other.tag == "Trap")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
