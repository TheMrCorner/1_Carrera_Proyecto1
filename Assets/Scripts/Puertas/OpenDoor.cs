using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ESTE SCRPIT FUNCIONA DE LA SIGUIENTE MANERA:
  SI EL JUGADOR ENTRA EN EL TRIGGER DE LA PUERTA, Y ÉSTE TIENE AL MENOS UNA LLAVE,
  ENTONCES SE ABRIRÁ LA PUERTA.
  */
public class OpenDoor : MonoBehaviour
{
    public Renderer renderer; 
    Color c;                   // Variables para aplicar una transparencia una vez abierta la puerta
    public float transparencia;

    public Collider2D Puerta; // Collider de la puerta (el cual no se puede atraversar sin "abrirla" con una llave)

    void Start()
    {
        c = renderer.material.color; // Inicializamos 'c' con el color del material del renderer
    }

    void OnTriggerEnter2D(Collider2D other) // Entramos en el collider exterior de la puerta:
    {
        if (other.gameObject.tag == "Player" && GameManager.instance.llaves >= 1) // Si el jugador tiene al menos una llave... 
        {
            Puerta.isTrigger = true; // ...el trigger del collider (interior) de la puerta  se activa y podemos pasar (la puerta "se abre") 
            GetComponent<BoxCollider2D>().enabled = false; //... el collider exterior se desactiva, y por tanto queda abierta permanentemente (y no usamos más llaves)
            GameManager.instance.llaves--; // ...y restamos una llave al jugador

            c.a = transparencia; // Aplicamos la transparencia a la puerta
            renderer.material.color = c;
        }
    }
}
