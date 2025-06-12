using UnityEngine;

public class S_PlatformDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sacrifice"))
        {
            Destroy(other.gameObject);
            Debug.Log("Destroyed");
        }
    }
}
