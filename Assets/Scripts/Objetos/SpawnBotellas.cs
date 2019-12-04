﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBotellas : MonoBehaviour {

	public int tiempoReaparicion;
	Vector2 inicial;

	void Start(){
		inicial = transform.position;
	}

	void OnTriggerEnter2D(Collider2D info){
		if (info.gameObject.CompareTag ("Player")) {
			gameObject.SetActive(false);
			GameManager.instance.AumentarEmbriaguez (1);
			Invoke ("Reaparecer", tiempoReaparicion);
		}
	}

	void Reaparecer(){
		gameObject.SetActive(true);
		transform.position = inicial;
	}
}
