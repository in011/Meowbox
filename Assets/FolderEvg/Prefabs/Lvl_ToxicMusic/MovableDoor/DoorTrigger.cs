using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Block"))
        {
            _doorAnimator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Block"))
        {
            //_doorAnimator.SetTrigger("Closed");
        }
    }
}
