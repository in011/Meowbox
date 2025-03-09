using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private SoundTrompete trompet;
    [SerializeField] private string buttonInput = "R";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Block"))
        {
            trompet.PressButton(buttonInput);
        }
    }
}
