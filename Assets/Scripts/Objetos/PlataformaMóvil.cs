using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMóvil : MonoBehaviour {
	public float speed = 0;
	private float origSpeed = 0;
	private Rigidbody2D bodyEnem;
	private Transform myTrans;
	bool mueve = true;

	// Use this for initialization
	void Start () {
		myTrans = this.transform;
		bodyEnem = this.GetComponent<Rigidbody2D>();
		origSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (mueve == true) {
			speed = origSpeed;
		} 
		else if (mueve == false) {
			speed = 0;
		}

		Movement ();
	}

	//Movimiento
	void Movement()
	{
		Vector2 myVel = bodyEnem.velocity;
		myVel.x = -myTrans.right.x * speed;
		bodyEnem.velocity = myVel;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "LimitePlat") {
			mueve = false;
			Invoke ("Flip", 2);
		}
	}

	//Cambia la dirección
	void Flip()
	{
		origSpeed *= -1;
		mueve = true;
	}
}
