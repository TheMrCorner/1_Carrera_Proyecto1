using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public int vidaInicial;
    public int currentHealth;
	public GameObject barraVida;

	private bool attacking;
	private Animator anim;
	private SpriteRenderer currentColor;
    private EnemyMovement enemyMov;
	private Color original;
	private float barraVidaInicialX;

    public bool drop; // Indicará si el enemigo dropea o no un objeto
    public GameObject dropeo; // Posible objeto a dropear por los enemigos

    //Sonido
    public AudioClip GolpeClip;
    public AudioClip MuerteClip;
    


	// Use this for initialization
	void Awake () {
		currentColor = GetComponent<SpriteRenderer> ();
		original = currentColor.color;
		anim = GetComponent<Animator>();
        enemyMov = GetComponent<EnemyMovement>();
		attacking = false;
		barraVidaInicialX = barraVida.transform.localScale.x;
		currentHealth = vidaInicial;
	}

    public void Damage (int damage)
    {
        if (currentHealth > 0) { 
            if (GameManager.instance.borracho)
                damage = damage * 2;

            currentHealth -= damage;
            //Reduce la escala de la barra de vida del enemigo.
            barraVida.transform.localScale = new Vector3 (barraVida.transform.localScale.x - ((barraVidaInicialX / 5) * damage),
				barraVida.transform.localScale.y, barraVida.transform.localScale.z);

            Debug.Log("Vida actual del enemigo" + currentHealth);
		    currentColor.color = Color.red;
		    Invoke ("OriginalColor", 0.4f);

            //Sonido del golpe en el enemigo
            AudioManager.Instance.Playsound(GolpeClip, gameObject);
        }
		if (currentHealth <= 0)
        {	// Si el drop está activado, hacemos aparecer dicho objeto en la posición del enemigo
			if (drop && dropeo!= null) 
			{
				Instantiate(dropeo); 
				dropeo.transform.position = gameObject.transform.position; 
			}

			AudioManager.Instance.Playsound(MuerteClip, gameObject);

            Invoke("Respawn", 0.01f);
            this.gameObject.SetActive(false);
        }
	}

	private void OriginalColor() {
		currentColor.color = original;
	}

    public void Respawn()
    {
        currentHealth = vidaInicial;
        barraVida.transform.localScale = new Vector3(barraVidaInicialX, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
        if (enemyMov != null) { 
            enemyMov.playerDetected = false;
            enemyMov.facingLeft = true;
        }
    }
}
