using UnityEngine;

public class Petrification : MonoBehaviour
{
    [SerializeField] private bool player2 = false;
    [SerializeField] private bool hasPetrificationAbility = false;
    [SerializeField] private KeyCode petrifyButton = KeyCode.LeftShift;
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
        if (Input.GetKeyDown(petrifyButton) && hasPetrificationAbility)
        {
            if (moveScript.player2)
            {
                abilityManager.Cat2Petrify(gameObject.transform.position, gameObject.transform.rotation);
            }
            else
            {
                abilityManager.Cat1Petrify(gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}
