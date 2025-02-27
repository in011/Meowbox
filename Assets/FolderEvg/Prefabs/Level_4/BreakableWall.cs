using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private GameObject brokenWall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb.linearVelocity.magnitude > 10f)
            {
                Instantiate(brokenWall, gameObject.transform);
                gameObject.SetActive(false);
            }        
        }
    }
}
