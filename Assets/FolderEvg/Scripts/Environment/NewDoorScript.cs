using System.Collections.Generic;
using UnityEngine;

public class NewDoorScript : MonoBehaviour
{
    [SerializeField] private Vector3 pressedPosition; // pressed position of the button
    private Vector3 defaultPosition; // Default position when no one is on the button
    private Vector3 addedposition;
    private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private bool stop = false;

    private void Start()
    {
        defaultPosition = transform.position;
        addedposition = defaultPosition + pressedPosition;
        targetPosition = defaultPosition;
    }
    
    private HashSet<GameObject> objectsInside = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInside.Contains(other.gameObject) && other.gameObject.tag == "Block")
        {
            objectsInside.Add(other.gameObject);
            stop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInside.Contains(other.gameObject))
        {
            objectsInside.Remove(other.gameObject);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            stop = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            stop = false;
        }
    }*/

    public void Open()
    {
        targetPosition = addedposition;
    }
    public void Close()
    {
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
        stop = false;
        foreach(GameObject obj in objectsInside)
        {
            if(obj == null)
            {
                objectsInside.Remove(obj);
            }
            else
            {
                if (obj.tag == "Block")
                {
                    stop = true;
                    break;
                }
            }
        }
        if (!stop)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = targetPosition; // Ensure exact positioning
            }
        }
    }
}
