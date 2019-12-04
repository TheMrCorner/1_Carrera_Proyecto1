using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCamara : MonoBehaviour
{

    public Camera camara;

     int zonaCastillo;

    // float timeInSecs;
   public float altura;
    public float distancia;
    float SlowTime;

    public float velocidadRegreso = 0.01f;

    private void Start()
    {
        SlowTime = altura / 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (zonaCastillo == 1)
           // {
                StartCoroutine(Stop());
            // }

            StartCoroutine(Distancia());

           // else if (zonaCastillo == 2)
           // {
             //   camara.GetComponent<Camera2DFollow>().yPosRestriction = -100f;
           // }
        }
    }

   IEnumerator Stop()
    {
        if (camara.GetComponent<Camera2DFollow>().yPosRestriction < altura)
        {
            while (camara.GetComponent<Camera2DFollow>().yPosRestriction < altura)
            {
                yield return new WaitForSeconds(0.000001f);
                camara.GetComponent<Camera2DFollow>().yPosRestriction += (0.05f);
            }
        }
        else
        {
            while (camara.GetComponent<Camera2DFollow>().yPosRestriction > altura)
            {
                yield return new WaitForSeconds(0.000001f);
                camara.GetComponent<Camera2DFollow>().yPosRestriction -= (velocidadRegreso);
            }
        }
    }
    IEnumerator Distancia()
    {
        if (camara.orthographicSize < distancia)
        {
            while (camara.orthographicSize < distancia)
            {
                yield return new WaitForSeconds(0.000001f);
                camara.orthographicSize += (0.1f);
            }
        }
        else
        {
            while (camara.orthographicSize > distancia)
            {
                yield return new WaitForSeconds(0.000001f);
                camara.orthographicSize -= (0.1f);
            }
        }
    }
}

