using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcaEnemiga : MonoBehaviour
{

    public int colliderX;


    float movX;
    float movJugador;

    bool persecucion;
    bool enemigoMuerto;

    MovimientoBarca barcaJugador;
    public GameObject enemigo;
    public GameObject barcaPlayer;
    GameObject nuevo;

    bool fin;

    void Start()
    {
        enemigoMuerto = false;
        persecucion = false;

        nuevo = Instantiate(enemigo);

        barcaJugador = barcaPlayer.GetComponent<MovimientoBarca>();
        movJugador = barcaJugador.movX;
        movX = barcaJugador.movX * 1.5f;
    }


    void Update()
    {
        // Debug.Log("Enemigo muerto: " + enemigoMuerto + " " + " Persecuccion: " + persecucion + "VelocidadJug: " +movJugador + "Velocidad: " + movX);
        if (GameManager.instance.Persecuccion && !enemigoMuerto)
        {
            if (!persecucion)
            {
                transform.Translate(movX * Time.deltaTime, 0, 0);
                nuevo.transform.position = transform.position + new Vector3(-0.05f, 0.7f, 0);
            }
            else
            {
                transform.Translate(movJugador * Time.deltaTime, 0, 0);
                nuevo.transform.position = transform.position + new Vector3(-0.05f, 0.7f, 0);
            }
            if (nuevo.GetComponent<EnemyAI>().currentHealth <= 0)
            {
                enemigoMuerto = true;
            }
        }
        else
        {
            Frenada();
            GetComponent<Collider2D>().isTrigger = false;
            if (gameObject != null)
            {
                Destroy(gameObject, 2);
                Destroy(nuevo);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            persecucion = true;
        }
    }

    void Frenada()
    {
        StartCoroutine(Frena());
    }

    IEnumerator Frena()
    {
        while (movJugador > 0)
        {
            transform.Translate(movJugador * Time.deltaTime, 0, 0);
            movJugador -= 0.5f;

            yield return new WaitForSeconds(0.8f);
        }
    }

}
