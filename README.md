# FDV_Scripts1

## **1. Crear un script que mueva el objeto hacia un punto fijo que se marque como objetivo utilizando el método `Translate` de la clase `Transform`. El objetivo debe ser una variable pública, de esta forma conseguimos manipularla en el inspector y ver el efecto de distintos valores en las coordenadas. Utilizar `this.transform.Translate(goal)` en el start.**

```c#
using UnityEngine;

public class Script1 : MonoBehaviour
{
    public Vector3 goal;

    void Start()
    {
        Debug.Log($"Cube position before {transform.position}");
        transform.Translate(goal);
        Debug.Log($"Cube position after {transform.position}");
    }
}
```

![1  Translate Start](https://github.com/user-attachments/assets/57dff5c1-7e52-4f8f-accc-3db14ea120de)

### _a. Añadir `this.transform.Translate(goal);` al `Update` e ir multiplicando `goal = goal * 0.5f;` en el update para dar saltos más pequeños cada vez._

```c#
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
```

![2  Ejercicio1a](https://github.com/user-attachments/assets/e1d1140a-a244-4715-98bf-8fd41d5d8a01)

### _b. Configurar la coordenada Y del Objetivo en 0._

![image](https://github.com/user-attachments/assets/fc915bb3-c554-4321-a2d6-424b925b4eb8)

![3  Ejercicio1b](https://github.com/user-attachments/assets/ea5bd2ae-9068-4c0f-a45a-ad0c95b50ded)

### _c. Poner al Objetivo una coordenada Y distinta de cero._

El resultado es el mismo que el mostrado en el apartado "1a", donde la coordenada Y ya era distinta de 0.

### _d. Modificar el script para que el objeto despegue del suelo y vuele como un avión._

No hace falta modificar el script. Tan solo es necesario asignar un `goal` cuyo valor en `X` sea 0 y cuyos valores en `Y` y `Z` sean mayores de 0 para simular un movimiendo diagonal, tal y como haría un avión.

![image](https://github.com/user-attachments/assets/f0de45c3-00e2-4330-9a15-e9d140264b12)

![4  Ejercicio 1d](https://github.com/user-attachments/assets/e6620b2f-3664-42e7-a51a-6f4e92ab9b82)

### _e. Duplicar los valores de X, Y, Z del Objetivo. ¿Es consistente el movimiento?._

El movimiento no es consistente dado que a que el cubo avanza a "trompicones", su movimiento no es _smooth_.

## **2. El Objetivo no es un objetivo propiamente dicho, sino una dirección en la que queremos movernos. La información relevante de un vector es la dirección. Los vectores normalizados, conservan la misma dirección pero su escala no afecta al movimiento. Se debe conseguir un movimiento consistente de forma que la escala no afecte a la traslación. Del mismo modo, se debe conseguir que el recorrido realizado por el personaje entre un frame y otro no tenga aberraciones espacio-temporales. Para ello se debe considerar la relación entre la velocidad, el espacio y el tiempo. Por otra parte, el tiempo que transcurre entre un frame y otro se obtiene con: `Time.deltaTime`.**

### _a. Sustituir la dirección del movimiento por su equivalente normalizada._
### _b. Con el vector normalizado, lo podemos multiplicar por un valor de velocidad para determinar cómo de rápido va el personaje_
### _c. Usar `Time.deltaTime` para suavizar el movimiento_

```c#
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
```

![2  Ejercicio 2c](https://github.com/user-attachments/assets/9f2203b8-3b4d-44f9-8466-1430daffd46f)

## **3. En lugar de seguir usando una dirección como objetivo, vamos a movernos ahora hacia una verdadera posición objetivo. Lo agregarermos como un campo público en la clase para poder configurarlo desde le Inspector. También agregaremos un campo para configurar la velocidad del personaje desde el propio Inspector. Aunque queramos desplazarnos hacia un punto en el espacio, el método Translate debe recibir la dirección del movimiento. La dirección que une dos puntos se obtiene restando el más lejano al más cercano. Por último, si el personaje no está encarando el objetivo (podría incluso estar de espaldas a él), el desplazamiento será suave pero la orientación de su malla no será consistente. Por esta razón será necesario rotarlo de forma que su eje z local (forward) apunte hacia el objetivo. La función `LookAt` del `Transform` nos ayudará con esto. En este caso, por tanto, para movernos hacia un punto en el espacio que configuramos a una velocidad dada:**

### _a. Hacemos el objetivo una variable pública `public Transform goal` y añadimos un `public float speed = 1.0f;`_
### _b. Giramos al personaje para lograr que su movimiento sea hacia delante utilizando `this.transform.LookAt(goal.position)` en el `Start` para que gire primero y luego se mueva._
### _c. La dirección en la que nos tenemos que mover viene determinada por la diferencia entre la posición del objetivo y nuestra posición:_
 `Vector3 direction = goal.position - this.transform.position;`
 `this.transform.Translate(direction.normalized * speed * Time.deltaTime)`
### _d. Si lo ejecutamos en este momento, como la orientación del personaje va a cambiar, el translate no va a funcionar correctamente porque los ejes del personaje y el mundo no están alineados. El movimiento se debe hacer de forma relativa al sistema de referencia del mundo._
`this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World).`

```c#
using UnityEngine;

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
```

![1  Ejercicio 3](https://github.com/user-attachments/assets/71054a5c-5c1f-49f8-bb95-ced583242376)

## **4. Añadir `Debug.DrawRay(this.transform.position,direction,Color.red)` para depuración para comprobar que la dirección está correctamente calculada.**

```c#
using UnityEngine;

public class Script4 : MonoBehaviour
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
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}
```
![Ejercicio4](https://github.com/user-attachments/assets/8b965b0c-595e-4a40-804d-f77de7a0f431)

## **5. Agregar un cubo en la escena que hará de objetivo, que debe ser movido usando el controlador de los Starter Assets. Sobre la escena que has trabajado ubica un personaje que va a seguir al cubo.**

Se crea un nuevo cubo al cual se le añaden los componentes `CharacterController`, `PlayerInput`, `ThirdPersonController` y `StarterAssetsInputs` para poder manejarlo con las teclas WASD.

![image](https://github.com/user-attachments/assets/9246f104-3017-4e3c-a20c-aefc6ab81b8c)

### _a. Crear un script que haga que el personaje siga al cubo continuamente sin aplicar simulación física._
### _b. Agregar un campo público que permita graduar la velocidad del movimiento desde el inspector de objetos._
### _c. Utilizar la tecla de espaciado para incrementar la velocidad del desplazamiento en el tiempo de juego._

Para no aplicar simulación física, desactivaremos cualquier componente de tipo `Collider` en la escena.

```c#
using UnityEngine;

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
```

![Ejercicio 5](https://github.com/user-attachments/assets/20b1659f-49d1-4f22-98d8-b5cae3be347e)

## **6. Realizar un script que gire al personaje hacia su objetivo para llegar hasta él desplazándose sobre su vector forward local.**

```c#
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
```

![1  Ejercicio 6a](https://github.com/user-attachments/assets/e6fbf219-284f-42fb-9cd6-1acfd9d781a7)


### _b. Realizar un script que gire al personaje y lo desplace hacia su objetivo sobre la dirección que lo une con su objetivo. Normarlizar la dirección para evitar la influencia de la magnitud del vector_
### _c. Realizar un script que gire al personaje y lo desplace hacia su objetivo en la dirección que lo une con él, respecto al sistema de referencia mundial. Normarlizar la dirección para evitar la influencia de la magnitud del vector._

```c#
using UnityEngine;

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
```

![2  Ejercicio 6c](https://github.com/user-attachments/assets/8db9c0ea-7e91-4a85-9792-cc6b2bc00ab9)

## **7. Cuando ejecutamos el script, el personaje calcula la dirección hacia el objetivo y se mueve hacia él, pero no puede dejar de moverse y se produce _jittering_. La razón es que todavía estamos dentro del bucle, calculando la dirección y moviéndonos hacia él. En la mayoría de los casos no vamos a conseguir que nuestro personaje se mueva a la posición exacta del objetivo, con lo que continuamente oscila en torno a esa posición. Por eso, debemos tener algún cálculo del tipo de rango de tolerancia. Incluimos una variable global pública, `public float accuracy = 0.01f;` y una condición `if(direction.magnitude > accuracy)`. Aún con el `accuracy`, el personaje puede hacer _jitter_ si la velocidad es muy alta.**

### _a. Controlar el jittering utilizando la magnitud de la dirección._

```c#
using UnityEngine;

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
```

![1  Ejercicio 7a](https://github.com/user-attachments/assets/29072fdf-4199-4f1d-8bb5-e14e88297816)

### _b. Controlar el jittering utilizando la distancia entre los dos puntos._

```c#
using UnityEngine;

public class Script7b : MonoBehaviour
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
        
        if (!(Vector3.Distance(transform.position, goal.position) > accuracy)) 
            return;
        
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}
```

_El resultado es el mismo que el gif anterior._

## **8. En esta sesión se trabaja el Movimiento rectilíneo haciendo avanzar al personaje siempre en línea recta hacia adelante introduciendo una mejora. El uso de la función `LookAt` hace que el personaje gire instantáneamente hacia el objetivo, provocando cambios bruscos. Se aconseja realizar una transición suave a lo largo de diferentes frames. Para ello, en lugar de computar una rotación del ángulo necesario, se realizan sucesivas rotaciones donde el ángulo en cada frame viene dado por los valores intermedios al interpolar la dirección original y la final. Para esto utilizaremos la función `Slerp` de la clase `Quaternion`.**

```c#
using UnityEngine;

public class Script8 : MonoBehaviour
{
    public Transform goal;
    public float movementspeed = 1.0f;
    public float rotationSpeed = 1.0f;
    public float accuracy = .01f;
    
    private Vector3 direction;
    private Quaternion goalRotation;
    
    void Update()
    {
        direction = goal.position - transform.position;
        goalRotation = Quaternion.LookRotation(direction);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, rotationSpeed * Time.deltaTime);
            
        if (!(direction.magnitude > accuracy)) 
            return;
        
        transform.Translate(direction.normalized * (movementspeed * Time.deltaTime), Space.World);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}
```

![1  Ejercicio 8](https://github.com/user-attachments/assets/0aedfccf-6351-4be0-8ab5-e156b186319d)

## **9. En esta sección se trabaja un sistema básico de Waypoints. Se debe crear un circuito en una escena con la colección de puntos que conforman el circuito. Cada punto del circuito será un objeto 3D al que se le asigne la etiqueta “waypoint”. También se agregará un objeto personaje que será el que recorra los objetivos. Este objeto debe implementar el script con la mecánica de recorrido del circuito. Para ello, debe recuperar la referencia a cada uno de los objetivo y realizar los desplazamientos de un objetivo a otro aplicando el trabajo anterior. En la lógica se debe incluir la gestión de obtener quién es el siguiente objetivo.**

Antes de comenzar con la lógica, creamos un GameObject vacío que contendrá los waypoints, que serán representados por esferas blancas.

![image](https://github.com/user-attachments/assets/42703ec0-e09d-4780-9576-2ce7f812b6b3)

Ahora, creamos el script que tendrá la lógica para recorrer el circuito y declararemos la variable privada `Transform[] waypoints`, que contendrá todos los waypoints a recorrer. Emplearemos el atributo `[SerializeField]` para modificar la variable desde el inspector sin importar que sea privada.

Además, declararemos otras dos variables (entre otras):
* `int currentWaypointIndex`, que nos ayudará a la hora de cambiar de waypoint.
* `Transform currentWaypoint`, que nos indicará el waypoint actual. 

```c#
[SerializeField] private float movementSpeed = 1.0f;
[SerializeField] private float rotationSpeed = 1.0f;
[SerializeField] private float accuracy = .01f;

[SerializeField] private Material defaultWaypointMaterial;
[SerializeField] private Material currentWaypointMaterial;
[SerializeField] private Transform[] waypoints;

private int currentWaypointIndex;
private Transform currentWaypoint;

private Vector3 direction;
private Quaternion goalRotation;
``` 

En el método `Start()`, inicializaremos `currentWaypoint` de forma que apunte al primer waypoint del array.

```c#
    private void Start()
    {
        currentWaypoint = waypoints[currentWaypointIndex];
        waypoints[currentWaypointIndex].gameObject.GetComponent<Renderer>().material = currentWaypointMaterial;
        Debug.Log($"Current waypoint: {currentWaypoint.name}");
    }
````

Y ahora, en el `Update()`, lo primero que haremos será comprobar si la distancia que hay desde nuestra posición actual hasta el waypoint actual es menor a una cierta cantidad, que vendrá dada por la variable `accuracy`.

```c#

void Update() {
    if (Vector3.Distance(transform.position, currentWaypoint.position) < accuracy)
        ChangeCurrentWaypoint();
}
```

En caso de que la distancia sea menor a la indicada por `accuracy`, significará que será hora de cambiar al siguiente waypoint. Para ello, se ha definido la función `ChangeWaypoint()`:

```c#
private void ChangeCurrentWaypoint()
{
    currentWaypoint.GetComponent<Renderer>().material = defaultWaypointMaterial;
    currentWaypointIndex++;

    if (currentWaypointIndex >= waypoints.Length)
        return;
        
    currentWaypoint = waypoints[currentWaypointIndex];
    Debug.Log($"Current waypoint: {currentWaypoint.name}");
    currentWaypoint.GetComponent<Renderer>().material = currentWaypointMaterial;

}
```

Luego, moveremos y rotaremos el cubo en la dirección del waypoint actual.

_Script completo:_

```c#
using UnityEngine;

public class Script9 : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float accuracy = .01f;

    [SerializeField] private Material defaultWaypointMaterial;
    [SerializeField] private Material currentWaypointMaterial;
    [SerializeField] private Transform[] waypoints;

    private int currentWaypointIndex;
    private Transform currentWaypoint;
    
    private Vector3 direction;
    private Quaternion goalRotation;

    private void Start()
    {
        currentWaypoint = waypoints[currentWaypointIndex];
        waypoints[currentWaypointIndex].gameObject.GetComponent<Renderer>().material = currentWaypointMaterial;
        Debug.Log($"Current waypoint: {currentWaypoint.name}");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, currentWaypoint.position) < accuracy)
            ChangeCurrentWaypoint();
     
        if (currentWaypointIndex >= waypoints.Length)
            return;

        direction = currentWaypoint.position - transform.position;
        goalRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, rotationSpeed * Time.deltaTime);
        
        transform.Translate(0, 0,movementSpeed * Time.deltaTime);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }

    private void ChangeCurrentWaypoint()
    {
        currentWaypoint.GetComponent<Renderer>().material = defaultWaypointMaterial;
        currentWaypointIndex++;
        
        if (currentWaypointIndex >= waypoints.Length)
            return;
        
        currentWaypoint = waypoints[currentWaypointIndex];
        Debug.Log($"Current waypoint: {currentWaypoint.name}");
        currentWaypoint.GetComponent<Renderer>().material = currentWaypointMaterial;
    }
}
```

![Ejercicio 9](https://github.com/user-attachments/assets/1235c89f-5804-491f-b8f6-3ad8fbef6d89)

## **10. En esta sección se trabaja con el sistema de Waypoints de Unity. Para ello debes importar como asset en el proyecto la carpeta Utility. Configura el circuito, agrega el objetivo que debe perseguir el personaje y añade al personaje que recorrerá el circuito el script WaypointProgressTracker. Finalmente agrega un script al personaje que lo haga perseguir al objetivo. El sistema moverá el objetivo alejándolo del personaje moviéndose de un punto a otro del circuito. El personaje intenta perseguir al objetivo con nuestro script, por tanto, está “obligando” al objetivo a ir de un punto a otro a la par que lo persigue.**

Añadimos el script `WaypointCircuit` al mismo circuito que se creó en el anterior apartado. Luego, con la pestaña _Game_ visualizándose, pulsamos en el botón _"Assign using all child objects"_, lo que hará que todos los GameObjects hijos del circuito sean los waypoints.

![image](https://github.com/user-attachments/assets/2d7e36e6-cbc6-4f5c-8e57-0cb65df06a60)

Ahora, añadimos el script `WaypointProgressTracker` al personaje que recorrerá el circucito. A continuación, desde el inspector asignamos la variable `Circuit` y `Target` del script. `Target` será otro GameObject en la escena que marcará el camino a seguir para nuestro personaje.

![image](https://github.com/user-attachments/assets/7ace9e51-65d1-46fb-ab31-30d7b36f6108)

Finalmente, añadimos un script al personaje que contenga la lógica de traslación para perseguir al objetivo.

```c#
using System;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Script10 : MonoBehaviour
{
    public float movementspeed = 1.0f;
    public float rotationSpeed = 1.0f;
    public float accuracy = .01f;

    private Transform goal;
    private Vector3 direction;
    private Quaternion goalRotation;

    private void Start()
    {
        goal = GetComponent<WaypointProgressTracker>().target;
    }

    void Update()
    {
        direction = goal.position - transform.position;
        goalRotation = Quaternion.LookRotation(direction);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, rotationSpeed * Time.deltaTime);
            
        if (!(direction.magnitude > accuracy)) 
            return;
        
        transform.Translate(direction.normalized * (movementspeed * Time.deltaTime), Space.World);
        
        Debug.DrawRay(transform.position, direction, Color.red);
    }
}
```

![Ejercicio 10](https://github.com/user-attachments/assets/debaa6e5-3b4a-48e6-8dc0-85b2e390ec2e)
