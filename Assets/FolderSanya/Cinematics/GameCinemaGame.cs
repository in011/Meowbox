using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndSceneLoader : MonoBehaviour
{
    public string cutsceneSceneName = "CutsceneScene"; // Название сцены катсцены

    public void LoadCutscene()
    {
        SceneManager.LoadScene(cutsceneSceneName);
    }
}
