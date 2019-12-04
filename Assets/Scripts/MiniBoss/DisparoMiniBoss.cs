using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoMiniBoss : MonoBehaviour {

	public float fireRate = 0.0f;
	public LayerMask toHit; //Máscara sobre la que se va a aplicar el daño
	public float range = 0;
	public Transform flecha;
	public float effectSpawnRate = 10;

    private Animator anim;
    float timeToSpawnEffect = 0;
	float timeToFire = 0.0f;
	Transform firePoint;

	//Sonido
	public AudioClip disparo;

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

	// Update is called once per frame
	void Update()
	{
		if (Time.time > timeToFire)
			{
				timeToFire = Time.time + 1 / fireRate;
				for (int i = 0; i < 8; i++) {
					transform.eulerAngles += new Vector3 (0,0,40);
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

		Effect();
		timeToSpawnEffect = Time.time + 1/effectSpawnRate;		

		//Colisión del RayCast
		if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
		{	
			Debug.DrawLine(firePointPosition, hit.point, Color.red);
			//GameManager.instance.Damage (damage);
		}
	}

	void Effect()
	{
        anim.SetBool("Attack", true);
		Instantiate(flecha, firePoint.position, objetivo.rotation);

		AudioManager.Instance.Playsound (disparo, gameObject);
	}
}

