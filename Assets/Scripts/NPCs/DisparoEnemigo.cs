using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour {

	public float fireRate = 0.0f;
	public LayerMask toHit; //Máscara sobre la que se va a aplicar el daño
	public float range = 0;
	public Transform flecha;
	public float effectSpawnRate = 10;

    public int velFlecha;

	float timeToSpawnEffect = 0;
	float timeToFire = 0.0f;
	Transform firePoint;
	private Animator anim;

	bool disparo = true;
	bool shootActivated = true;
	public Transform objetivo;
	// Use this for initialization
	void Awake()
	{
		anim = GetComponentInParent<Animator>();
		firePoint = transform.FindChild("FirePoint");
		if (firePoint == null)
		{
			Debug.Log("No fire point");
		}

	}

	void ActivaDisparo(){
		disparo = true;
	}

	// Update is called once per frame
	void Update()
	{
		//Arma de disparo único (arco )
		if (fireRate == 0)
		{
			if (disparo)
			{
				shootActivated = true;
				Shoot();
			}
		}
		//Arma de varios disparos (posibles mejoras o idas de olla)
		else
		{
			if (disparo && Time.time > timeToFire)
			{
				shootActivated = true;
				timeToFire = Time.time + 1 / fireRate;
				Shoot();
			}
		}
	}

	void Shoot()
	{
		//Posición de la camara en el mundo
		Vector2 puntoFinal = 
			new Vector2 (objetivo.transform.position.x,	objetivo.transform.position.y);
		
		//Posición del punto de disparo
		Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
		//origen del disparo, dirección, rango y layer 
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, puntoFinal-firePointPosition, range, toHit);

		if (Time.time >= timeToSpawnEffect)
		{	if (anim != null)
				anim.SetBool ("Attack", true);
			Effect();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}

		//Colisión del RayCast
		if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
		{	
			shootActivated = false;
			Debug.DrawLine(firePointPosition, hit.point, Color.red);
			//GameManager.instance.Damage (damage);
		}
	}

	void Effect()
	{
        Transform nuevo = Instantiate(flecha, firePoint.position, objetivo.rotation);

        MovFlecha mov;
        mov = nuevo.GetComponent<MovFlecha>();
        mov.moveSpeed = velFlecha;
        mov.range = range;
	}
}

