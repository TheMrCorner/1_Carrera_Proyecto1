using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaCae : MonoBehaviour {
	Rigidbody2D plat;
	Transform inicial;
	public int tiempo = 2;
	float x;
	float y;

	// Use this for initialization
	void Awake (){
		x = transform.position.x;
		y = transform.position.y;
	}

	void Start () {
		inicial = this.transform;
		plat = this.GetComponent<Rigidbody2D> ();
		plat.bodyType = RigidbodyType2D.Static;
	}

	public void Caera (){
		Invoke ("Cae", 0.5f);
	}

	void Cae (){
		plat.bodyType = RigidbodyType2D.Dynamic;
		plat.gravityScale = 0.5f;
		Invoke ("ReseteaPlataforma", tiempo);
	}

	void ReseteaPlataforma(){
		transform.position = new Vector2 (x, y);
		plat.bodyType = RigidbodyType2D.Static;
	}
}
