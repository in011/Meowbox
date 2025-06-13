using UnityEngine;

public class RatBomb : MonoBehaviour
{
    public float checkRadius = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Rigidbody rb = other.attachedRigidbody;
            Debug.Log("Open!" + rb.linearVelocity.magnitude);
            if (rb.linearVelocity.magnitude > 7.5f)
            {
                CheckForSacrifice();
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
                // ���������� ������ � ����� Sacrifice
                Destroy(col.gameObject);

                // ����� BossRatManager �� �����
                BossRatManager bossManager = FindFirstObjectByType<BossRatManager>();
                if (bossManager != null)
                {
                    bossManager.HP -= 1;
                    bossManager.StageA();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("BossRatManager �� ������ �� �����.");
                }

                break; // �������� ����� ������� ���������� Sacrifice
            }
        }
    }
}
