using UnityEngine;

public class RatBomb : MonoBehaviour
{
    public float checkRadius = 5f;
    public GameObject explosionVFX;
    private Block blockScript;
    private void Start()
    {
        gameObject.TryGetComponent<Block>(out blockScript);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Rigidbody rb = other.attachedRigidbody;
            Debug.Log("Open!" + rb.linearVelocity.magnitude);
            if (rb.linearVelocity.magnitude > 7.5f)
            {
                CheckForSacrifice();
                Instantiate(explosionVFX, gameObject.transform.position, Quaternion.identity);
                blockScript.Return();
            }
        }
    }
    void CheckForSacrifice()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Sacrifice"))
            {
                // Уничтожить объект с тегом Sacrifice
                Destroy(col.gameObject);

                // Найти BossRatManager на сцене
                BossRatManager bossManager = FindFirstObjectByType<BossRatManager>();
                if (bossManager != null)
                {
                    bossManager.HP -= 1;
                    bossManager.StageA();
                    Instantiate(explosionVFX, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("BossRatManager не найден на сцене.");
                }

                break; // Прервать после первого найденного Sacrifice
            }
        }
    }
}
