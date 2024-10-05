using UnityEngine;

public class Script2 : MonoBehaviour
{
    public Vector3 goal;
    public float speed = .1f;
    
    void Update()
    {
        transform.Translate(goal.normalized * (speed * Time.deltaTime));
        goal *= .5f;
    }
}