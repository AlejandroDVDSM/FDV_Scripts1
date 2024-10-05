using UnityEngine;

// 9. En esta sección se trabaja un sistema básico de Waypoints. Se debe crear un circuito en una escena con la colección
// de puntos que conforman el circuito. Cada punto del circuito será un objeto 3D al que se le asigne la etiqueta
// “waypoint”. También se agregará un objeto personaje que será el que recorra los objetivos. Este objeto debe implementar
// el script con la mecánica de recorrido del circuito. Para ello, debe recuperar la referencia a cada uno de los objetivo
// y realizar los desplazamientos de un objetivo a otro aplicando el trabajo anterior. En la lógica se debe incluir la
// gestión de obtener quién es el siguiente objetivo.

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