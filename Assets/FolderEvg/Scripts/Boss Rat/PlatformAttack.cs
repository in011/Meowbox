using UnityEngine;

public class PlatformAttack : MonoBehaviour
{
    [SerializeField] BossRatManager bossManager;

    [SerializeField] private MoveObject Lava;
    [SerializeField] private CauldronFloor[] ladderSteps;
    [SerializeField] private GameObject platform;
    [SerializeField] private Transform[] PlatformStarts;
    [SerializeField] private float platformDeath = 20f;

    public void Platforms()
    {
        foreach (CauldronFloor step in ladderSteps)
        {
            step.GoUp();
        }
        Invoke("LavaUp", 3);

        Debug.Log("Platforms");
        PlatformOne();
        Invoke("PlatformOne", 3);
        Invoke("PlatformZero", 5);
        Invoke("PlatformOne", 7);
        Invoke("PlatformTwo", 9);
        Invoke("PlatformThree", 11);
        Invoke("PlatformTwo", 13);
        Invoke("PlatformTwo", 15);
        Invoke("PlatformOne", 17);
        Invoke("PlatformOne", 19);

        Invoke("LavaDown", 22);

        Invoke("LadderDown", 32);

        Invoke("AttackEnd", 40);
    }
    private void AttackEnd()
    {
        bossManager.free = true;
    }
    private void LadderDown()
    {
        foreach (CauldronFloor step in ladderSteps)
        {
            step.GoDown();
        }

    }
    private void LavaUp()
    {
        Lava.GoUp();

    }

    private void LavaDown()
    {
        Lava.GoDown();

    }
    private void PlatformZero()
    {
        GameObject obj = Instantiate(platform, PlatformStarts[0]);
        obj.GetComponentInChildren<S_MovingPlatform>().StartMoving();
        Destroy(obj, platformDeath);
    }
    private void PlatformOne()
    {
        GameObject obj = Instantiate(platform, PlatformStarts[1]);
        obj.GetComponentInChildren<S_MovingPlatform>().StartMoving();
        Debug.Log("One");
        Destroy(obj, platformDeath);
    }
    private void PlatformTwo()
    {
        GameObject obj = Instantiate(platform, PlatformStarts[2]);
        obj.GetComponentInChildren<S_MovingPlatform>().StartMoving();
        Debug.Log("Two");
        Destroy(obj, platformDeath);
    }
    private void PlatformThree()
    {
        GameObject obj = Instantiate(platform, PlatformStarts[3]);
        obj.GetComponentInChildren<S_MovingPlatform>().StartMoving();
        Debug.Log("Three");
        Destroy(obj, platformDeath);
    }
}
