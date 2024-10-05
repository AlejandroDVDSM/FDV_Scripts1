using UnityEngine;

// 5. Agregar un cubo en la escena que hará de objetivo, que debe ser movido usando el controlador de los Starter Assets.
// Sobre la escena que has trabajado ubica un personaje que va a seguir al cubo. 
//
// a. Crear un script que haga que el personaje siga al cubo continuamente sin aplicar simulación física.
// b. Agregar un campo público que permita graduar la velocidad del movimiento desde el inspector de objetos.
// c. Utilizar la tecla de espaciado para incrementar la velocidad del desplazamiento en el tiempo de juego.

public class Script5 : MonoBehaviour
{
    public Transform goal;
    
    public float speed = 1.0f;
    public float speedIncreaser = .5f;
    
    private Vector3 direction;

    void Start()
    {
        transform.LookAt(goal);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed += speedIncreaser;
            Debug.Log(($"Speed increase to {speed}"));
        }
        
        transform.LookAt(goal);
        direction = goal.position - transform.position;
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}