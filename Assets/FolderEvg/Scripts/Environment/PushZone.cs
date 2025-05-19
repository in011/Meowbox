using UnityEngine;

public class PushZone : MonoBehaviour
{
    public Vector3 pushDirection = Vector3.forward; // Direction to push
    public float pushForce = 5f; // Force applied to the player
    public float exitPush = 5f; // Force applied to the player

    private void OnTriggerStay(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller != null && (other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            Vector3 movement = pushDirection.normalized * pushForce * Time.deltaTime;
            controller.Move(movement);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Moving moving = other.GetComponent<Moving>();
        if (moving != null && (other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            moving.PushUp();
        }
    }
}
