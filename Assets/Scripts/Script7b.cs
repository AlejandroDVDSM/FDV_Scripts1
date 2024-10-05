using UnityEngine;

public class Script7b : MonoBehaviour
{
    public Transform goal;
    public float speed = 1.0f;
    public float accuracy = .01f;
    
    private Vector3 direction;
    
    void Start()
    {
        transform.LookAt(goal);
    }
    
    void Update()
    {
        transform.LookAt(goal);

        direction = goal.position - transform.position;
        
        if (!(Vector3.Distance(transform.position, goal.position) > accuracy)) 
            return;
        
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}