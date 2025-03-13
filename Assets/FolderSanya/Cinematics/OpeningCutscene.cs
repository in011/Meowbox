using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class IntroSceneLoader : MonoBehaviour
{
    public PlayableDirector timeline;
    public string nextSceneName = "GameLevel"; // Название следующей сцены

    void Start()
    {
        if (timeline != null)
        {
            timeline.stopped += OnTimelineFinished;
        }
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
