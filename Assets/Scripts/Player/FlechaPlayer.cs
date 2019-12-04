using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaPlayer : MonoBehaviour
{
	public float damage = 1;

    public GameObject objetoADestruir;

    // Para que las flechas desaparezcan se le debe poner uno de los tags de abajo al objeto con el que van a chocar.
    //Hacer 2 Scripts uno para player y otro para enemigo, hacer 2 prefabs, uno que sea una copia del line trail pero cada uno con un script de destruir distinto.
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.CompareTag ("Enemigo")) {
			other.gameObject.SendMessageUpwards ("Damage", damage, SendMessageOptions.DontRequireReceiver);
			Destroy (objetoADestruir);
		}
		else if (other.gameObject.CompareTag ("Colision")) 
			Destroy(objetoADestruir);
	}
}
