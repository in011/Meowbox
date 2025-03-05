using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int levelToChange;
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
                SceneManager.LoadScene(levelToChange);
            }
            CheckPlayers();
        }
        if (other.tag == "Player2")
        {
            player2 = true;
            if (!twoPlayersToFinish)
            {
                SceneManager.LoadScene(levelToChange);
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

    public void CheckPlayers()
    {
        if(player1 &&  player2)
        {
            SceneManager.LoadScene(levelToChange);
        }
    }
}
