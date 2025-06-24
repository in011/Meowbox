using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ExitApp()
    {
        Application.Quit();
    }
    public void LevelOne()
    {
        SceneManager.LoadScene(1);
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene(2);
    }
    public void LevelThree()
    {
        SceneManager.LoadScene(3);
    }
    public void LevelFour()
    {
        SceneManager.LoadScene(4);
    }
    public void LevelFive()
    {
        SceneManager.LoadScene(5);
    }
    public void LevelSix()
    {
        SceneManager.LoadScene(6);
    }
    public void LevelSeven()
    {
        SceneManager.LoadScene(7);
    }
    public void LevelEight()
    {
        SceneManager.LoadScene(8);
    }
}
