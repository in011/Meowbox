using UnityEngine;

public class BossRatManager : MonoBehaviour
{
    public int HP = 3;

    [SerializeField] float firstAttackTime = 3f;

    public bool free = false;
    [SerializeField] PlatformAttack platformScript;
    [SerializeField] BulletHell bulletScript;

    [SerializeField] private CauldronFloor[] cauldrons;
    public bool startFlag = false;
    public bool stopFlag = false;

    [SerializeField] DualButtonManager firstButtons;
    [SerializeField] DualButtonManager secondButtons;
    [SerializeField] DualButtonManager thirdButtons;

    private void Start()
    {

        Invoke("AttackStart", firstAttackTime);
    }

    public void StageB()
    {
        firstButtons.Activate();
        foreach (CauldronFloor cauldron in cauldrons)
        {
            cauldron.GoUp();
        }
    }

    public void StageA()
    {
        firstButtons.Activate();
        foreach (CauldronFloor cauldron in cauldrons)
        {
            cauldron.GoDown();
        }
    }

    private void Update()
    {
        
    }

    private void AttackStart()
    {
        bulletScript.StartSpew();
    }
    private void PlatformsStart()
    {
        platformScript.Platforms();
    }
}
