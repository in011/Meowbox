using UnityEngine;

public class ButtonUP : MonoBehaviour
{
    [SerializeField] private Animator ACanim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            ACanim.SetTrigger("Up");
        }
    }
}
