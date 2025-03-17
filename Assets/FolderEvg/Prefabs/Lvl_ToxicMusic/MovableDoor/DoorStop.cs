using UnityEngine;

public class DoorStop : MonoBehaviour
{
    [SerializeField] private float scanDistance = 1f;
    private Animator _doorAnimator;
    bool m_HitDetect;
    RaycastHit m_Hit;

    void Start()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            _doorAnimator.speed = 0f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            _doorAnimator.speed = 1f;
        }
    }

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        m_HitDetect = Physics.BoxCast(transform.position, transform.localScale * 0.5f, -transform.up, out m_Hit, transform.rotation, scanDistance);
        if (m_HitDetect)
        {
            //Debug.Log("Hit : " + m_Hit.collider.name);

            if (m_Hit.transform.gameObject.tag == "Block")
            {
                _doorAnimator.speed = 0f;
            }
            else
            {
                _doorAnimator.speed = 1f;
            }
        }
        else
        {
            _doorAnimator.speed = 1f;
        }
    }
}
