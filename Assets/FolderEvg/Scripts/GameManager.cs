using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AudioManager audioManager;
    LevelLoader levelLoader;
    [SerializeField] public HintPause hint;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] PauseMenu gameOverMenu;
    private bool gameover = false;

    [SerializeField] private bool onPlayerRespawn = false;
    [SerializeField] public GameObject player1;
    private Moving player1Script;
    public bool player1Dead = false;
    [SerializeField] public GameObject player2;
    private Moving player2Script;
    public bool player2Dead = false;

    [SerializeField] private int score = 0;
    [SerializeField] private int scoreNeeded = 1000;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject portalPlaceEmpty;

    [Header("Falling Blocks")]
    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private bool blockFall = false;
    [SerializeField] private float timeBetweenBlocks = 3f;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        levelLoader = FindAnyObjectByType<LevelLoader>();

        player1Script = player1.GetComponent<Moving>();
        player2Script = player2.GetComponent<Moving>();

        // Lock and disable Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (blockFall)
        {
            Invoke(nameof(DropBlock), timeBetweenBlocks); //            
        }
    }
    void Update()
    {
        // Call Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape) && !gameover)
        {
            pauseMenu.Pause();
        }
    }

    public void Player1Death()
    {
        audioManager.PlaySFX(audioManager.catBurn);

        player1Dead = true;
        if (player2Dead)
        {
            Debug.Log("GAME OVER!");
            gameOverMenu.Pause();
            gameover = true;
        }

        player1Script.Death();
        if (onPlayerRespawn)
        {
            player1Script.safePos = player2.transform.position;
        }
        player1.transform.position = player1Script.safePos;
        Invoke(nameof(RespawnPlayer1), 6); // ResetJump jumpCooldown       
        //StartCoroutine(Timer(5));
    }
    public void Player2Death()
    {
        audioManager.PlaySFX(audioManager.catBurn);

        player2Dead = true;
        if (player1Dead)
        {
            Debug.Log("GAME OVER!");
            gameOverMenu.Pause();
            gameover = true;
        }

        player2Script.Death();
        if(onPlayerRespawn)
        {
            player2Script.safePos = player1.transform.position;
        }
        player2.transform.position = player2Script.safePos;
        Invoke(nameof(RespawnPlayer2), 6); // ResetJump jumpCooldown       
        //StartCoroutine(Timer(5));
    }

    private void RespawnPlayer1()
    {
        player1Dead = false;
        player1Script.Revive();
    }
    private void RespawnPlayer2()
    {
        player2Dead = false;
        player2Script.Revive();
    }

    //                (       )
    IEnumerator Timer(int seconds)
    {
        do
        {
            --seconds;
            Debug.Log(seconds + "...");
            yield return new WaitForSeconds(1.0f);
        } while (seconds > 0);
    }

    private void DropBlock()
    {
        int randXPos = Random.Range(-7, 5);
        int randZPos = Random.Range(-7, 5);

        if (randXPos % 2 == 0)
        {
            randXPos -= 1;
        }
        if (randZPos % 2 == 0)
        {
            randZPos -= 1;
        }

        Instantiate(blockPrefabs[0], new Vector3((float)randXPos, 20f, (float)randZPos), blockPrefabs[0].transform.rotation);

        Invoke(nameof(DropBlock), timeBetweenBlocks); //                        
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (score >= scoreNeeded)
        {
            Invoke(nameof(SpawnPortal), 0.5f); //                        
        }
        else
        {
            Debug.Log("Not enough " + score);
        }
    }
    private void SpawnPortal()
    {
        Debug.Log("PORTAL");
        Instantiate(portal, portalPlaceEmpty.transform.position, portalPlaceEmpty.transform.rotation);
    }
}