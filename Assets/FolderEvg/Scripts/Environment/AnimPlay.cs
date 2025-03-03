using UnityEngine;

public class AnimPlay : MonoBehaviour
{
    [SerializeField] private Animator[] _animators;
    [SerializeField] private bool restart = false; // Animation can be restarted with trigger "Restart"
    [SerializeField] private bool attackable = false; // Animation can be restarted with trigger "Attacked"
    [SerializeField] private float attackStrengh = 7.5f; // block push speed needed to trigger animation


    private void OnTriggerEnter(Collider other)
    {
        if (attackable && other.CompareTag("Block"))
        {
            Rigidbody rb = other.attachedRigidbody;
            Debug.Log("Open!" + rb.linearVelocity.magnitude);
            if (rb.linearVelocity.magnitude > attackStrengh)
            {
                PlayTrigger("Attacked");
            }
        }
        else
        {
            // if not pushable start
            if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Block"))
            {
                PlayTrigger("Start");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (restart)
        {
            if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Block"))
            {
                PlayTrigger("Restart");
            }
        }
    }
    private void PlayTrigger(string trigStr)
    {
        foreach(Animator anim in  _animators)
        {
            anim.SetTrigger(trigStr);
        }
    }
}
