using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyAI : MonoBehaviour
{
    public Collider2D attackTrigger;
    public int vidaInicial;
    public GameObject barraVida;

    private bool shield;
    private Animator anim;
    private SpriteRenderer currentColor;
    private Color original;
	private float barraVidaInicialX;
	private int currentHealth;
    public bool drop; // Indicará si el enemigo dropea o no un objeto
    public GameObject dropeo; // Posible objeto a dropear por los enemigos

    void Awake()
    {   
        currentColor = GetComponent<SpriteRenderer>();
        attackTrigger.enabled = false;
        original = currentColor.color;
        anim = GetComponent<Animator>();
        shield = true;
		currentHealth = vidaInicial;
		barraVidaInicialX = barraVida.transform.localScale.x;
}


	// Método al que se llama cuando el enemigo recibe daño.
    public void Damage(int damage)
    {
        if  (!shield)
        {
            if (currentHealth > 0)
            {
                if (GameManager.instance.borracho)
                    damage = damage * 2;

                currentHealth -= damage;
                //Reduce la escala de la barra de vida del enemigo.
                barraVida.transform.localScale = new Vector3 (barraVida.transform.localScale.x - ((barraVidaInicialX / 5) * damage),
				barraVida.transform.localScale.y, barraVida.transform.localScale.z);

                Debug.Log("Vida actual del enemigo" + currentHealth);
                currentColor.color = Color.red;
                Invoke("OriginalColor", 0.4f);

            }
			if (currentHealth <= 0)
            {
                //Mirar como desactivar los componentes de forma que solo se vea la imagen del guerrero muerto.
                if (drop)
                {
                    Instantiate(dropeo);
                    dropeo.transform.position = gameObject.transform.position;
                }
                Respawn();
                this.gameObject.SetActive(false);
            }
        }
        else if (GameManager.instance.borracho)
        {
            Debug.Log("Escudo roto");
            anim.SetBool("Shield", false);
			shield = false;
			currentColor.color = Color.red;
			Invoke("OriginalColor", 0.4f);
            attackTrigger.enabled = true;
        }
    }


	// Método que devuelve el sprite a su color original.
    private void OriginalColor()
    {
        currentColor.color = original;
    }
		
	// Método que se utiliza para desactivar la animación de ataque desde Animation.
	public void DesactivateAttackAnimation() {
		anim.SetBool("Attack", false);
	}

    public void Respawn()
    {
        shield = true;
        attackTrigger.enabled = false;
        anim.SetBool("Shield", true);
        currentHealth = vidaInicial;
        barraVida.transform.localScale = new Vector3(barraVidaInicialX, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
    }
}
