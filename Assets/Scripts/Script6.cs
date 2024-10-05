using UnityEngine;

// 6. En esta sesión se trabaja el Movimiento rectilíneo hacia el objetivo haciendo avanzar al personaje siempre en línea
// recta hacia adelante.  Para ello, el personaje debe rotar hacia el objetivo y luego avanzar en la dirección foward.
// En este caso hay  que destacar que el método Translate de la clase Transform tiene dos formas de realizar la traslación.
// Esto lo podemos resolver rotando al personaje hacia su objetivo (LookAt) y trasladándolo en el eje forward, respecto
// al sistema de referencia local, lo que corresponde al valor por defecto del parámetro de Translate: relativeTo.
//
// Sin embargo, imagina que el personaje está dentro de un vehículo que también se está moviendo. Si solo avanzamos en el
// eje Z local, el personaje se moverá hacia adelante en relación al vehículo, pero no necesariamente hacia el objetivo
// en el mundo. Para resolver esto lo que debemos hacer es movernos en la dirección correcta con respecto a sistema de
// referencia mundial, que corresponde al valor Space.World del parámetro relativeTo de la clase Transform. En este
// ejercicio experimentamos con estas cuestiones:
//
// a. Realizar un script que gire al personaje hacia su objetivo para llegar hasta él desplazándose sobre su vector
//    forward local.

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