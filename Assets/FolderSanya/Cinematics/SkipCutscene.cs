using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    public PlayableDirector timeline; // Ссылка на Timeline
    public string nextSceneName; // Имя следующей сцены (если нужно)
    public GameObject skipText; // UI-текст "Нажмите пробел, чтобы пропустить"

    void Start()
    {
        if (skipText != null)
        {
            skipText.SetActive(false); // Скрываем текст в начале
        }
    }

    void Update()
    {
        if (timeline != null && timeline.state == PlayState.Playing)
        {
            if (skipText != null) skipText.SetActive(true); // Показываем подсказку
        }

        if (Input.GetKeyDown(KeyCode.Space))
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
