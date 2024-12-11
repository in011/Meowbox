using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int levelToChange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            SceneManager.LoadScene(levelToChange);
        }
        if (other.tag == "Player2")
        {
            SceneManager.LoadScene(levelToChange);
        }
    }

    public void NextLevel()
    {

    }
}
