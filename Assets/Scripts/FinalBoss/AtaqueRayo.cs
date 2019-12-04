using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueRayo : MonoBehaviour {	
	public Transform PosRayo1;
	public Transform PosRayo2;
	public GameObject Rayo;
	public GameObject Rayo2;
	private Animator anim;

    //Sonido
    public AudioClip rayo;

	void Start () {
		anim = GetComponent<Animator>();
	}

	public void Accion(){ 
		Instantiate (Rayo);
		Rayo.transform.position = PosRayo1.position;
        AudioManager.Instance.Playsound(rayo, Rayo);
		Instantiate (Rayo2);
		Rayo.transform.position = PosRayo2.position;
        AudioManager.Instance.Playsound(rayo, Rayo2);
		anim.SetBool ("Charge", false);
	}
}
