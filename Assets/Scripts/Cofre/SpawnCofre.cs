using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCofre : MonoBehaviour {

    
    public GameObject cofre; // Objeto a spawnear (el cofre)
    public Transform spawnPoint; // Zona de spawn del cofre  

    void OnTriggerStay2D (Collider2D other)
    {
        // En caso de que el jugador muera en una zona de spawn:
        if (other.tag == "Player" && GameManager.instance.muerto)
        {
            Debug.Log("Me han matao");
            GameManager.instance.muerto = false;

            if (GameManager.instance.embriaguez >= 3 && !GameManager.instance.hayCofre)
            {
                GameObject nuevo =  Instantiate(cofre); // Situaremos el cofre de la armadura...
                nuevo.transform.position = spawnPoint.position; // ...en la posicón específica de spawn de ese 'Trigger' 
                GameManager.instance.hayCofre = true;
                Debug.Log("Nuevo cofre!");
            }
            Debug.Log("Respawn!");
            GameManager.instance.Respawn();   
        }
    }

}
