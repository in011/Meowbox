using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private PetrifiedCat petrifCatScript;
    private Moving catScript;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if not pushable start
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            catScript = other.gameObject.GetComponent<Moving>();
            catScript.safePos = other.gameObject.transform.position;
        }
        else
        {
            // Check for petrified cat
            if (other.CompareTag("Block"))
            {
                Rigidbody rb = other.attachedRigidbody;
                if (rb != null)
                {
                    // Check if the collided object has the "PetrifiedCat" script
                    if (other.gameObject.GetComponent("PetrifiedCat") != null)
                    {
                        petrifCatScript = other.gameObject.GetComponent<PetrifiedCat>();
                        if(petrifCatScript.player2)
                        {
                            gameManager.player2.GetComponent<Moving>().safePos = other.gameObject.transform.position;
                        }
                        else
                        {
                            gameManager.player1.GetComponent<Moving>().safePos = other.gameObject.transform.position;
                        }
                    }
                }
                else
                {
                    Debug.Log("No Rigidbody attached to " + other.gameObject.name);
                }
            }
        }
    }
}
