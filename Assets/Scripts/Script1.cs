using UnityEngine;

// 1. Crear un script que mueva el objeto hacia un punto fijo que se marque como objetivo utilizando el método Translate
// de la clase Transform. El objetivo debe ser una variable pública, de esta forma conseguimos manipularla en el inspector
// y ver el efecto de distintos valores en las coordenadas. Utilizar this.transform.Translate(goal) en el start, solo se
// mueve una vez. Experimentar las siguientes opciones:
//
//     a. Añadir this.transform.Translate(goal); al Update e ir multiplicando goal = goal * 0.5f; en el start para dar
//        saltos más pequeños cada vez.
//     b. Configurar la coordenada Y del Objetivo en 0.
//        CompareTo(). Poner al Objetivo una coordenada Y distinta de cero.
//     d. Modificar el script para que el objeto despegue del suelo y vuele como un avión.
//     e. Duplicar los valores de X, Y, Z del Objetivo. ¿Es consistente el movimiento?.

public class Script1 : MonoBehaviour
{
    public Vector3 goal;

    void Update()
    {
        transform.Translate(goal);
        goal *= .5f;
    }
}