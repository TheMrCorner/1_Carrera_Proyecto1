using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //Variables publicas
    //Velocidad y movimiento del enemigo
    public LayerMask enemyMask;
    public float speed;
    public Transform limiteDcha;
    public Transform limiteIzda;
	public GameObject player;
    public GameObject barraVida;
       
    //Detecta si hay jugador, para perseguirle o no
    public bool isPlayer;
    public bool facingLeft;
    public bool playerDetected;
    //Variables privadas
    private Rigidbody2D bodyEnem;
	private Transform myTrans;
	private float myWidth;

    
    int dirRayo = 1;


	// Use this for initialization
    void Start()
    {
        playerDetected = false;
        facingLeft = true;
        myTrans = this.transform;
		myWidth = this.GetComponent<SpriteRenderer> ().bounds.extents.x;
        bodyEnem = this.GetComponent<Rigidbody2D>();
    }

	void Update (){


        //Checking if there is ground infront of us, in order to keep the enemy inside the platform
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
			bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down, enemyMask);

			if (dirRayo < 0) {
				isPlayer = Physics2D.Linecast (lineCastPos, (lineCastPos + Vector2.right), enemyMask);
				Debug.DrawLine (lineCastPos, (lineCastPos + Vector2.right));
			} else if (dirRayo > 0) {
				isPlayer = Physics2D.Linecast (lineCastPos, (lineCastPos + Vector2.left), enemyMask);
				Debug.DrawLine (lineCastPos, (lineCastPos + Vector2.left));
			}


			//If there is no platform, turn around
			if (!isGrounded) {
				Flip ();
			}


        if (playerDetected)
        {

            if (player.transform.position.x > transform.position.x && facingLeft) { 
                Flip();
                Debug.Log("Primer");
            }
            else if (player.transform.position.x < transform.position.x && !facingLeft)
            {
                Flip();
                Debug.Log("Segundo");
            }
        }


        Movement ();
		   
    }

    void Movement()
    {
		Vector2 myVel = bodyEnem.velocity;
		myVel.x = -myTrans.right.x * speed;
		bodyEnem.velocity = myVel;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "LimiteDcha" || other.tag == "LimiteIzda") {
            playerDetected = false;
            Flip (); 
        }
    }

    //Voltea al enemigo
    void Flip()
    {
        if (facingLeft)
            facingLeft = false;
        else
            facingLeft = true;
        //facingRight = !facingRight;
        Vector3 currRotation = myTrans.eulerAngles;
		currRotation.y += 180;
		myTrans.eulerAngles = currRotation;
        dirRayo *= -1;

        // Esto hace que la barra de vida no voltee horizontalmente
        if (currRotation.y == 180)
            barraVida.GetComponent<SpriteRenderer>().flipX = true;
        else
            barraVida.GetComponent<SpriteRenderer>().flipX = false;
    }   
}


/*//El enemigo se configurará de la siguiente forma:
Enemigo
    >>>LimiteDcha
       >>>RigidBody2D  --> BodyType : static
    >>>LimiteIzda
       >>>RigidBody2D  --> BodyType : static
 */
