using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botella : MonoBehaviour {

	public int alcohol;
	public int vida;

	//SOnido
	public AudioClip powerUp;

	void OnTriggerEnter2D(Collider2D info){
		if (info.gameObject.CompareTag ("Player")) {
			GameManager.instance.AumentarVida(vida);
			GameManager.instance.AumentarEmbriaguez(alcohol);
			GameManager.instance.UpdateGUI ();
			gameObject.SetActive(false);
			AudioManager.Instance.Playsound(powerUp, gameObject);
		}
	}
}
