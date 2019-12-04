using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalera : MonoBehaviour {

	public float velocidadSubida;
	PlayerMovement script;
	Rigidbody2D jugador;


	void OnTriggerEnter2D(Collider2D info){
		if (info.gameObject.CompareTag("Player")) {				
			script = info.gameObject.GetComponent<PlayerMovement> ();
			jugador = info.gameObject.GetComponent<Rigidbody2D> ();
			jugador.velocity = new Vector2(0, 0);
			jugador.gravityScale = 0;
			script.velocidadEscalera = velocidadSubida;
			script.escalera = true;
			info.gameObject.GetComponent<Animator> ().SetBool ("Stair", true);
		}
	}
	void OnTriggerExit2D(Collider2D info){
        if (info.gameObject.CompareTag("Player")) {
            script.escalera = false;
		    jugador.gravityScale = 3;
		    info.gameObject.GetComponent<Animator> ().SetBool ("Stair", false);
        }
    }
}
