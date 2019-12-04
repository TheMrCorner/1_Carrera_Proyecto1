using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaCollider : MonoBehaviour {

	private Collider2D colli;

	void OnTriggerEnter2D(Collider2D info){
		
		if (info.gameObject.CompareTag ("CambiaCollider")) {
			colli = info.gameObject.GetComponent<Collider2D> ();
			colli.isTrigger = false;
		}
	}
}
