using UnityEngine;

public class SoundTrompete : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] public string correctCombination = "RGB"; // Example combination: Red, Green, Blue
    private string playerInput = "";

    public void PressButton(string color)
    {
        playerInput += color;
        Debug.Log("Current input: " + playerInput);

        if (playerInput.Length >= correctCombination.Length)
        {
            CheckCombination();
        }
    }

    private void CheckCombination()
    {
        if (playerInput == correctCombination)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("Incorrect combination, try again.");
            playerInput = ""; // Reset input
        }
    }

    private void OpenDoor()
    {
        Debug.Log("Door unlocked!");
        doorAnimator.SetTrigger("Start");
    }
}
