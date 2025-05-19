using UnityEngine;
using UnityEngine.Rendering;

public class BossRatManager : MonoBehaviour
{
    [SerializeField] private CauldronFloor[] cauldrons;
    public bool startFlag = false;
    public bool stopFlag = false;

    [SerializeField] private CauldronFloor[] ladderSteps;
    [SerializeField] private S_MovingPlatform[] platforms;
    public bool startLadder = false;
    public bool stopLadder = false;

    private void Update()
    {
        if (startFlag)
        {
            foreach (CauldronFloor cauldron in cauldrons)
            {
                cauldron.GoUp();
            }
            startFlag = false;
        }

        if (stopFlag)
        {
            foreach (CauldronFloor cauldron in cauldrons)
            {
                cauldron.GoDown();
            }
            stopFlag = false;
        }

        if (startLadder)
        {
            foreach (CauldronFloor step in ladderSteps)
            {
                step.GoUp();
            }
            foreach (S_MovingPlatform platform in platforms)
            {
                platform.StartMoving();
            }
            startLadder = false;
        }

        if (stopLadder)
        {
            foreach (CauldronFloor step in ladderSteps)
            {
                step.GoDown();
            }
            stopLadder = false;
        }
    }
}
