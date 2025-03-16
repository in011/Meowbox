using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Ship : MonoBehaviour
{
    [SerializeField] int NextLevel = -1;
    [SerializeField] private float startLevelTime = 1.5f;
    private bool player1 = false;
    private bool player2 = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            player1 = true;
            CheckPlayers();
        }
        if (other.tag == "Player2")
        {
            player2 = true;
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
        if (player1 && player2)
        {
            gameObject.GetComponent<S_MovingPlatform>().StartMoving();
            Invoke("SwimAway", startLevelTime);
        }
    }
    private void SwimAway()
    {
        if (NextLevel >= 0)
        {
            SceneManager.LoadScene(NextLevel);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
