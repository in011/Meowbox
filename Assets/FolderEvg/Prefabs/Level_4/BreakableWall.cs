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
            Debug.Log("Broken");
            Instantiate(brokenWall, gameObject.transform);
            gameObject.SetActive(false);
        }
    }
}
