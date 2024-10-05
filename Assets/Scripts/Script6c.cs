using UnityEngine;

// b. Realizar un script que gire al personaje y lo desplace hacia su objetivo sobre la dirección que lo une con su
//    objetivo. Normarlizar la dirección para evitar la influencia de la magnitud del vector.
// c. Realizar un script que gire al personaje y lo desplace hacia su objetivo en la dirección que lo une con él,
//    respecto al sistema de referencia mundial. Normarlizar la dirección para evitar la influencia de la magnitud del
//    vector.

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