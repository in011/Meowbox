using UnityEngine;

public class S_MovingPlatform : MonoBehaviour
{
    [SerializeField] public bool asleep = false;
    [SerializeField] private S_Waypoint _waypointPath;
    [SerializeField] private float _speed;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;
    
    void Start()
    {
        if (!asleep)
        {
            TargetNextWaypoint();
        }
        gameObject.GetComponent<S_MovingPlatform>().enabled = false;
        Invoke(nameof(TurnOnOff), 0.01f);
    }

    public void TurnOnOff()
    {
        gameObject.GetComponent<S_MovingPlatform>().enabled = true;
    }

    public void StartMoving()
    {
        if (asleep)
        {
            TargetNextWaypoint();
            asleep = false;
        }
    }

    void FixedUpdate()
    {
        if(!asleep)
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
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Block"))
        {
            other.transform.SetParent(transform);
        }
        Debug.Log("Child " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Block"))
        {
            other.transform.SetParent(null);
        }
        Debug.Log("Delete Child " + other.gameObject.name);
    }
}