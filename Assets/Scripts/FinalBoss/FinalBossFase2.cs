using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinalBossFase2 : MonoBehaviour
{
    public int vidaBoss;
    public Transform jugador; //Para tener su posición en todo momento
    public GameObject barraVida;
    public GameObject cerveza;
    private SpriteRenderer currentColor;
    private Color original;
    private Animator anim;
    private float barraVidaInicialX;
    [HideInInspector]
    public bool muerto; // Cuando el boss llegue a 0 de vida se pondrá a 'true'
	bool invulnerable = false;
    //Colocar al boss
    Transform actual;
    public Transform[] posiciones;
    int current;

    [HideInInspector]
    public bool player = false; //Esta variable controla que el jugador no esté en la plataformna central
                                //ATAQUES
    AtaqueRayo ataqueRay;
    AtaqueRayoJugador ataqueRay2;
    AtaqueOnda ataqueOnda;

    //Zonas
    [HideInInspector]
    public bool zona1 = false;

    void Awake()
    {
        ataqueRay = GetComponent<AtaqueRayo>();
        ataqueRay2 = GetComponent<AtaqueRayoJugador>();
        ataqueOnda = GetComponent<AtaqueOnda>();

        anim = GetComponent<Animator>();
        currentColor = GetComponent<SpriteRenderer>();
        original = currentColor.color;
        //barraVidaInicialX = barraVida.transform.localScale.x;
    }
    void Start()
    {       
        muerto = false;
    }
		
    public int numAtaques; //el random será de 0 al numAtaques (max)

    void Ataque()
    {
        if (!muerto && isActiveAndEnabled)
        {
            Debug.Log("ATAQUE");
            int ataque = Random.Range(0, (numAtaques));
            if (ataque == 0)
            {//ATAQUE RAYO
                anim.SetBool("Charge", true);
                ataqueRay.Invoke("Accion", 1.5f);

            }
            else if (ataque == 1)
            {//ATAQUE RAYO JUGADOR
                anim.SetBool("Charge", true);
                ataqueRay2.Invoke("Accion", 1.5f);           

            }
            else if (ataque == 2)
            { //ONDA
                ataqueOnda.Accion();
           
            }

           Invoke("SiguientePosicion", 2.5f);
        }
    }

    void SiguientePosicion()
    {
        if (!muerto && isActiveAndEnabled)
        {
            Debug.Log("SIGUENTEPOSICION");
            if (player) 
                current = 0; //La plataforma de cambio será siempre la inicial
            else 
                current = Random.Range(0, (posiciones.Length) - 1);
        }

        //Cualquier otra plataforma
        transform.position = posiciones[current].position;      
        Invoke("Ataque", 2);
    }

	void CambiaVulnerabilidad(){
		invulnerable = false;
	}

    public void Damage(int damage)
    {
        if (vidaBoss > 0.001 && !invulnerable){
            if (GameManager.instance.borracho)
                damage = damage * 2;

            vidaBoss -= damage;
			invulnerable = true;
			Invoke ("CambiaVulnerabilidad", 0.8f); //Tiempo de invulnerabilidad
            //Reduce la escala de la barra de vida del enemigo.
            barraVida.transform.localScale = new Vector3(barraVida.transform.localScale.x - ((barraVidaInicialX / 20) * damage),
                barraVida.transform.localScale.y, barraVida.transform.localScale.z);

            Debug.Log("Vida actual del enemigo" + vidaBoss);
            currentColor.color = Color.red;
            Invoke("OriginalColor", 0.4f);

            //Droppeo de objetos
            int aleatorio = Random.Range(0, 100);
            if (aleatorio % 7 == 0)
            {
                Instantiate(cerveza);
                cerveza.transform.position = transform.position;
            }
        }

        if (vidaBoss <= 0)
        {
            barraVida.transform.localScale = new Vector3(0, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
            muerto = true;
            Debug.Log("He muerto!");
            anim.SetBool("Dead", true); 
			Invoke ("irACreditos", 4);
        }
    }
	void irACreditos(){
		Application.LoadLevel ("Creditos");
	}
    private void OriginalColor()
    {
        currentColor.color = original;
    }

    public void StopAttackAnimation()
    {
        anim.SetBool("Attack", false);
    }

    public void KillBoss()
    {
        this.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        vidaBoss = 20;
        barraVidaInicialX = 4;
        barraVida.transform.localScale = new Vector3(barraVidaInicialX, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
        this.transform.parent.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        barraVidaInicialX = 4;
        SiguientePosicion();
    }
}
