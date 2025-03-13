using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator skeletonAC;
    void Start()
    {
        Invoke(nameof(Call), 2f);
    }

    private void Call()
    {
        skeletonAC.SetBool("Playing", true);
        Debug.Log("Start");
    }

}
