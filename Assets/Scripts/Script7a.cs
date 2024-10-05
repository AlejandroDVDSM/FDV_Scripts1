using UnityEngine;

// 7. Cuando ejecutamos el script, el personaje calcula la dirección hacia el objetivo y se mueve hacia él, pero no puede
// dejar de moverse y se produce jittering. La razón es que todavía estamos dentro del bucle, calculando la dirección y
// moviéndonos hacia él. En la mayoría de los casos no vamos a conseguir que nuestro personaje se mueva a la posición
// exacta del objetivo, con lo que continuamente oscila en torno a esa posición. Por eso, debemos tener algún cálculo del
// tipo de rango de tolerancia. Incluimos una variable global pública, public float accuracy = 0.01f; y una condición
// if(direction.magnitude > accuracy). Aún con el accuracy, el personaje puede hacer jitter si la velocidad es muy alta.
// 
// a. Controlar el jittering utilizando la magnitud de la dirección.

public class Script7a : MonoBehaviour
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
        
        if (!(direction.magnitude > accuracy)) 
            return;
        
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}