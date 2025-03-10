using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    [Header("Petrify")]
    // White
    [SerializeField] private GameObject player1;
    [SerializeField] private KeyCode abilityButton1 = KeyCode.LeftShift;
    [SerializeField] private GameObject petrifiedPrefab1;
    private GameObject petrifCat1;
    private bool isRock1 = false;
    // Black
    [SerializeField] private GameObject player2;
    [SerializeField] private KeyCode abilityButton2 = KeyCode.RightControl;
    [SerializeField] private GameObject petrifiedPrefab2;
    private GameObject petrifCat2;
    private bool isRock2 = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Cat1Petrify(Vector3 pos, Quaternion rot)
    {
        player1.GetComponent<Moving>().Deactivate();
        petrifCat1 = Instantiate(petrifiedPrefab1, pos, petrifiedPrefab1.transform.rotation);
        petrifCat1.GetComponent<PetrifiedCat>().player2 = false;

        Invoke(nameof(WaitForSignal1), 0.1f);
    }
    public void Cat1Restore(Vector3 pos, Quaternion rot)
    {
        isRock1 = false;
        player1.transform.position = pos;
        player1.GetComponent<Moving>().Reactivate();
        player1.GetComponent<Petrification>().PetryfyReset();
        player1.GetComponent<Moving>().PushUp();
    }
    void WaitForSignal1()
    {
        isRock1 = true;
    }

    public void Cat2Petrify(Vector3 pos, Quaternion rot)
    {

        player2.GetComponent<Moving>().Deactivate();
        petrifCat2 = Instantiate(petrifiedPrefab2, pos + new Vector3(0f,1f,0f), petrifiedPrefab2.transform.rotation);
        petrifCat2.GetComponent<PetrifiedCat>().player2 = true;
        
        Invoke(nameof(WaitForSignal2), 0.1f);
    }
    public void Cat2Restore(Vector3 pos, Quaternion rot)
    {
        isRock2 = false;
        player2.transform.position = pos;
        player2.GetComponent<Moving>().Reactivate();
        player2.GetComponent<Petrification>().PetryfyReset();
        player2.GetComponent<Moving>().PushUp();
    }
    void WaitForSignal2()
    {
        isRock2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRock1)
        {
            if (Input.GetKeyDown(abilityButton1))
            {
                if (isRock1)
                {
                    Vector3 restPos = petrifCat1.transform.position;
                    Quaternion restRot = petrifCat1.transform.rotation;
                    Destroy(petrifCat1);
                    Cat1Restore(restPos, restRot);
                }
            }
        }
        
        if(isRock2)
        {
            if (Input.GetKeyDown(abilityButton2))
            {
                Vector3 restPos = petrifCat2.transform.position;
                Quaternion restRot = petrifCat2.transform.rotation;
                Destroy(petrifCat2);
                Cat2Restore(restPos, restRot);
            }
        }
    }

}
