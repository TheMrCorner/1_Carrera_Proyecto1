using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenciaJug : MonoBehaviour {

    BossIA boss;
    public GameObject jefazo;
    void Start()
    {
        boss = jefazo.GetComponent<BossIA>();  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            boss.player = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            boss.player = false;
        }
    }
}
