using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueOnda : MonoBehaviour {

    public Transform zonaTeleport;
    public GameObject Efecto;
    public Transform zonaOnda1, zonaOnda2, zonaOnda3, zonaOnda4;

    private Animator anim;

    //Sonido
    public AudioClip explosion;
    void Start () {
        anim = GetComponent<Animator>();       
	}

	public void Accion(){
		anim.SetBool("Hit", true);
		this.transform.position = zonaTeleport.position; 
		Invoke("PrimeraOnda",1);

	}

	void PrimeraOnda()
    {
        GameObject onda1 = GameObject.Instantiate(Efecto);
        GameObject onda2 = GameObject.Instantiate(Efecto);

        onda1.transform.position = zonaOnda1.position;
        onda2.transform.position = zonaOnda2.position;

        //sonido
        AudioManager.Instance.Playsound(explosion, onda1);
        AudioManager.Instance.Playsound(explosion, onda2);

        Invoke("SegundaOnda", 1.0f);
    }

    void SegundaOnda()
    {
        GameObject onda3 = GameObject.Instantiate(Efecto);
        GameObject onda4 = GameObject.Instantiate(Efecto);

        onda3.transform.position = zonaOnda3.position;
        onda4.transform.position = zonaOnda4.position;

        AudioManager.Instance.Playsound(explosion, onda3);
        AudioManager.Instance.Playsound(explosion, onda4);

        anim.SetBool("Hit", false);
    }
}
