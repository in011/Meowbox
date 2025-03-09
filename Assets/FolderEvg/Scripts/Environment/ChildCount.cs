using UnityEngine;

public class ChildCount : MonoBehaviour
{
    void Start()
    {
        WithForeachLoop();
        Invoke(nameof(DeactivateAll), 2);
        Invoke(nameof(ActivateAll), 6);
    }

    void WithForeachLoop()
    {
        foreach (Transform child in transform)
        {
            print("Foreach loop: " + child);
        }            
    }
    void DeactivateAll()
    {
        foreach (Transform child in transform)
        {
            print("Deactivated: " + child);
            child.gameObject.SetActive(false);
        }
            
    }
    void ActivateAll()
    {
        foreach (Transform child in transform)
        {
            print("Activated: " + child);
            child.gameObject.SetActive(true);
        }          

    }
}
