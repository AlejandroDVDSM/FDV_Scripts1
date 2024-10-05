using UnityEngine;

public class Script1 : MonoBehaviour
{
    public Vector3 goal;

    void Update()
    {
        transform.Translate(goal);
        goal *= .5f;
    }
}