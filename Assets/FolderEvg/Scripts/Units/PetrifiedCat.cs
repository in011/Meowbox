using UnityEngine;

public class PetrifiedCat : BaseBlock
{
    [SerializeField] float petrificationResetTime = 3f;
    public bool player2 = true;
    private AbilityManager abilityManager;

    void Start()
    {
        abilityManager = FindAnyObjectByType<AbilityManager>();
        gameObject.GetComponent<Rigidbody>().linearDamping = 0;
    }

    public override void Respawn()
    {
        gameObject.GetComponent<Rigidbody>().linearDamping = 10;
        Invoke(nameof(ReTurn), petrificationResetTime);
    }
    private void ReTurn()
    {
        gameObject.GetComponent<Rigidbody>().linearDamping = 0;
        abilityManager.Cat2Restore(gameObject.transform.position + new Vector3(0f, 3f, 0f), gameObject.transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Boss boss;
        if (other.TryGetComponent<Boss>(out boss))
        {
            if (boss.stoned && !boss.hurt && !boss.angry)
            {
                Debug.Log("Hurt!");

                boss.Damage();
            }
        }
    }
}
