using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuelveteSolido : MonoBehaviour {
	
	private Collider2D collid;

	void Start(){
		collid = GetComponent<Collider2D> ();
	}

	void OnTriggerEnter2D(Collider2D info){
		if (info.gameObject.CompareTag ("Colision")) {
			collid.isTrigger = false;
		}
	}
}
