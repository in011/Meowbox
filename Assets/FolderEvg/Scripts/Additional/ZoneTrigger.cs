using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] BlockFallManager Zone;
    [SerializeField] bool doOnce = true;
    bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (doOnce)
        {
            if (!activated)
            {
                if (other.tag == "Player1")
                {
                    Zone.Activate();
                    activated = true;
                }
                if (other.tag == "Player2")
                {
                    Zone.Activate();
                    activated = true;
                }
            }
        }
        else
        {
            if (other.tag == "Player1")
            {
                Zone.Activate();
            }
            if (other.tag == "Player2")
            {
                Zone.Activate();
            }
        }
    }
}
