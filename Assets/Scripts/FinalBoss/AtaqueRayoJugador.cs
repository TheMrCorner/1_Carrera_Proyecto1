using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueRayoJugador : MonoBehaviour {
	public GameObject Rayo;
	public GameObject jugador;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	public void Accion(){ 
		Instantiate (Rayo);
		Rayo.transform.position = jugador.transform.position;
		anim.SetBool ("Charge", false);
	}
}
