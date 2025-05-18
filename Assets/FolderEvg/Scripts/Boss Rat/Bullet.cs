using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager gameManager;
    AudioManager audioManager;
    public float speed = 5f;
    [Tooltip("Lifetime")]
    public float lifetime = 10f;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        gameManager = FindAnyObjectByType<GameManager>();

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            gameManager.Player1Death();
            Debug.Log("Shot!");
        }
        else if (other.tag == "Player2")
        {
            gameManager.Player2Death();
            Debug.Log("Shot!");
        }
        else if (other.gameObject != gameObject)
        {
            Destroy(gameObject);
        }
    }
}
