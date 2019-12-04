using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo : MonoBehaviour {
	public int damage = 5;
	public float tiempoDesaparicion = 0;

	void OnTriggerEnter2D(Collider2D other)	{
		if (other.gameObject.CompareTag ("Player")) {
			GameManager.instance.dentroRayo = true; 
			GameManager.instance.DamageRayo(damage);
		}
	}
	void OnTriggerExit2D(Collider2D other)	{	
		if (other.gameObject.CompareTag ("Player"))	
		GameManager.instance.dentroRayo = false;
	}

	void Start(){
		Invoke ("Destruir", tiempoDesaparicion);
	}

	void Destruir(){
		GameManager.instance.dentroRayo = false;
		Destroy (gameObject);
	}
}
