using UnityEngine;

public class DualButton : MonoBehaviour
{
    public bool IsPressed { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPressed = false;
        }
    }
}
