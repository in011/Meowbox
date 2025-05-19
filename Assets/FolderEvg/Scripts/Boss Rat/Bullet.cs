using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager gameManager;
    public float speed = 5f;
    [Tooltip("Lifetime")]
    public float lifetime = 10f;

    void Start()
    {
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
        else 
        {
            Destroy(gameObject);
        }
    }
}
