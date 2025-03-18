using UnityEngine;
using System.Collections.Generic;

public class ButtonDoor : MonoBehaviour
{
    [SerializeField] private NewDoorScript door;
    private bool open = false;
    private HashSet<GameObject> objectsInside = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInside.Contains(other.gameObject) && 
            (other.gameObject.tag == "Block" || other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2"))
        {
            objectsInside.Add(other.gameObject);
            door.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInside.Contains(other.gameObject))
        {
            objectsInside.Remove(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        open = CheckCollisions();
        if (!open)
        {
            door.Close();
        }
    }
    private bool CheckCollisions()
    {
        open = false;
        foreach (GameObject obj in objectsInside)
        {
            if (obj == null)
            {
                objectsInside.Remove(obj);
                return CheckCollisions();
            }
            else
            {
                if (obj.tag == "Block" || obj.tag == "Player1" || obj.tag == "Player2")
                {
                    open = true;
                    break;
                }
            }
        }
        return open;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Block")
        {
            door.Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Block")
        {
            door.Close();
        }
    }*/
}
