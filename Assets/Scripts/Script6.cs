using UnityEngine;

public class Script6 : MonoBehaviour
{
    public Transform goal;
    
    public float speed = 1.0f;

    void Start()
    {
        transform.LookAt(goal);
    }
    
    void Update()
    {
        transform.LookAt(goal);
        transform.Translate(Vector3.forward.normalized * (speed * Time.deltaTime));
    }
}