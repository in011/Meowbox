using UnityEngine;

public class PushObject : MonoBehaviour
{
    Rigidbody body;
    GameObject pushObject;
    bool hasRigidBody;
    RaycastHit rayHit;
    [SerializeField] float checkDistance = 2f;
    [SerializeField] public float pushForce = 1f; // 14.25f - old
    [SerializeField] private LayerMask blockLayerMask; // LayerMask for filtering collisions

    RaycastHit testRayHit;
    GameObject floor;

    /// <summary>
    /// Returns true if no object blocking the way or false 
    /// Checks only the first collision!
    /// </summary>
    /// <returns></returns>
    public bool ObjectCollisionCheck(Vector3 moveVector, bool movePermission, bool StepMovement)
    {
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, checkDistance, blockLayerMask))
        {
            Rigidbody body = rayHit.collider.attachedRigidbody;
            GameObject pushObject = rayHit.collider.gameObject;

            if (pushObject.TryGetComponent<BoxCollider>(out BoxCollider collider) && !pushObject.TryGetComponent<Rigidbody>(out Rigidbody rbody))
            {
                return false;
            }

            if (body == null || body.isKinematic)
            {
                return true;
            }
            /*if (hit.moveDirection.y < -0.3f)
            {
                return;
            }*/

            if (FloorObject(pushObject))
            {
                return false;
            }
            if (movePermission)
            {
                TryObjectMove(pushObject, body, -rayHit.normal); // moveVector
            }


            return false;
        }
        return true;
    }

    // ���������� true, ���� ����� ���� �����������, ������� ����� �������
    public bool ObjectCollision()
    {
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, checkDistance, blockLayerMask))
        {
            Rigidbody body = rayHit.collider.attachedRigidbody;
            GameObject pushObject = rayHit.collider.gameObject;

            if (FloorObject(pushObject))
            {
                return false;
            }
            if (pushObject.TryGetComponent<BoxCollider>(out BoxCollider collider) && pushObject.TryGetComponent<Rigidbody>(out Rigidbody rbody))
            {
                return true;
            }
            return false;
        }
        return false;
    }

    private bool FloorObject(GameObject testObject)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out testRayHit))
        {
            floor = testRayHit.collider.gameObject; // hit.collider.gameObject

            if (testObject == floor)
            {
                return true;
            }
        }
        return false;
    }

    private bool TryObjectMove(GameObject moveObject, Rigidbody rbody, Vector3 direction)
    {
        //rbody.linearVelocity = pushForce * direction;
        rbody.AddForce(direction * pushForce * 348.25f, ForceMode.Force);

        return false;
    }
}
