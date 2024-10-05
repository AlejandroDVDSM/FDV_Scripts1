using UnityEngine;


// 3. En lugar de seguir usando una dirección como objetivo, vamos a movernos ahora hacia una verdadera posición objetivo.
// Lo agregarermos como un campo público en la clase para poder configurarlo desde le Inspector. También agregaremos un
// campo para configurar la velocidad del personaje desde el propio Inspector. Aunque queramos desplazarnos hacia un
// punto en el espacio, el método Translate debe recibir la dirección del movimiento. La dirección que une dos puntos se
// obtiene restando el más lejano al más cercano. Por último, si el personaje no está encarando el objetivo (podría
// incluso estar de espaldas a él), el desplazamiento será suave pero la orientación de su malla no será consistente.
// Por esta razón será necesario rotarlo de forma que su eje z local (forward) apunte hacia el objetivo.
// La función LookAt del Transform nos ayudará con esto. En este caso, por tanto, para movernos hacia un punto en el
// espacio que configuramos a una velocidad dada:
//
// a. Hacemos el objetivo una variable pública public Transform goal y añadimos un public float speed = 1.0f.
// b. Giramos al personaje para lograr que su movimiento sea hacia delante utilizando this.transform.LookAt(goal.position)
//    en el Start para que gire primero y luego se mueva. 
// c. La dirección en la que nos tenemos que mover viene determinada por la diferencia entre la posición del objetivo y
//    nuestra posición: Vector3 direction = goal.position - this.transform.position; y
//    this.transform.Translate(direction.normalized * speed * Time.deltaTime)
// d. Si lo ejecutamos en este momento, como la orientación del personaje va a cambiar, el translate no va a funcionar
//    correctamente porque los ejes del personaje y el mundo no están alineados. El movimiento se debe hacer de forma
//    relativa al sistema de referencia del mundo.
//    this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World).

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