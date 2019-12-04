using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onda : MonoBehaviour {
    public int damage = 5;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameManager.instance.Damage(damage);
    }


    public void DestruyeOnda()
    {
        Destroy(this.gameObject);
    }
}


