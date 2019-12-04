using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Añadido el onStayAttack y onEnterPlayerCollider para hacer mas coherente el ataque del enemigo.
public class EnemyAttackTrigger : MonoBehaviour {
	// Daño que hace el enemigo.
	public int damage = 0;
    // Tiempo entre ataques del enemigo.
    public float cooldownValue = 5;
    public bool attacking;
    public bool onStayAttack;
    public bool onEnterPlayerCollider;
    private Animator anim;
    private EnemyMovement enemyMov;
    private float cooldownTimer;
    

    void Awake () {
		anim = GetComponentInParent<Animator> ();       
		attacking = true;
        onStayAttack = false;
        onEnterPlayerCollider = false;
		enemyMov = GetComponentInParent<EnemyMovement>();
        cooldownTimer = cooldownValue;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            if (onEnterPlayerCollider == true)
                onStayAttack = true;

            attacking = true;
            cooldownTimer = cooldownValue;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.isTrigger != true && other.gameObject.CompareTag ("Player") && attacking) {
            if (enemyMov != null) //If añadido ahora
                enemyMov.playerDetected = true;
            onEnterPlayerCollider = true;
            CancelAttack();
            Debug.Log("ONENTER");


            if (enemyMov != null)
                enemyMov.enabled = false; 
		}
	}
		
	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.isTrigger != true && other.gameObject.CompareTag ("Player") && attacking && onStayAttack) {
            CancelAttack();

            Debug.Log("ONSTAY");
            
			if (enemyMov != null)
				enemyMov.enabled = false;
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
        if (other.isTrigger != true && other.gameObject.CompareTag("Player"))
        {
            cooldownTimer = cooldownValue;
      
            onEnterPlayerCollider = false;
            onStayAttack = false;
            attacking = true;
            anim.SetBool("Attack", false);
            Debug.Log("ONEXIT");

            if (enemyMov != null)
				enemyMov.enabled = true;
        } 
	}

	// Desactiva el ataque para que el enemigo no pueda atacar sin pausa. Lo mismo sobra
	void CancelAttack() 
	{
		attacking = false;
        onStayAttack = false;
        anim.SetBool("Attack", true);
        GameManager.instance.Damage(damage);
	}
}
