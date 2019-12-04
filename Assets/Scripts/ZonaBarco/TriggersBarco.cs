using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersBarco : MonoBehaviour {

    public bool finalRio;

    public float colliderBarco;

    public GameObject barca;
   // public GameObject enemigoBarca;

    public Transform posicionSpawn;


    void OnTriggerEnter2D(Collider2D other)
    {
        MovimientoBarca movBarca = other.GetComponent<MovimientoBarca>();

        if (movBarca != null)
        {
            if (finalRio)
            {
                movBarca.muerto = false;
                movBarca.fin = true;
            }
            else
            {
                GameObject nuevo = Instantiate(barca);
                nuevo.transform.position = posicionSpawn.position;
                nuevo.GetComponent<BoxCollider2D>().size = new Vector2(colliderBarco, 5);
            }
        }
    }
}
