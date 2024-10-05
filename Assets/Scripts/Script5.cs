using UnityEngine;

public class Script5 : MonoBehaviour
{
    public Transform goal;
    
    public float speed = 1.0f;
    public float speedIncreaser = .5f;
    
    private Vector3 direction;

    void Start()
    {
        transform.LookAt(goal);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed += speedIncreaser;
            Debug.Log(($"Speed increase to {speed}"));
        }
        
        transform.LookAt(goal);
        direction = goal.position - transform.position;
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}