using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diálogos : MonoBehaviour
{
   // public Text texto;
    //public string dialogo;
    public bool boss;
    //public bool boss2;

    public BoxCollider2D activaCollider;
    public float tiempoActivacion;

    public bool eliminarCollider;
    public bool mantener;

    //public bool stopHere;

    public float WaitTimeInSecs;
    public float SlowTime;
    float WaitTime;


    bool entered;

    public Text[] conversacion;

    Collider2D other;
    private void Start()
    {
        WaitTime = WaitTimeInSecs * SlowTime;

    }

    private void Update()
    {
        if (entered && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.activaPausa = true;
        }

        AudioManager.Instance.boss = boss;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           
            entered = true;
            Dialogo(other, boss);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (mantener)
            { 
                GameManager.instance.activaPausa = true;
                GameManager.instance.CuadroDialogos.SetActive(false);
                if(eliminarCollider)
                    this.GetComponent<Collider2D>().enabled = false;

            }
            entered = false;
        }
    }


    IEnumerator Conversacion(Collider2D Player, bool boss)
    {
        for (int i = 0; i < conversacion.Length; i++)
            {
            if (eliminarCollider && !mantener)
            {
                this.GetComponent<Collider2D>().enabled = false;
            }

            if (entered)
                {
                    GameManager.instance.activaPausa = false;
                    Time.timeScale = SlowTime;
                }

                GameManager.instance.CuadroDialogos.SetActive(true);
                GameManager.instance.dialogos.text = conversacion[i].text;
                GameManager.instance.dialogos.color = conversacion[i].color;

                yield return new WaitForSeconds(WaitTime);
            }

        if (!mantener)
        {
            GameManager.instance.activaPausa = true;
            GameManager.instance.CuadroDialogos.SetActive(false);
        }
        // Player
         if (boss)
         {
             Player.gameObject.GetComponent<PlayerAttack>().enabled = true;
             Player.gameObject.GetComponentInChildren<Disparo>().enabled = true;
         }
         if (activaCollider != null)
         {
             Invoke("ActivaCollider", tiempoActivacion);
         }
            

    }

    void Dialogo(Collider2D Player,bool boss)
    {
       
        if (boss)
        {
            Player.gameObject.GetComponent<PlayerAttack>().enabled = false;
            Player.gameObject.GetComponentInChildren<Disparo>().enabled = false;
			AudioManager.Instance.CancelInvoke ();
			AudioManager.Instance.audioSource1.SetActive(false);
			AudioManager.Instance.audioSource2.SetActive(false);
        }
        StartCoroutine(Conversacion(Player, boss));
       
    }

    void ActivaCollider()
    {
        Debug.Log("activaCOLLIDER");
        activaCollider.enabled = true;

        if (boss)
        {
            AudioManager.Instance.audioSourceBoss.SetActive(true);
            AudioManager.Instance.audioSource2.SetActive(false);
        }
    }
}

