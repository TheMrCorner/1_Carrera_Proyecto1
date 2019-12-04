using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenciaJugFinalBoss : MonoBehaviour {
	
	public GameObject boss;

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && boss != null)
		{
            if (boss.GetComponent<FinalBoss>() != null)
                boss.GetComponent<FinalBoss>().player = true;
            else if (boss.GetComponent<FinalBossFase2>() != null)
                boss.GetComponent<FinalBossFase2>().player = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player" && boss != null)
		{
            if (boss.GetComponent<FinalBoss>() != null)
                boss.GetComponent<FinalBoss>().player = false;
            else if (boss.GetComponent<FinalBossFase2>() != null)
                boss.GetComponent<FinalBossFase2>().player = false;
        }
	}
}