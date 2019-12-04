using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruible : MonoBehaviour {

	public int tiempoDesaparicion;

	public int alcohol;
	public int vida;
	private Collider2D collid;

	void Start () {
		collid = GetComponent<Collider2D> ();
		Invoke ("Destruir", tiempoDesaparicion);
	}

	void OnTriggerEnter2D(Collider2D info){
		if (info.gameObject.CompareTag ("Player")) {
			GameManager.instance.AumentarVida (vida);
			GameManager.instance.AumentarEmbriaguez (alcohol);
			GameManager.instance.UpdateGUI ();
			gameObject.SetActive (false);
		} 
	}

	void Destruir () {
		Destroy (gameObject);
	}
}
