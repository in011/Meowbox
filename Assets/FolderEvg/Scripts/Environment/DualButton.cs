using UnityEngine;

public class DualButton : MonoBehaviour
{
    public bool IsPressed { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            IsPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            IsPressed = false;
        }
    }
}
