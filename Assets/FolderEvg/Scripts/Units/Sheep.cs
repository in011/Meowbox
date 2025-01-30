using UnityEngine;

public class Sheep : MonoBehaviour
{
    private bool burned = false;
    [SerializeField] private int score = 1;

    AudioManager audioManager;
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lava" && !burned)
        {
            audioManager.PlaySFX(audioManager.sheepBurn);

            gameManager.AddScore(score);
            Debug.Log("+1!");

            burned = true;
        }
    }
}
