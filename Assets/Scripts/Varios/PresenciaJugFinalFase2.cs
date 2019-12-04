using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenciaJugFinalFase2 : MonoBehaviour {

	FinalBossFase2 boss;
	public GameObject jefazo;
	void Start()
	{
		boss = jefazo.GetComponent<FinalBossFase2>();  
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			boss.player = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			boss.player = false;
		}
	}
}
