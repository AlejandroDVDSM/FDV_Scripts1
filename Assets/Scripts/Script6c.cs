using UnityEngine;

public class Script6c : MonoBehaviour
{
    public Transform goal;
    
    public float speed = 1.0f;

    
    private Vector3 direction;
    
    void Start()
    {
        transform.LookAt(goal);
    }
    
    void Update()
    {
        transform.LookAt(goal);

        direction = goal.position - transform.position;
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}