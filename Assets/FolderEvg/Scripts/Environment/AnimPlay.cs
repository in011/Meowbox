using UnityEngine;

public class AnimPlay : MonoBehaviour
{
    [SerializeField] private Animator[] _animators;
    [SerializeField] private bool restart = false; // Animation can be restarted with trigger "Restart"
    [SerializeField] private bool attackable = false; // Animation can be restarted with trigger "Attacked"
    [SerializeField] private float attackStrengh = 7.5f; // block push speed needed to trigger animation
    [SerializeField] private string triggerName = "Start";
    [SerializeField] private ParticleSystem[] vfx;
    [SerializeField] private float vfxOffTime = 0f; 


    private void OnTriggerEnter(Collider other)
    {
        if (attackable)
        {
            if(other.CompareTag("Block"))
            {
                Rigidbody rb = other.attachedRigidbody;
                Debug.Log("Open!" + rb.linearVelocity.magnitude);
                if (rb.linearVelocity.magnitude > attackStrengh)
                {
                    PlayTrigger(triggerName);
                    foreach (ParticleSystem effect in vfx)
                    {
                        if (effect != null)
                        {
                            effect.Play();
                            if(vfxOffTime <= 0)
                            {
                                Invoke(nameof(VfxOff), vfxOffTime);
                            }
                        }
                    }
                }
            }
            
        }
        else
        {
            // if not pushable start
            if (other.CompareTag("Player1") || other.CompareTag("Player2"))
            {
                PlayTrigger(triggerName);
                foreach (ParticleSystem effect in vfx)
                {
                    if (effect != null)
                    {
                        effect.Play();
                        if (vfxOffTime <= 0)
                        {
                            Invoke(nameof(VfxOff), vfxOffTime);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (restart)
        {
            if (attackable)
            {
                if (other.CompareTag("Block"))
                {
                    Rigidbody rb = other.attachedRigidbody;
                    Debug.Log("Open!" + rb.linearVelocity.magnitude);
                    if (rb.linearVelocity.magnitude > attackStrengh)
                    {
                        PlayTrigger("Restart");
                    }
                }

            }
            else
            {
                // if not pushable start
                if (other.CompareTag("Player1") || other.CompareTag("Player2"))
                {
                    PlayTrigger("Restart");
                }
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
    private void VfxOff()
    {
        foreach (ParticleSystem effect in vfx)
        {
            if (effect)
            {
                effect.Stop();
            }
        }
    }
}
