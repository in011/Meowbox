using UnityEngine;

public class BossFinalStage : MonoBehaviour
{
    [SerializeField] private int bossHealth = 2;
    private bool alive = true;

    [SerializeField] private GameObject fieldOfView;
    [SerializeField] private GameObject DeathBox;
    [SerializeField] private GameObject[] CoverGO;
    [SerializeField] private GameObject bossFloor;
    [SerializeField] private Material matRed;
    [SerializeField] private Material matBlue;
    private Animator animator;
    private bool asleep = true;
    private bool active = false;

    [SerializeField] private S_Waypoint _waypointPath;
    [SerializeField] private float _speed;

    [SerializeField] private GameObject ship;
    [SerializeField] private Transform shipPlace;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool wayBack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<BossFinalStage>().enabled = false;
        Invoke(nameof(TurnOnOff), 0.01f);

        StartMoving();
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    public void TurnOnOff()
    {
        gameObject.GetComponent<BossFinalStage>().enabled = true;
    }

    public void StartMoving()
    {
        //animator.enabled = false;

        transform.position = _waypointPath.transform.position;
        TargetNextWaypoint();
        fieldOfView.SetActive(true);
        DeathBox.SetActive(true);
        active = true;
        asleep = false;
        gameObject.GetComponent<Animator>().enabled = false;

        foreach (GameObject cover in CoverGO)
        {
            cover.SetActive(true);
        }
        bossFloor.SetActive(false);
    }

    void FixedUpdate()
    {
        if (alive)
        {
            animator.enabled = false;

            if (!asleep)
            {
                _elapsedTime += Time.deltaTime;

                float elapsedPercentage = _elapsedTime / _timeToWaypoint;
                elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
                transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
                transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);

                if (elapsedPercentage >= 1)
                {
                    TargetNextWaypoint();
                }
            }
            else
            {
                _elapsedTime += Time.deltaTime;

                float newElapsedPercentage = _elapsedTime / _timeToWaypoint;
                //elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
                transform.position = Vector3.Lerp(startPosition, endPosition, newElapsedPercentage);

                if (newElapsedPercentage >= 1 && !wayBack)
                {
                    Return();
                }
                else
                {
                    if (newElapsedPercentage >= 1 && wayBack)
                    {
                        asleep = false;
                        fieldOfView.GetComponent<Renderer>().material = matBlue;
                        Invoke(nameof(ResetActive), 1f);
                    }
                }
            }
        }        
    }
    public void Damage()
    {
        bossHealth -= 1;
        animator.enabled = false;
        if (bossHealth <= 0)
        {
            alive = false;
            // FINISH
            Debug.Log("WIN");
            animator.enabled = true;
            animator.SetBool("End", true);
            Invoke(nameof(End), 1.5f);
        }
    }
    private void End()
    {
        Instantiate(ship, shipPlace);
        gameObject.SetActive(false);
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player1" || other.tag == "Player2") && active)
        {
            fieldOfView.GetComponent<Renderer>().material = matRed;

            Invoke(nameof(Attack), 0.35f);            
            active = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Player1" || other.tag == "Player2") && active)
        {
            fieldOfView.GetComponent<Renderer>().material = matBlue;
        }
    }

    private void Attack()
    {
        startPosition = transform.position;
        endPosition = transform.position + transform.forward * 25;
        asleep = true;
        wayBack = false;

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(startPosition, endPosition);
        _timeToWaypoint = distanceToWaypoint / (_speed * 2);
    }
    private void Return()
    {
        Vector3 safe = startPosition;
        startPosition = endPosition;
        endPosition = safe;
        wayBack = true;

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(startPosition, endPosition);
        _timeToWaypoint = distanceToWaypoint / (_speed * 2);
    }
    public void Collision()
    {
        endPosition = startPosition;
        startPosition = transform.position;

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(startPosition, endPosition);
        _timeToWaypoint = distanceToWaypoint / (_speed * 2);
    }
    private void ResetActive()
    {
        active = true;
    }
}
