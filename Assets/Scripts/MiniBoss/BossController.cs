using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    int vidaBoss;
    public bool muerto; // Cuando el boss llegue a 0 de vida se pondrá a 'true'
	
	void Start () 
    {
        muerto = false;
        vidaBoss = 0; //***TESTEO*** AL FINAL CAMBIARLO AL VALOR DE VIDA NORMAL
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (vidaBoss <= 0.001) // ***TESTEO*** AL FINAL CAMBIARLO A 0
            muerto = true;
	}
}
