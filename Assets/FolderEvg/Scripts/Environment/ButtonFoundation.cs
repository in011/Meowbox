using UnityEngine;

public class ButtonFoundation : MonoBehaviour
{
    private AudioManager audioManager;


    [SerializeField] private Button connectedButton; // button under control
    private bool pressed = false;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Block")) && !pressed)
        {
            audioManager.PlaySFX(audioManager.rockPush);
            connectedButton.SwitchTargetPosition();
            pressed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Block"))
        {
            audioManager.PlaySFX(audioManager.rockPush);
            connectedButton.SwitchTargetPosition();
            pressed = false;
        }
    }
}
