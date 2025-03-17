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
            Debug.Log("Destroyed^" + other.gameObject.name);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
