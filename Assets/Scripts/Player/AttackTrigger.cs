using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {


    public int damage = 0;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger != true && other.gameObject.CompareTag("Enemigo"))
            other.SendMessageUpwards("Damage", damage);
    }
}
