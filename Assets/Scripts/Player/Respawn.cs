using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public bool quitarCollider;
	void OnTriggerEnter2D(Collider2D info){

		if (info.gameObject.CompareTag("Player")) {
            GameManager.instance.vida = 0;

			if (quitarCollider)
				Destroy (gameObject);
		}

	}
}
