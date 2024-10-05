using UnityEngine;


// 2. El Objetivo no es un objetivo propiamente dicho, sino una dirección en la que queremos movernos. La información
// relevante de un vector es la dirección. Los vectores normalizados, conservan la misma dirección pero su escala no
// afecta al movimiento. Se debe conseguir un movimiento consistente de forma que la escala no afecte a la traslación.
// Del mismo modo, se debe conseguir que el recorrido realizado por el personaje entre un frame y otro no tenga
// aberraciones espacio-temporales. Para ello se debe considerar la relación entre la velocidad, el espacio y el tiempo.
// Por otra parte, el tiempo que transcurre entre un frame y otro se obtiene con: Time.deltaTime. 
// En este ejercicio se pretende dotar de esa consistencia al movimiento que hacer el personaje para ello:
//
// a. Sustituir la dirección del movimiento por su equivalente normalizada. Esto se consigue con el método normalized de
//    la clase Vector3: this.transform.Translate(goal.normalized);
// b. Con el vector normalizado, lo podemos multiplicar por un valor de velocidad para determinar cómo de rápido va el
//    personaje. public float speed = 0.1f this.transform.Translate(goal.normalized*speed)
// c. A pesar de que esas velocidades puedan parecer ahora que son consistentes, no lo son, porque dependen de la
//    velocidad a la que se produzca el update. El tiempo entre dos updates no es necesariamente siempre el mismo, con
//    lo que se pueden tener inconsistencias en la velocidad, y a pesar de que en aplicaciones con poca complejidad no
//    lo notemos, se debe usar: this.transform.Translate(goal.normalized * speed*Time.deltaTime); para suavizar el
//    movimiento ya que Time.deltaTime es el tiempo que ha pasado desde el último frame.

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