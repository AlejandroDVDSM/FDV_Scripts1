using UnityEngine;
using UnityStandardAssets.Utility;

// 10. En esta sección se trabaja con el sistema de Waypoints de Unity. Para ello debes importar como asset en el proyecto
// la carpeta Utility. Configura el circuito, agrega el objetivo que debe perseguir el personaje y añade al personaje que
// recorrerá el circuito el script WaypointProgressTracker. Finalmente agrega un script al personaje que lo haga perseguir
// al objetivo. El sistema moverá el objetivo alejándolo del personaje moviéndose de un punto a otro del circuito.
// El personaje intenta perseguir al objetivo con nuestro script, por tanto, está “obligando” al objetivo a ir de un
// punto a otro a la par que lo persigue.

public class Script10 : MonoBehaviour
{
    public float movementspeed = 1.0f;
    public float rotationSpeed = 1.0f;
    public float accuracy = .01f;

    private Transform goal;
    private Vector3 direction;
    private Quaternion goalRotation;

    private void Start()
    {
        goal = GetComponent<WaypointProgressTracker>().target;
    }

    void Update()
    {
        direction = goal.position - transform.position;
        goalRotation = Quaternion.LookRotation(direction);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, rotationSpeed * Time.deltaTime);
            
        if (!(direction.magnitude > accuracy)) 
            return;
        
        transform.Translate(direction.normalized * (movementspeed * Time.deltaTime), Space.World);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}