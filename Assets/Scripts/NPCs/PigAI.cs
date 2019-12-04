using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAI : MonoBehaviour {

    public float velocidadMovimiento;
	Vector3 pos;

    //Sonido
    public AudioClip turnOnClip;

    void Update () {
		pos.x = velocidadMovimiento;
		transform.Translate (pos*Time.deltaTime);
	}

    public void Damage(int damage)
    {
        //Sonido del golpe en el enemigo
        AudioManager.Instance.Playsound(turnOnClip, gameObject);
        Destroy(this.gameObject);
    }
}
