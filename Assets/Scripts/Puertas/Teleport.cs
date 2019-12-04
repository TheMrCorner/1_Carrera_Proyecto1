using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform puntoTeleport;

    void OnTriggerEnter2D(Collider2D other) // Entramos
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = puntoTeleport.position;
        }
    }
}
