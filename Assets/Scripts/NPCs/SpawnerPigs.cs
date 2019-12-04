using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPigs : MonoBehaviour {

    public GameObject objetoSpawn;
    public float tiempoSpawn;


	void Start () {
        InvokeRepeating("Spawn", 0,  tiempoSpawn);
	}
	
    void Spawn ()
    {   if (isActiveAndEnabled)
            Instantiate(objetoSpawn, this.transform.position, this.transform.rotation);
    }
}
