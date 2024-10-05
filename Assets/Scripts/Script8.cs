using UnityEngine;

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