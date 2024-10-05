using UnityEngine;

// 8. En esta sesión se trabaja el Movimiento rectilíneo haciendo avanzar al personaje siempre en línea recta hacia
// adelante introduciendo una mejora. El uso de la función LookAt hace que el personaje gire instantáneamente hacia el
// objetivo, provocando cambios bruscos. Se aconseja realizar una transición suave a lo largo de diferentes frames.
// Para ello, en lugar de computar una rotación del ángulo necesario, se realizan sucesivas rotaciones donde el ángulo
// en cada frame viene dado por los valores intermedios al interpolar la dirección original y la final. Para esto
// utilizaremos la función Slerp de la clase Quaternion.
// Un quaternion es un instrumento matemático que facilita el cálculo de rotaciones evitando el Gimbal Lock.

public class Script8 : MonoBehaviour
{
    public Transform goal;
    public float movementspeed = 1.0f;
    public float rotationSpeed = 1.0f;
    public float accuracy = .01f;
    
    private Vector3 direction;
    private Quaternion goalRotation;
    
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