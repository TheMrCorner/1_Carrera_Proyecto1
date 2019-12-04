using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumoVeneno : MonoBehaviour {
	public int damage = 5;
	public int tiempoDesaparicion = 0;

	void OnTriggerEnter2D(Collider2D other)	{
		if (other.gameObject.CompareTag ("Player")) {
			GameManager.instance.dentroHumo = true;
			GameManager.instance.DamageHumo(damage);
		}
	}
	void OnTriggerExit2D(Collider2D other)	{		
		if (other.gameObject.CompareTag ("Player"))
			GameManager.instance.dentroHumo = false;
	}

	void Start(){
		Invoke ("Destruir", tiempoDesaparicion);
	}

	void Destruir(){
		GameManager.instance.dentroHumo = false;
		Destroy (gameObject);
	}
}
