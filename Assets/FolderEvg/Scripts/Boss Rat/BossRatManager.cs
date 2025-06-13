using System;
using UnityEngine;

public class BossRatManager : MonoBehaviour
{
    public int HP = 3;
    public float timeBetweenThrows = 4f;
    bool stageA = true;

    [SerializeField] float firstAttackTime = 3f;

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
        stageA = false;
        foreach (CauldronFloor cauldron in cauldrons)
        {
            cauldron.GoUp();
        }
        switch (HP)
        {
            case > 2:
                firstButtons.Activate();
                // Invoke("ThrowBomb", 4f);
                break;

            case 2:
                secondButtons.Activate();
                Invoke("ThrowBomb", 4f);
                break;

            case 1:
                thirdButtons.Activate();
                Invoke("ThrowBomb", 4f);
                break;

            case 0:
                EndDialog();
                break;

            default:
                Debug.Log("A: Wrong HP");
                break;
        }
    }

    public void StageA()
    {
        stageA = true;
        foreach (CauldronFloor cauldron in cauldrons)
        {
            cauldron.GoDown();
        }
        switch (HP)
        {
            case 2:
                bulletScript.MassThrows();
                break;

            case 1:
                platformScript.Platforms();
                break;

            case 0:
                EndDialog();
                break;

            default:
                Debug.Log("B: Wrong HP");
                break;
        }
    }

    private void EndDialog()
    {
        
    }

    private void ThrowBomb()
    {
        if(!stageA)
        {
            bulletScript.SpawnBottle();

            Invoke("ThrowBomb", timeBetweenThrows);
        }
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
