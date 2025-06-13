using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 20f;
    GameManager gameManager;
    AudioManager audioManager;

    [SerializeField] private bool fallen = false;
    [SerializeField] private float scanDistance = 1f;
    [SerializeField] public float destroyTime = 20f;
    bool m_HitDetect;
    RaycastHit m_Hit;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        gameManager = FindAnyObjectByType<GameManager>();

        Destroy(gameObject, destroyTime);
    }

    private void FixedUpdate()
    {
        m_HitDetect = Physics.BoxCast(transform.position, transform.localScale * 0.5f, -transform.up, out m_Hit, transform.rotation, scanDistance);
        if (m_HitDetect)
        {
            if (m_Hit.transform.gameObject.tag == "Wall")
            {
                fallen = true;
            }
        }
        if (!fallen)
        {
            // Move the object downward
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        audioManager.PlaySFX(audioManager.fallInLava);

        if (other.tag == "Player1")
        {
            gameManager.Player1Death();
            Destroy(gameObject);
        }
        if (other.tag == "Player2")
        {
            gameManager.Player2Death();
            Destroy(gameObject);
        }
        if (other.tag == "Wall")
        {
            fallen = true;
        }
        if (other.tag == "Sacrifice")
        {
            Boss boss = other.GetComponent<Boss>();
            if(!boss.stoned && !boss.angry && !boss.hurt)
            {
                Debug.Log("Hurt!");

                boss.Damage();
            }
            if (boss.stoned && fallen && !boss.angry)
            {
                other.GetComponent<Boss>().Stunned();
                Debug.Log("Stunned!");
            }
            Destroy(gameObject);
        }
    }
}
