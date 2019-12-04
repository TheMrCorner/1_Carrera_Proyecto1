using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    // Colliders de ataque
    public Collider2D attackTriggerRight;
    public Collider2D attackTriggerLeft;

    public float attackCd;

    //Este booleano tendra mas utilidad si metemos algun tipo de cd interno
    public bool attacking;
	public int HP = 5;
    private Animator anim;

    //Sonido del ataque
    public AudioClip AttackClip;

    void Awake()
    {
        anim = GetComponent<Animator>();
        attacking = false;
        attackTriggerRight.enabled = false;
        attackTriggerLeft.enabled = false;
    }

    void Update () {

		if (Input.GetKeyDown(KeyCode.Mouse0) && !attacking) {

            attacking = true;

            anim.SetBool("Attack", true);

            //Comprueba la animacion actual para activar el collider izquierdo o derecho
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkingRight") || anim.GetCurrentAnimatorStateInfo(0).IsName("Stop_Right")
				|| anim.GetCurrentAnimatorStateInfo(0).IsName("WalkingRightDesnudo") || anim.GetCurrentAnimatorStateInfo(0).IsName("Stop_Right_Desnudo"))
                    attackTriggerRight.enabled = true;
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkingLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("Stop_Left")
				|| anim.GetCurrentAnimatorStateInfo(0).IsName("WalkingLeftDesnudo") || anim.GetCurrentAnimatorStateInfo(0).IsName("Stop_Left_Desnudo"))
                attackTriggerLeft.enabled = true;
        }
    }
			
    // Desactiva la animacion de ataque ademas de los colliders, cuando pasa el tiempo elegido como cd invoca el metodo que permite volver a atacar.
    public void AttackCd()
    {
        if (attacking)
        {
            anim.SetBool("Attack",false);
            attackTriggerRight.enabled = false;
            attackTriggerLeft.enabled = false;
            AudioManager.Instance.Playsound(AttackClip, gameObject);

            // Controla el CD
            Invoke("CancelAttackAnimation", attackCd);
        }
    }


    public void CancelAttackAnimation()
    {
        attacking = false;
    }
		
}
