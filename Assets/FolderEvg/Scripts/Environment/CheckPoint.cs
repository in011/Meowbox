using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;

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
            if (catScript.player2)
            {
                catScript.safePos = position2.position;
            }
            else
            {
                catScript.safePos = position1.position;
            }
            
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
                            gameManager.player2.GetComponent<Moving>().safePos = position2.position;
                        }
                        else
                        {
                            gameManager.player1.GetComponent<Moving>().safePos = position1.position;
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
