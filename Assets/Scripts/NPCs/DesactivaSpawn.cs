using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivaSpawn : MonoBehaviour {

    public GameObject[] spawns;
    GameObject[] enemigos;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) 
            DesactivarSpawn();
    }

    public void DesactivarSpawn () {
        for (int i = 0; i < spawns.Length; i++)
            spawns[i].GetComponent<SpawnerPigs>().enabled = false;

        enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

        foreach (GameObject Enemigo in enemigos)
        {
            if (Enemigo.GetComponent<PigAI>() != null)
                Destroy(Enemigo);
        }
    }
}
