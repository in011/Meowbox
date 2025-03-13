using TMPro;
using UnityEngine;

public class HintPause : MonoBehaviour
{
    //[SerializeField] GameObject Hint;
    [SerializeField] KeyCode ExitButton;
    [SerializeField] TextMeshProUGUI HintText;
    [SerializeField] public string TextField;

    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;

        HintText.text = TextField;
    }

    void Update()
    {
        // Call Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(ExitButton))
        {
            Resume();
        }
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
