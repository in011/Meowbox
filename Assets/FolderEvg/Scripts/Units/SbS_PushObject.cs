using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SbS_PushObject : MonoBehaviour
{
    Rigidbody body;
    GameObject pushObject;
    bool hasRigidBody;
    RaycastHit rayHit;
    [SerializeField] float checkDistance = 2f;
    [SerializeField] float pushForce = 14.25f;

    RaycastHit testRayHit;
    GameObject floor;

    /// <summary>
    /// Returns true if no object blocking the way or false 
    /// Checks only the first collision!
    /// </summary>
    /// <returns></returns>
    public bool ObjectCollisionCheck(Vector3 moveVector, bool movePermission, bool StepMovement)
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(transform.forward) * checkDistance, UnityEngine.Color.green);

        if (Physics.Raycast(transform.position, transform.forward, out rayHit, checkDistance))
        {
            Rigidbody body = rayHit.collider.attachedRigidbody;
            GameObject pushObject = rayHit.collider.gameObject;

            Debug.Log(pushObject.tag);
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
                TryObjectMove(pushObject, body, moveVector);
            }

            return false;
        }
        return true;
    }

    private bool FloorObject(GameObject testObject)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out testRayHit))
        {
            floor = testRayHit.collider.gameObject; // hit.collider.gameObject
            Debug.Log(floor.name);

            if (testObject == floor)
            {
                Debug.Log("Trying to push, while standing on it!");
                return true;
            }
        }
        return false;
    }

    private bool TryObjectMove(GameObject moveObject, Rigidbody rbody, Vector3 direction)
    {
        // rbody.MovePosition(moveObject.transform.position + direction);

        // shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
        Debug.Log("Trying to Move!");
        rbody.linearVelocity = pushForce * direction;

        return false;
    }
}
