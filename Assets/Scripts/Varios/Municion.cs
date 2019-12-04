using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : MonoBehaviour {

	public int numeroFlechas;
	public int tiempoReaparicion;
	Vector2 inicial;

	void Start(){
		inicial = transform.position;
	}

	void OnTriggerEnter2D(Collider2D info){
		if (info.gameObject.CompareTag ("Player")) {
			GameManager.instance.AumentarMunicion (numeroFlechas);		
			GameManager.instance.UpdateGUI ();
			gameObject.SetActive(false);
			Invoke ("Reaparecer", tiempoReaparicion);
		}
	}

	void Reaparecer(){
		gameObject.SetActive(true);
		transform.position = inicial;
	}
}
