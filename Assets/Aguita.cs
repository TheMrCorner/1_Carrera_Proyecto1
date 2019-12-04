using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aguita : MonoBehaviour {

	public int damage;

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

}
