using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private GameObject brokenWall;
    [SerializeField] private float pushStr = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Debug.Log("Collision");
            Rigidbody rb = other.attachedRigidbody;
            if (rb.linearVelocity.magnitude > pushStr)
            {
                //Instantiate(brokenWall, gameObject.transform);
                Debug.Log("Disable");
                gameObject.SetActive(false);
            }        
        }
    }
}
