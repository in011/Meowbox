using UnityEngine;

public class Petrification : MonoBehaviour
{
    [SerializeField] private bool hasPetrificationAbility = false;
    [SerializeField] private KeyCode petrifyButton = KeyCode.LeftShift;
    [SerializeField] private float petrificationCD = 1.5f;
    private bool canPetrify = true;
    private AbilityManager abilityManager;
    private Moving moveScript;

    void Start()
    {
        moveScript = gameObject.GetComponent<Moving>();
        abilityManager = FindAnyObjectByType<AbilityManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(petrifyButton) && hasPetrificationAbility && canPetrify)
        {
            if (moveScript.player2)
            {
                canPetrify = false;
                abilityManager.Cat2Petrify(gameObject.transform.position, gameObject.transform.rotation);
            }
            else
            {
                canPetrify = false;
                abilityManager.Cat1Petrify(gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
    public void PetryfyReset()
    {
        Invoke(nameof(ResetCD), petrificationCD);
        //Debug.Log("Wait for CD");
    }
    void ResetCD()
    {
        canPetrify = true;
        //Debug.Log("Can Petrify");
    }
}
