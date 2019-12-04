using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public Collider2D Puerta;
    public BossIA boss;
    public Renderer renderer;
    Color c;                   // Variables para aplicar una transparencia una vez abierta la puerta
    public float transparencia;


    void Start()
    {
        c = renderer.material.color; // Inicializamos 'c' con el color del material del renderer
    }
    void OnTriggerEnter2D(Collider2D other) // Entramos en el collider exterior de la puerta:
    {
        if (boss.muerto && other.gameObject.tag == "Player") // Si hemos derrotado al boss...
        {
            Debug.Log("lo maté!");
            Puerta.isTrigger = true; // ...el trigger del collider (interior) de la puerta  se activa y podemos pasar (la puerta "se abre") 
            GetComponent<BoxCollider2D>().enabled = false; //... el collider exterior se desactiva, y por tanto queda abierta permanentemente 
            c.a = transparencia; // Aplicamos la transparencia a la puerta
            renderer.material.color = c;
        }
    }
}