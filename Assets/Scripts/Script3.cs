using UnityEngine;

public class Script3 : MonoBehaviour
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
        direction = goal.position - transform.position;
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
    }
}