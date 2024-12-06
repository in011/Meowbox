using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] private float pushPower = 4;
    [SerializeField] Transform playerTransform;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        GameObject pushObject = hit.collider.gameObject;

        if (body == null || body.isKinematic)
        {
            return;
        }
        if(hit.moveDirection.y < -0.3f)
        {
            return;
        }

        // Проверяем не стоим ли мы на объекте
        RaycastHit rayHit;
        GameObject floor;
        if (Physics.Raycast(playerTransform.position, Vector3.down, out rayHit))
        {
            floor = rayHit.collider.gameObject; // hit.collider.gameObject
            Debug.Log(floor.name);

            if (pushObject == floor)
            {
                Debug.Log("Trying to push, while standing on it!");
                return;
            }
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
        /*
        Vector3 collisionPoint = hit.point;
        body.AddForceAtPosition(pushDir * pushPower, collisionPoint, ForceMode.Impulse);
        */
    }

    private void FloorCheck(Rigidbody floor)
    {
        
    }
}
