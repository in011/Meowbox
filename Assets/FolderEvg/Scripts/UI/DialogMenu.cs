using UnityEngine;
using TMPro;

public class DialogMenu : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialogTMP;
    [SerializeField] GameObject dPanel;
    public void WakeUp()
    {
        dialogTMP.gameObject.SetActive(true);
        dPanel.SetActive(true);
    }
    public void Sleep()
    {
        dialogTMP.gameObject.SetActive(false);
        dPanel.SetActive(false);
    }
}
