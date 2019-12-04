using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerCofre : MonoBehaviour
{
    public GameObject cofreAbierto;
    GameObject cofrenuevo;// Objeto a spawnear (el cofre abierto)

    private Animator anim;

	void Awake () {
		anim = GetComponent<Animator> (); 
	}
		
    void OnTriggerEnter2D(Collider2D other)
    {
        // En caso de que el jugador "abra" el cofre, es decir, entre en contacto con él:
        if (other.tag == "Player")
        {
            Destroy(gameObject); // 1) Destruye el cofre cerrado
            cofrenuevo = Instantiate(cofreAbierto); // 2) Coloca el cofre abierto... 
            cofrenuevo.transform.position = gameObject.transform.position; //... en la misma posición del cofre cerrado
            Destroy(cofrenuevo, 15); // *Después de un tiempo, se destruye*
            GameManager.instance.hayCofre = false;
		}
    }
   
}
