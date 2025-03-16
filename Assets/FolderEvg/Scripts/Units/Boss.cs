using UnityEngine;

public class Boss : MonoBehaviour
{
    GameManager gameManager;
    AudioManager audioManager;
    private Animator animator;

    [SerializeField] private Moving player1;
    [SerializeField] private Moving player2;
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private int bossHealth = 3;

    [Header("Assets")]
    [SerializeField] private SkinnedMeshRenderer body;
    [SerializeField] private GameObject Horn;
    [SerializeField] private GameObject DeathBox;
    [Header("Hints")]
    [SerializeField] private HintPause hint;
    [SerializeField] private string hintStart;
    [SerializeField] private string hintSpikes;
    private bool secondhintSpikes = false;
    [SerializeField] private string hintHorn;
    private bool secondhintHorn = false;
    [SerializeField] private string hintGroundPound;
    private bool secondTimePound = false;
    private bool secondhintGroundPound = false;
    [SerializeField] private string hintStunned;
    private bool secondhintStunned = false;

    private string hintText;
    private Color matCol1;
    private Color matCol2;
    private Color matCol3;

    [Header("Timings")]
    [SerializeField] private float stunTime = 15f;
    [SerializeField] private float startDelay = 1f;

    public bool hurt = false;
    public bool stunned = false;
    public bool stoned = false;
    public bool angry = false;
    private bool animStop = false;
    private Transform targetPlayer;
    private int currentAttack; // int for keeping track of current attack stage
    private int nextAttack; // int for storing next attack
    private bool targetMoving;
    private bool stop = false;

    [SerializeField] private float scanDistance = 0.1f;
    bool m_HitDetect;
    RaycastHit m_Hit;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        animator = GetComponent<Animator>();
        currentAttack = 0;
        nextAttack = 1;

        hintText = hintStart;
        Invoke(nameof(CallHint), 0.5f);
        Invoke(nameof(NextAttack), startDelay);
    }
    private void FixedUpdate()
    {
        if (!stop)
        {
            switch (currentAttack)
            {
                case 0:
                    break;
                case 1:
                    SpikesAttack();
                    break;
                case 2:
                    Jump();
                    break;
                case 3:
                    RamAttack();
                    break;
                default:
                    Debug.Log("Invalid Attack selection");
                    break;
            }
        }
        
        m_HitDetect = Physics.BoxCast(transform.position, transform.localScale * 0.5f, -transform.up, out m_Hit, transform.rotation, scanDistance);
        if (m_HitDetect)
        {
            if (m_Hit.transform.gameObject.tag == "Wall")
            {
                DeathBox.SetActive(false);
                Debug.Log("DeathDisabled");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PetrifiedCat catStone;
        if (other.TryGetComponent<PetrifiedCat>(out catStone))
        {
            if (stoned && !hurt && !angry)
            {                
                Debug.Log("Stone Hit!");
                Damage();
            }
        }
    }

    private void Update()
    {
        
    }

    public void Damage()
    {
        currentAttack = 0;
        hurt = true;
        animator.SetBool("Spikes", false);
        animator.SetTrigger("Hurt");
        bossHealth -= 1;

        if (bossHealth == 1)
        {
            Invoke(nameof(RedColor), 2f);
            Invoke(nameof(RamAttack), 2f);
            //RamAttack();
        }
        else
        {
            if (!secondhintHorn)
            {
                hintText = hintHorn;
                Invoke(nameof(CallHint), 1f);
                secondhintHorn = true;
            }

            Instantiate(Horn, transform.position + (transform.forward * 5), transform.rotation);
            Invoke(nameof(StoneColor), 2f);
            nextAttack = 2;
            Invoke(nameof(Heal), 6f);
            Invoke(nameof(NextAttack), 6f);
        }
    }
    private void Heal()
    {
        hurt = false;
    }

    private void SpikesAttack()
    {
        currentAttack = 0;
        Debug.Log("SpikesAttack");
        animator.SetBool("Spikes", true);

        Invoke(nameof(Spike), 1f);
        Invoke(nameof(Spike), 2f);
        Invoke(nameof(Spike), 3f);
        Invoke(nameof(Spike), 4f);

        if (!secondhintSpikes)
        {
            hintText = hintSpikes;
            Invoke(nameof(CallHint), 4f);
            secondhintSpikes = true;

        }

        Invoke(nameof(Spike), 5f);
        Invoke(nameof(Spike), 6f);

        if (bossHealth < 3)
        {
            nextAttack = 2;
        }
        else
        {
            nextAttack = 1;
        }
        Invoke(nameof(StopSpikesAnim), 7f);
        Invoke(nameof(NextAttack), 7f);
    }
    private void Spike()
    {
        // Choose a random player to target
        ChoosePlayer();
        if (!hurt)
        {
            if (targetMoving)
            {
                Instantiate(spikePrefab, targetPlayer.transform.position + targetPlayer.transform.forward * 5 + new Vector3(0f, 25f, 0f), Quaternion.identity);
            }
            else
            {
                Instantiate(spikePrefab, targetPlayer.transform.position + new Vector3(0f, 25f, 0f), Quaternion.identity);
            }
        }
    }
    private void StopSpikesAnim()
    {
        animator.SetBool("Spikes", false);
    }

    private void Jump()
    {
        currentAttack = 0;
        animator.SetTrigger("Jump");
        Invoke(nameof(GroundPoundAttack), 0.5f);
    }
    private void GroundPoundAttack()
    {
        Debug.Log("GroundPound");
        currentAttack = 0;

        if (secondTimePound)
        {
            if (!secondhintGroundPound)
            {
                hintText = hintGroundPound;
                Invoke(nameof(CallHint), 4f);
                secondhintGroundPound = true;
            }
        }
        else
        {
            secondTimePound = true;
        }

        // Choose a random player to target
        ChoosePlayer();
        if (targetMoving)
        {
            animator.enabled = false; 
            transform.position = targetPlayer.transform.position + targetPlayer.transform.forward * 4 + new Vector3(0f, 25f, 0f);
        }
        else
        {
            animator.enabled = false;
            transform.position = targetPlayer.transform.position + new Vector3(0f, 25f, 0f);
        }
        DeathBox.SetActive(true);
        Debug.Log("DeathEnabled");
        Invoke(nameof(DisableAnimator), 2f);
        // Invoke(nameof(ReturnColor), 2f);


        nextAttack = 1;
        Invoke(nameof(EnableAnimator), 4f);
        Invoke(nameof(NextAttack), 4f);
    }
    private void DisableAnimator()
    {
        animator.enabled = false;
    }
    private void EnableAnimator()
    {
        DeathBox.SetActive(false);
        if (!stunned)
        {
            animator.enabled = true;
        }
        else
        {
            animStop = true;
        }
    }

    private void RamAttack()
    {
        Debug.Log("Ram");
        currentAttack = 0;
        stop = true;

        gameObject.GetComponent<BossFinalStage>().enabled = true;
        gameObject.GetComponent<Boss>().enabled = false;
    }

    public void Stunned()
    {
        Debug.Log("Stunned");
        if (!secondhintStunned)
        {
            hintText = hintStunned;
            Invoke(nameof(CallHint), 0.1f);
            secondhintStunned = true;
        }

        animator.SetBool("Stunned", true);
        DeathBox.SetActive(false);
        currentAttack = 0;
        stunned = true;
        Invoke(nameof(StartAgain), stunTime);
    }
    private void StartAgain()
    {
        stunned = false;
        if(animStop)
        {
            animator.enabled = true;
            animStop = false;
        }
        animator.SetBool("Stunned", false);
        currentAttack = 1;
    }

    private void ChoosePlayer()
    {
        //targetPlayer = player1.transform;
        //targetMoving = player1.animator.GetBool("isRunning");
        
        // Check if dead
        if (gameManager.player1Dead)
        {
            targetPlayer = player2.transform;
            targetMoving = player2.animator.GetBool("isRunning");
        }
        else
        {
            if (gameManager.player2Dead)
            {
                targetPlayer = player1.transform;
                targetMoving = player1.animator.GetBool("isRunning");
            }
            else
            {
                if (Random.value > 0.5f)
                {
                    targetPlayer = player1.transform;
                    targetMoving = player1.animator.GetBool("isRunning");
                }
                else
                {
                    targetPlayer = player2.transform;
                    targetMoving = player2.animator.GetBool("isRunning");
                }
            }
        }
    }
    private void WakeUp()
    {
        animator.SetBool("Stunned", false);
    }
    private void NextAttack()
    {
        if (!stunned && !hurt)
        {
            currentAttack = nextAttack;
        }        
    }
    private void CallHint()
    {
        hint.TextField = hintText;
        hint.Pause();
    }

    // Color work
    private void StoneColor()
    {
        stoned = true;
        matCol1 = body.materials[0].color;
        body.materials[0].color = Color.grey;
        matCol2 = body.materials[1].color;
        body.materials[1].color = Color.grey;
        matCol3 = body.materials[2].color;
        body.materials[2].color = Color.grey;
    }
    private void RedColor()
    {
        angry = true;
        matCol1 = body.materials[0].color;
        body.materials[0].color = Color.red;
        matCol2 = body.materials[1].color;
        body.materials[1].color = Color.red;
        matCol3 = body.materials[2].color;
        body.materials[2].color = Color.red;
    }
    private void ReturnColor()
    {
        body.materials[0].color = matCol1;
        body.materials[1].color = matCol2;
        body.materials[2].color = matCol3;
    }
}