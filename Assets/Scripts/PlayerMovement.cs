using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector] public bool facingRight = true; 	//Booleano que nos ayudará a orientar el jugador, para aplicarle la fuerza en el sentido correcto.
	[HideInInspector] public bool jump = false;			//Booleano que comprueba si la posibilidad de saltar está disponible (si toca suelo).

	//Variables Públicas (Editor)
	public float moveForce = 365f;		//Velocidad de movimiento
	public float maxSpeed = 5f;			//Velocidad máxima
	public float jumpForce = 1000f;		//Fuerza de salto
	public Transform groundCheck;		//Comprueba si el jugador toca el suelo (está colocado en los pies de este)

	private bool grounded = false;		//Booleano que se activa si groundCheck toca el suelo (Tag = "Ground")

	//Componentes del Player
	private Animator anim;
	private Rigidbody2D rb2d;

	public float salto; //velocidad auxiliar
	public int dash = 8000; //Velocidad del dash
	bool Bdash; //Booleano que permite hacer dash
	bool saltoDoble; //Booleano para el salto doble (sin uso actualmente)

    public Disparo disparador; //Para activar o desactivar el disparo

	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		Bdash = true;
        disparador = disparador.GetComponent<Disparo>();
    }

    // Update is called once per frame
	void Update () 
	{     
		float h = Input.GetAxis("Horizontal"); //Eje Horizontal del GameObject (Funcion de Unity)
		//Lanzamos un rayo al suelo (dirección a groundCheck de longitud 1) y se iguala a grounded
		//Según lo que devuelva el rayo será true o false.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); 

		if (Input.GetButtonDown("Jump") && grounded)
		{
			jump = true; //Puede saltar  
			saltoDoble = false;
		}

        //Salto doble
        /*if (Input.GetButtonDown("Jump") && saltoDoble)
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x * salto, jumpForce));
            saltoDoble = false;
        }
        */

        //DASH
		if( Input.GetKeyDown(KeyCode.E) && Bdash){
			rb2d.AddForce(Vector2.right * h * dash);
			Bdash = false;
			Invoke ("CambiaDash", 1);
		}

        //Activar Disparo
        if (Input.GetKeyDown(KeyCode.Q))
        {
            disparador.enabled = true;
        }
        //Desactivar disparo
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            disparador.enabled = false;
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
			rb2d.AddForce(Vector2.right * h * moveForce); //Le aplicamos fuerza mientras esta sea menor a la velocidad maxima

		//Mathf.Abs devuelve el valor absoluto
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			//Mathg.sign devuelve 1 si es positivo o 0, devuelve -1 si es negativo
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}
			
		//Animación, según la velocidad se activan booleanos responsables de activar X animación
		if (h > 0) {
			//anim.SetBool ("Left", false);
			anim.SetBool ("Right", true);
		} else if (h < 0) {
			//anim.SetBool ("Right", false);
			anim.SetBool ("Left", true);
		} else {
			anim.SetBool ("Right", false);
			anim.SetBool ("Left", false);
		}

		//Posibilidad de salto si está en contacto con el suelo
		if (jump)
		{
			//anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0, jumpForce));
            saltoDoble = true;
            jump = false;        
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
}