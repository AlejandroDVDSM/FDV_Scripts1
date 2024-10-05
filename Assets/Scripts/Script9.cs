using UnityEngine;

public class Script9 : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float accuracy = .01f;

    [SerializeField] private Material defaultWaypointMaterial;
    [SerializeField] private Material currentWaypointMaterial;
    [SerializeField] private Transform[] waypoints;

    private int currentWaypointIndex;
    private Transform currentWaypoint;
    
    private Vector3 direction;
    private Quaternion goalRotation;

    private void Start()
    {
        currentWaypoint = waypoints[currentWaypointIndex];
        waypoints[currentWaypointIndex].gameObject.GetComponent<Renderer>().material = currentWaypointMaterial;
        Debug.Log($"Current waypoint: {currentWaypoint.name}");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, currentWaypoint.position) < accuracy)
            ChangeCurrentWaypoint();
     
        if (currentWaypointIndex >= waypoints.Length)
            return;

        direction = currentWaypoint.position - transform.position;
        goalRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, rotationSpeed * Time.deltaTime);
        
        transform.Translate(0, 0,movementSpeed * Time.deltaTime);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }

    private void ChangeCurrentWaypoint()
    {
        currentWaypoint.GetComponent<Renderer>().material = defaultWaypointMaterial;
        currentWaypointIndex++;
        
        if (currentWaypointIndex >= waypoints.Length)
            return;
        
        currentWaypoint = waypoints[currentWaypointIndex];
        Debug.Log($"Current waypoint: {currentWaypoint.name}");
        currentWaypoint.GetComponent<Renderer>().material = currentWaypointMaterial;
    }
}