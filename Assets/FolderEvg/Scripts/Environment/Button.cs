using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Vector3 pressedPosition; // pressed position of the button
    private Vector3 defaultPosition; // Default position when no one is on the button
    private Vector3 addedposition;
    private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 1f;

    private void Start()
    {
        defaultPosition = transform.position;
        addedposition = defaultPosition + pressedPosition;
        targetPosition = defaultPosition;
    }

    public void SwitchTargetPosition()
    {
        if (Vector3.Distance(transform.position, addedposition) < 0.005f)
        {
            targetPosition = defaultPosition; // Ensure exact positioning
        }
        else
        {
            targetPosition = addedposition; // Ensure exact positioning
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else 
        {
            transform.position = targetPosition; // Ensure exact positioning
        }
    }
}
