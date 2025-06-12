using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [Header("Object to Move")]
    public MoveObject[] objectToMove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            foreach (MoveObject obj in objectToMove)
            {
                obj.GoUp();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            foreach (MoveObject obj in objectToMove)
            {
                obj.GoDown();
            }
        }
    }
}
