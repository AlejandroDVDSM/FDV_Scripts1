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
