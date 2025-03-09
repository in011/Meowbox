using System.Collections;
using UnityEngine;

public class SoundTrompete : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] public string correctCombination = "RGB"; // Example combination: Red, Green, Blue
    [SerializeField] private GameObject _SoundRed;
    [SerializeField] private GameObject _SoundPurple;
    [SerializeField] private GameObject _SoundBlue;
    private string playerInput = "";

    [SerializeField] private Transform soundSpawn; // place where sounds will be spawned
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float flySpeed = 2f;
    [SerializeField] private float lifetime = 1.5f;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    public void PressButton(string color)
    {
        playerInput += color;
        Debug.Log("Current input: " + playerInput);
        PlayMusic();

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

    private void PlayMusic()
    {
        audioManager.PlaySFX(audioManager.catPush);
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        Debug.Log("Music started");

        foreach (char soundObj in playerInput)
        {
            if(soundObj == 'R')
            {
                GameObject obj = Instantiate(_SoundRed, soundSpawn);
                StartCoroutine(MoveAndDestroy(obj));
                Debug.Log("RED");
            }
            if (soundObj == 'B')
            {
                GameObject obj = Instantiate(_SoundBlue, soundSpawn);
                StartCoroutine(MoveAndDestroy(obj));
                Debug.Log("BLUE");
            }
            if (soundObj == 'P')
            {
                GameObject obj = Instantiate(_SoundPurple, soundSpawn);
                StartCoroutine(MoveAndDestroy(obj));
                Debug.Log("PURP");
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MoveAndDestroy(GameObject obj)
    {
        float elapsedTime = 0f;
        while (elapsedTime < lifetime)
        {
            obj.transform.position += obj.transform.up * flySpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(obj);
    }
}
