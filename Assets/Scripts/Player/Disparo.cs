using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour {

	public float fireRate = 0.0f;
	public LayerMask toHit; //Máscara sobre la que se va a aplicar el daño
	public float range = 0;
	public Transform flecha;
	public float effectSpawnRate = 10;

	float timeToSpawnEffect = 0;
	float timeToFire = 0.0f;
	bool shootActivated = true;

	Transform firePoint;

	// Use this for initialization
	void Awake()
	{
		firePoint = transform.FindChild("FirePoint");
		if (firePoint == null)
		{
			Debug.Log("No fire point");
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.instance.flechas > 0) {
			//Arma de disparo único (arco)
			if (fireRate == 0) {
				if (Input.GetButtonUp ("Fire2")) {
					shootActivated = true;
					Shoot ();
				}
			}
		//Arma de varios disparos (posibles mejoras o idas de olla)
		else {
				if (Input.GetButton ("Fire2") && Time.time > timeToFire) {
					timeToFire = Time.time + 1 / fireRate;
					Shoot ();
				}
			}
		}
	}

	void Shoot()
	{
		//Posición de la camara en el mundo
		Vector2 mousePosition = 
			new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
				Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		//Posición del punto de disparo
		Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
		//origen del disparo, dirección, rango y layer 
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, range, toHit);

		if (Time.time >= timeToSpawnEffect)
		{
			Effect();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}

		//Colisión del RayCast
		if (hit.collider != null && shootActivated)
		{
			shootActivated = false;
			Debug.DrawLine(firePointPosition, hit.point, Color.red);
			//Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage.");
			//hit.collider.gameObject.SendMessageUpwards("Damage", damage, SendMessageOptions.DontRequireReceiver);
		}
		GameManager.instance.Disparo();
	}

	void Effect()
	{
		Instantiate(flecha, firePoint.position, firePoint.rotation);
	}
}
