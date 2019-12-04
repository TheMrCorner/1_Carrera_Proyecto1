using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour {	

	void OnTriggerExit2D(Collider2D col)
	{
		//Debug.Log ("Sale");
		this.GetComponent<Collider2D>().isTrigger = false;
		//Invoke ("cambiaTrigger", 1f); //Plataforma temporal
	}

	void cambiaTrigger(){ //PLATAFORMA TEMPORAL
		this.GetComponent<Collider2D>().isTrigger = true;
	}
}
