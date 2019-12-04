using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinalBoss : MonoBehaviour
{
	public int vidaBoss;
	public Transform jugador; //Para tener su posición en todo momento
	public GameObject barraVida;
    public GameObject barreraFase1;
	public GameObject cerveza;
	private SpriteRenderer currentColor;
	private Color original;
	private Animator anim;
	private float barraVidaInicialX;
	[HideInInspector] public bool transporte;
	[HideInInspector] public bool muerto; // Cuando el boss llegue a 0 de vida se pondrá a 'true'
	//Colocar al boss
	Transform actual;
	public Transform[] posiciones;
	int current;

	[HideInInspector] public bool player = false; //Esta variable controla que el jugador no esté en la plataformna central
	//ATAQUES
	AtaqueRayo ataqueRay;
	AtaqueRayoJugador ataqueRay2;
	AtaqueOnda ataqueOnda;

	//Zonas
	[HideInInspector] public bool zona1 = false;

	void Awake()
	{
		ataqueRay = GetComponent<AtaqueRayo> ();
		ataqueRay2 = GetComponent<AtaqueRayoJugador> ();
		ataqueOnda = GetComponent<AtaqueOnda> ();

		transporte = true;
		anim = GetComponent<Animator>();
		currentColor = GetComponent<SpriteRenderer>();
		original = currentColor.color;
		//barraVidaInicialX = barraVida.transform.localScale.x;
	}
	void Start()
	{
		SiguientePosicion ();
		muerto = false;
	}

    public float minTiempo, maxTiempo, varianza;
	public int numAtaques; //el random será de 0 al numAtaques (max)

	void Ataque(){
		if (!transporte && isActiveAndEnabled) {
			Debug.Log ("ATAQUE");
			int ataque = Random.Range (0, (numAtaques));
			if (ataque == 0) {//ATAQUE RAYO
				anim.SetBool ("Charge", true);
				ataqueRay.Invoke ("Accion", 1.5f);
				Invoke ("ActivaTransporte", 1.5f);

            } else if (ataque == 1) {//ATAQUE RAYO JUGADOR
				anim.SetBool ("Charge", true);
				ataqueRay2.Invoke ("Accion", 1.5f);
				Invoke ("ActivaTransporte", 1.5f);
            }

			float tiempo = Random.Range (minTiempo, maxTiempo);
			Invoke ("SiguientePosicion", 3.0f);
		}
	}
		
	void ActivaTransporte(){
		transporte = true;
	}

	void SiguientePosicion()
	{
		if (transporte && isActiveAndEnabled) {
			Debug.Log ("SIGUENTEPOSICION");
				//Primera fase
				if (!player)
					current = Random.Range(0, (posiciones.Length)-1);
				else
					current = Random.Range(1, (posiciones.Length));
			}

			//Cualquier otra plataforma
			transform.position = posiciones [current].position;
			transporte = false;
			Invoke ("Ataque", 1.5f);
	}

	public void Damage(int damage)
	{	
		if (vidaBoss > 0.001)
		{
			if (GameManager.instance.borracho)
				damage = damage * 2;
			
			vidaBoss -= damage;
			//Reduce la escala de la barra de vida del enemigo.
			barraVida.transform.localScale = new Vector3 (barraVida.transform.localScale.x - ((barraVidaInicialX / 10) * damage),
				barraVida.transform.localScale.y, barraVida.transform.localScale.z);

			Debug.Log("Vida actual del enemigo" + vidaBoss);
			currentColor.color = Color.red;
			Invoke("OriginalColor", 0.4f);

			//Droppeo de objetos
			int aleatorio = Random.Range(0, 100);
			if (aleatorio % 5 == 0) {
				Instantiate (cerveza);
				cerveza.transform.position = transform.position;
			}
		}
        // Cuando la vida sea 0 se desactivará el collider y el jefe aparecerá en la zona final.
		if (vidaBoss <= 0)
		{
            Debug.Log("DESACTIVABARRERA");
            barreraFase1.GetComponent<Collider2D>().enabled = false;
            barreraFase1.GetComponent<SpriteRenderer>().enabled = true;
			barraVida.transform.localScale = new Vector3 (0, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
            muerto = true;
            Destroy(this.gameObject);
        }
	}

	private void OriginalColor()
	{
		currentColor.color = original;
	}

	public void StopAttackAnimation()
	{
		anim.SetBool("Attack", false);
	}

    public void Respawn ()
    {
        vidaBoss = 10;
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
