using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogMenu : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialogTMP;
    [SerializeField] GameObject dPanel;
    [SerializeField] public RawImage face;
    public void WakeUp()
    {
        dialogTMP.gameObject.SetActive(true);
        face.gameObject.SetActive(true);
        dPanel.SetActive(true);
    }
    public void Sleep()
    {
        dialogTMP.gameObject.SetActive(false);
        face.gameObject.SetActive(false);
        dPanel.SetActive(false);
    }
}
