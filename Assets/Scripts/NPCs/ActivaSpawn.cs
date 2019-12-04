using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaSpawn : MonoBehaviour {

    public GameObject[] spawns;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) { 
            for (int i = 0; i < spawns.Length; i++)
                spawns[i].GetComponent<SpawnerPigs>().enabled = true;
        }
    }
}