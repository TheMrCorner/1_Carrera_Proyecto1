using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossIA : MonoBehaviour
{

    int vidaBoss;
    public GameObject barraVida;
    private SpriteRenderer currentColor;
    private Color original;
    private Animator anim;
	private float barraVidaInicialX;

	[HideInInspector] public bool muerto; // Cuando el boss llegue a 0 de vida se pondrá a 'true'

    //Controlar al boss
    Transform actual;
    public Transform posicion1;
    public Transform posicion2;
    public Transform posicion3;
    int posSig = 0;
    int posAct = 0;
	[HideInInspector] public bool player = false; //Esta variable controla que el jugador no esté en la plataformna central

	//Sonido
	public AudioClip Transporte;
	public AudioClip Golpe;
	public AudioClip Muerte;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentColor = GetComponent<SpriteRenderer>();
        original = currentColor.color;
		//barraVidaInicialX = barraVida.transform.localScale.x;
    }
    void Start()
    {
        Invoke("SiguientePosicion", 4);

        muerto = false;
        vidaBoss = 10;
        GameManager.instance.flechas = 0; //Opcional
        Debug.Log("Vida actual del Boss" + vidaBoss);

    }

    void Update()
    {
        if (GameManager.instance.vida <= 0)
            Respawn();
    }
   

    void CambiaPosicion(Transform posicionDestino)
    {
        if (posSig != posAct)
        {
            this.transform.position = posicionDestino.transform.position;

			AudioManager.Instance.Playsound (Transporte, gameObject);

            Debug.Log("Me muevo");
        }
        else
            Debug.Log("No me muevo");

        posAct = posSig;

        actual = posicionDestino;

        Invoke("SiguientePosicion", 4);
    }

    void PosicionRandom()
    {
		posSig = Random.Range(1, 4);
    }

    void SiguientePosicion()
    {

        if (player)
        {
            posSig = 3;
        }
        else
            PosicionRandom();

        Debug.Log("Siguiente Posicion, deprisa");

        if (posSig == 1)
        {
            CambiaPosicion(posicion1);
            Debug.Log("Me voy a mover pos 1");
        }
        else if (posSig == 2)
        {
            CambiaPosicion(posicion2);
            Debug.Log("Me voy a mover pos 2");
        }
        else if (posSig == 3)
        {
            CambiaPosicion(posicion3);
            Debug.Log("Me voy a mover pos 3");
        }
        else
            CambiaPosicion(actual);
        Debug.Log("No me muevo");
    }

    public void Damage(int damage)
    {	
		vidaBoss -= damage;
        if (vidaBoss > 0.001)
        {
			
			//Reduce la escala de la barra de vida del enemigo.
			barraVida.transform.localScale = new Vector3 (barraVida.transform.localScale.x - ((barraVidaInicialX / 10) * damage),
				barraVida.transform.localScale.y, barraVida.transform.localScale.z);

			Debug.Log("Vida actual del enemigo" + vidaBoss);
			currentColor.color = Color.red;
			Invoke("OriginalColor", 0.4f);

			AudioManager.Instance.Playsound (Golpe, gameObject);
        }

        else 
        {	
			barraVida.transform.localScale = new Vector3 (0, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
            muerto = true;
            Debug.Log("He muerto!");
			anim.SetBool("Dead", true);;

			AudioManager.Instance.Playsound (Muerte, gameObject);
			AudioManager.Instance.audioSourceBoss.SetActive (false);
			AudioManager.Instance.audioSource1.SetActive (false);
			AudioManager.Instance.CancelInvoke ();
			AudioManager.Instance.audioSource2.SetActive (false);
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

	public void KillBoss()
	{
		Destroy(this.gameObject);
	}

    public void Respawn()
    {
        vidaBoss = 10;
        barraVidaInicialX = 4;
        barraVida.transform.localScale = new Vector3(barraVidaInicialX, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        barraVidaInicialX = 4;
        SiguientePosicion();
    }
}
