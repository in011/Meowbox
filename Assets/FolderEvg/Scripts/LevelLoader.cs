using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int specificLevel = -1;
    [SerializeField] private bool twoPlayersToFinish = true;
    private bool player1 = false;
    private bool player2 = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            player1 = true;
            if (!twoPlayersToFinish)
            {
                if(specificLevel >= 0)
                {
                    SceneManager.LoadScene(specificLevel);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            CheckPlayers();
        }
        if (other.tag == "Player2")
        {
            player2 = true;
            if (!twoPlayersToFinish)
            {
                if (specificLevel >= 0)
                {
                    SceneManager.LoadScene(specificLevel);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            CheckPlayers();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            player1 = false;
        }
        if (other.tag == "Player2")
        {
            player2 = false;
        }
    }

    private void CheckPlayers()
    {
        if(player1 && player2)
        {
            if (specificLevel >= 0)
            {
                SceneManager.LoadScene(specificLevel);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
