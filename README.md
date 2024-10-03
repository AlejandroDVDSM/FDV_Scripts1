# FDV_Scripts1

## **1. Crear un script que mueva el objeto hacia un punto fijo que se marque como objetivo utilizando el método Translate de la clase Transform. El objetivo debe ser una variable pública, de esta forma conseguimos manipularla en el inspector y ver el efecto de distintos valores en las coordenadas. Utilizar this.transform.Translate(goal) en el start, solo se mueve una vez.**

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

### **a. Añadir this.transform.Translate(goal); al Update e ir multiplicando goal = goal * 0.5f; en el update para dar saltos más pequeños cada vez.**

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

### **b. Configurar la coordenada Y del Objetivo en 0.**

![image](https://github.com/user-attachments/assets/fc915bb3-c554-4321-a2d6-424b925b4eb8)

![3  Ejercicio1b](https://github.com/user-attachments/assets/ea5bd2ae-9068-4c0f-a45a-ad0c95b50ded)

### **c. Poner al Objetivo una coordenada Y distinta de cero.**

El resultado es el mismo que el mostrado en el apartado "1a", donde la coordenada Y ya era distinta de 0.

### **d. Modificar el script para que el objeto despegue del suelo y vuele como un avión.**

No hace falta modificar el script. Tan solo es necesario asignar un `goal` cuyo valor en `X` sea 0 y cuyos valores en `Y` y `Z` sean mayores de 0 para simular un movimiendo diagonal, tal y como haría un avión.

![image](https://github.com/user-attachments/assets/f0de45c3-00e2-4330-9a15-e9d140264b12)

![4  Ejercicio 1d](https://github.com/user-attachments/assets/e6620b2f-3664-42e7-a51a-6f4e92ab9b82)

### **e. Duplicar los valores de X, Y, Z del Objetivo. ¿Es consistente el movimiento?.**

El movimiento no es consistente dado que a que el cubo avanza a "trompicones", su movimiento no es _smooth_.

## **2. El Objetivo no es un objetivo propiamente dicho, sino una dirección en la que queremos movernos. La información relevante de un vector es la dirección. Los vectores normalizados, conservan la misma dirección pero su escala no afecta al movimiento. Se debe conseguir un movimiento consistente de forma que la escala no afecte a la traslación. Del mismo modo, se debe conseguir que el recorrido realizado por el personaje entre un frame y otro no tenga aberraciones espacio-temporales. Para ello se debe considerar la relación entre la velocidad, el espacio y el tiempo. Por otra parte, el tiempo que transcurre entre un frame y otro se obtiene con: Time.deltaTime.**

a. Sustituir la dirección del movimiento por su equivalente normalizada.

b. Con el vector normalizado, lo podemos multiplicar por un valor de velocidad para determinar cómo de rápido va el personaje

c. Usar `Time.deltaTime` para suavizar el movimiento

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

## **3. En lugar de seguir usando una dirección como objetivo, vamos a movernos ahora hacia una verdadera posición objetivo. Lo agregarermos como un campo público en la clase para poder configurarlo desde le Inspector. También agregaremos un campo para configurar la velocidad del personaje desde el propio Inspector. Aunque queramos desplazarnos hacia un punto en el espacio, el método Translate debe recibir la dirección del movimiento. La dirección que une dos puntos se obtiene restando el más lejano al más cercano. Por último, si el personaje no está encarando el objetivo (podría incluso estar de espaldas a él), el desplazamiento será suave pero la orientación de su malla no será consistente. Por esta razón será necesario rotarlo de forma que su eje z local (forward) apunte hacia el objetivo. La función LookAt del Transform nos ayudará con esto. En este caso, por tanto, para movernos hacia un punto en el espacio que configuramos a una velocidad dada:**

a. Hacemos el objetivo una variable pública `public Transform goal` y añadimos un `public float speed = 1.0f.`

b. Giramos al personaje para lograr que su movimiento sea hacia delante utilizando `this.transform.LookAt(goal.position)` en el Start para que gire primero y luego se mueva.

c. La dirección en la que nos tenemos que mover viene determinada por la diferencia entre la posición del objetivo y nuestra posición:
 `Vector3 direction = goal.position - this.transform.position;`
 `this.transform.Translate(direction.normalized * speed * Time.deltaTime)`
 
d. Si lo ejecutamos en este momento, como la orientación del personaje va a cambiar, el translate no va a funcionar correctamente porque los ejes del personaje y el mundo no están alineados. El movimiento se debe hacer de forma relativa al sistema de referencia del mundo.
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

a. Crear un script que haga que el personaje siga al cubo continuamente sin aplicar simulación física.

b. Agregar un campo público que permita graduar la velocidad del movimiento desde el inspector de objetos.

c. Utilizar la tecla de espaciado para incrementar la velocidad del desplazamiento en el tiempo de juego.

![Ejercicio 5](https://github.com/user-attachments/assets/20b1659f-49d1-4f22-98d8-b5cae3be347e)


