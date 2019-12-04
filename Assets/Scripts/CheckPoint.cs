using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
	[HideInInspector] public bool cogido = false;

	Light light;
	public Sprite faroApagado;
	public Sprite faroEncendido;
	private SpriteRenderer spriteRenderer;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = faroApagado;
		light = GetComponent<Light> ();
		light.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D info){

		if (info.gameObject.CompareTag ("Player") && !cogido) {			
			GameManager.instance.AumentarCheck();
			cogido = true;
		}
		//Encenter luces
		if(cogido){
			light.enabled = true;
			spriteRenderer.sprite = faroEncendido;
		}
	}

	//Para cuando lo pierdas, apagarlo y dar la posibilidad de cogerlo de nuevo.
	public void ReiniciarCheck(){
		cogido = false;
		spriteRenderer.sprite = faroApagado;
		light.enabled = false;
	}
}
