using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaActivacion : MonoBehaviour {

	public GameObject bossHUD;
    public GameObject finalBoss;
    public Camera camara;

    bool activaCamara;

     void Update()
    {
        if (activaCamara && camara.orthographicSize < 15)
        {
            camara.orthographicSize += 0.1f;
        }
       
      else  if (camara.orthographicSize >= 15)
            activaCamara = false;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            finalBoss.SetActive(true);
		    bossHUD.SetActive (true);
            activaCamara = true;
            Debug.Log("Hola " + activaCamara);
        }
    }
}
