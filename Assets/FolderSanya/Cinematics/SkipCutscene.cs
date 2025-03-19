using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    public PlayableDirector timeline; // Ссылка на Timeline
    public string nextSceneName; // Имя следующей сцены (если нужно)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Можно поменять на нужную кнопку
        {
            Skip();
        }
    }

    public void Skip()
    {
        if (timeline != null)
        {
            timeline.time = timeline.duration; // Перематываем Timeline до конца
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // Загружаем следующую сцену
        }
    }
}
