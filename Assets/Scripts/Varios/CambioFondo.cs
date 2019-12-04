using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioFondo : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            GameManager.instance.CambiaFondo();
    }
}
