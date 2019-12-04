using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBarca : MonoBehaviour
{

    public Camera camara;

    public float movX;
    public float posicionCamara;
    Vector3 posInicial;


    public bool JugadorEncima;
    [HideInInspector]
    public bool fin = false;
    public bool muerto;

    float posicionCamaraInicial;

    public GameObject personaje;

    void Start()
    {
        fin = false;
        posicionCamaraInicial = camara.orthographicSize;
        posInicial = this.transform.position;
        muerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fin)
        {
            
            if (JugadorEncima)
            {
                if (camara.orthographicSize < posicionCamara)
                {
                    camara.orthographicSize += 0.1f;
                }

                transform.Translate(movX*Time.deltaTime, 0, 0);

                //Movimiento personaje en barca
                personaje.transform.position = new Vector3(transform.position.x - 0.05f, personaje.transform.position.y, 0);
                personaje.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            }

           
        }

        else
        {
            if (!muerto)
            {
                if (movX > 0)
                {
                    GetComponent<Collider2D>().isTrigger = false;
                       
                    Frenada();
                    StartCoroutine(Camara());
                    Debug.Log("Frena");
                    //personaje.transform.position = transform.position + new Vector3(1f, 1f, 0);
                }
            }
            personaje.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            personaje.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GameManager.instance.Persecuccion = false;

          //  Destroy(gameObject, 10);

            fin = false;
            JugadorEncima = false;
            Debug.Log("me bajo");
            /*GameObject nuevo = Instantiate(gameObject);
            nuevo.transform.position = posInicial;*/
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            movX = 9;
            JugadorEncima = true;
            GameManager.instance.Persecuccion = true;
            muerto = true;
        }
    }


    void Frenada()
    {
        StartCoroutine(Frena());
    }

    IEnumerator Frena()
    {
        while (movX > 0)
        {
            transform.Translate(movX*Time.deltaTime, 0, 0);
            movX -= 0.75f;
         
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }

    IEnumerator Camara()
    {


        if (camara.orthographicSize > posicionCamaraInicial)
        {
            while (camara.orthographicSize > posicionCamaraInicial)
            {
                yield return new WaitForSeconds(0.000001f);
                camara.orthographicSize -= (0.15f);
            }
           
        }
        else
        {
            while (camara.orthographicSize < posicionCamaraInicial)
            {
                yield return new WaitForSeconds(0.000001f);
                camara.orthographicSize += (0.1f);
            }

        }

    }
}
        

