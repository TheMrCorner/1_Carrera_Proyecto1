using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D info){

		if (info.gameObject.CompareTag("Player")) {			
			GameManager.instance.Respawn ();
		}

	}
}
