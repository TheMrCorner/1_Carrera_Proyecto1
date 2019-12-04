using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruyeFlechaEnemigo : MonoBehaviour
{

    public GameObject objetoADestruir;
	public int damage = 10;
	public GameObject Humo;

    // Para que las flechas desaparezcan se le debe poner uno de los tags de abajo al objeto con el que van a chocar.
    //Hacer 2 Scripts uno para player y otro para enemigo, hacer 2 prefabs, uno que sea una copia del line trail pero cada uno con un script de destruir distinto.
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.CompareTag ("Player")) {
			GameManager.instance.Damage (damage);
			if (Humo != null) {
				Instantiate (Humo);
				Humo.transform.position = transform.position;
			}
			Destroy (objetoADestruir);
		} else if (other.gameObject.CompareTag ("Colision")) { 
			if (Humo != null) {
				Instantiate (Humo);
				Humo.transform.position = transform.position;
			}
			Destroy (objetoADestruir);
		}
	}
}
