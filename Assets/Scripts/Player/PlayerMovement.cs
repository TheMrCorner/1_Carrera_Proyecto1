using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector] public bool facingRight = true; 	//Booleano que nos ayudará a orientar el jugador, para aplicarle la fuerza en el sentido correcto.
	[HideInInspector] public bool jump = false;			//Booleano que comprueba si la posibilidad de saltar está disponible (si toca suelo).

	//Variables Públicas (Editor)
	public float moveForce = 365f;		//Velocidad de movimiento
	public float maxSpeed = 5f;			//Velocidad máxima
	public float jumpForce = 16;		//Fuerza de salto
	public Transform groundCheck;		//Comprueba si el jugador toca el suelo (está colocado en los pies de este)
	public Transform platformCheck;		//Comprueba si el jugador tiene techo encima

	//Desnudez
	public bool apareceVestido;
	[HideInInspector]public bool seHaCogidoUnCofre = false;
	//Disparo
	public Disparo disparador; //Para activar o desactivar el disparo
	public GameObject arco;
	public GameObject brazo;
	ArmRotation offSet;

	private bool grounded = false;		//Booleano que se activa si groundCheck toca el suelo (Tag = "Ground")
	[HideInInspector] public bool escalera = false;
	[HideInInspector] public float velocidadEscalera;

	//Componentes del Player
	private Animator anim;
	private Rigidbody2D rb2d;
	public int dash = 8000; //Velocidad del dash
	bool Bdash; //Booleano que permite hacer dash
	bool saltoDoble; //Booleano para el salto doble (sin uso actualmente)
	float gravedad;

    //Sonido
    public AudioClip DamageClip;
    public AudioClip JumpClip;
    public AudioClip DashClip;
    public AudioClip ArrowClip;
	public AudioClip PlatCaida;
	public AudioClip PlatImpulsa;


	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		Bdash = true;
		disparador = disparador.GetComponent<Disparo>();
		offSet = brazo.GetComponent<ArmRotation>();
		apareceVestido = false;	
		gravedad = -3;
		Physics.gravity = new Vector2(0, gravedad);
	}

	public void setDesnudo(bool vestido){
		apareceVestido = vestido;
		anim.SetBool ("Naked", apareceVestido);
	}

	// Update is called once per frame
	void Update () 
	{     
		
		float h = Input.GetAxis("Horizontal"); //Eje Horizontal del GameObject (Funcion de Unity)
		//Lanzamos un rayo al suelo (dirección a groundCheck de longitud 1) y se iguala a grounded
		//Según lo que devuelva el rayo será true o false.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); 

		if (Input.GetKeyDown(KeyCode.W) && grounded && !escalera)
		{
			jump = true; //Puede saltar  
			saltoDoble = false;

            //Sonido
            AudioManager.Instance.Playsound(JumpClip, gameObject);
		}

		//Salto doble
		if (Input.GetKeyDown(KeyCode.W) && saltoDoble)
		{
			rb2d.velocity = new Vector2(0, 13);
			//Atravesar plataformas de arriba
			RaycastHit2D hit = Physics2D.Linecast(transform.position, platformCheck.position, 1 << LayerMask.NameToLayer("Ground"));
			if (hit)
				hit.collider.isTrigger = true;
			saltoDoble = false;

            //Sonido
            AudioManager.Instance.Playsound(JumpClip, gameObject);
		}

		//SUBIR ESCALERAS
		if (Input.GetKey(KeyCode.W) && escalera)
		{
			rb2d.velocity += (new Vector2(0, velocidadEscalera));
			RaycastHit2D hit = Physics2D.Linecast(transform.position, platformCheck.position, 1 << LayerMask.NameToLayer("Ground"));
			if (hit)
				hit.collider.isTrigger = true;
		}

		//DASH
		if(Input.GetKeyDown(KeyCode.Space) && Bdash){
			rb2d.AddForce(Vector2.right * h * dash);
			Bdash = false;
            Invoke("CambiaDash", 1);

            //Sonido
            AudioManager.Instance.Playsound(DashClip, gameObject);
		}

		//PAUSE
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)){
			GameManager.instance.TogglePause ();
		}

		//Activar Disparo
		if (Input.GetButtonDown("Fire2"))
		{
			maxSpeed /= 2;
			arco.SetActive(true);

			if (GameManager.instance.borracho)
			{
				offSet.rotationOffset = Random.Range(80, 111);
			}

		}
		//Desactivar disparo
		else if (Input.GetButtonUp("Fire2"))
		{
			maxSpeed *= 2;
			arco.SetActive(false);
			if (offSet.rotationOffset > 90)
                offSet.rotationOffset = 90;

            //Sonido 
            if (GameManager.instance.flechas >= 1)
                AudioManager.Instance.Playsound(ArrowClip, gameObject);
		}

	}

	//Cambia el estado del booleano Bdash
	void CambiaDash(){
		Bdash = !Bdash;
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal"); //Eje Horizontal del GameObject (Funcion de Unity)

        if (h * rb2d.velocity.x < maxSpeed)
        {
            rb2d.AddForce(Vector2.right * h * moveForce); //Le aplicamos fuerza mientras esta sea menor a la velocidad maxima
        }

		//Mathf.Abs devuelve el valor absoluto
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			//Mathg.sign devuelve 1 si es positivo o 0, devuelve -1 si es negativo
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}

		//Animación, según la velocidad se activan booleanos responsables de activar X animación
		if (h > 0) {
			anim.SetBool ("Left", false);
			anim.SetBool ("Right", true);
		} else if (h < 0) {
			anim.SetBool ("Right", false);
			anim.SetBool ("Left", true);
		} else {
			anim.SetBool ("Right", false);
			anim.SetBool ("Left", false);
		}

		//Posibilidad de salto si está en contacto con el suelo
		if (jump)
		{
			rb2d.velocity = new Vector2(0, jumpForce);
			saltoDoble = true;
			//Atravesar plataformas de arriba
			RaycastHit2D hit = Physics2D.Linecast(transform.position, platformCheck.position, 1 << LayerMask.NameToLayer("Ground"));
			if (hit) {
				hit.collider.isTrigger = true;
				Debug.Log ("Vamos a saltar");
			}

			jump = false;        
		}

		//Plataformas que caen
		if (grounded) {
			RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
			if (hit.collider != null && hit.collider.tag == "PlataformaCaida") {
				Debug.Log ("Vamos a caernos");
				PlataformaCae caida = hit.collider.GetComponent<PlataformaCae> ();

				AudioManager.Instance.Playsound(PlatCaida, gameObject);

				caida.Caera ();
			} 
			else if (hit.collider != null && hit.collider.tag == "PlataformaImpulsa") {
                rb2d.velocity = new Vector2(0, jumpForce*1.5f);

				AudioManager.Instance.Playsound(PlatImpulsa, gameObject);
			}
		}
	}

	//Método para voltear al jugador y aplicarle la fuerza correctamente
	//Del sprite mostrado se encarga FixedUpdate y el animator
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }
   

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("CofreCerrado")) {
			setDesnudo (false);
            seHaCogidoUnCofre = true;
			Debug.Log("Estoy vestido!");
		}
        else if (other.gameObject.CompareTag("Llave"))
        {
            GameManager.instance.AddKey();
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}