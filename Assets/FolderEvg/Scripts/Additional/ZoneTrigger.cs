using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] BlockFallManager Zone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            Zone.Activate();
            Debug.Log("Activated!");
        }
        if (other.tag == "Player2")
        {
            Zone.Activate();
            Debug.Log("Activated!");
        }
    }
}
