using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;
    public GameObject audioSource1;
	public GameObject audioSource2;
    public GameObject audioSourceBoss;
    public bool boss = false;

	// Use this for initialization
	void Awake () {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

		Invoke ("Musica1", 18);

	}
	
    public void Playsound (AudioClip clip, GameObject objectToplayOn)
    {
        AudioSource.PlayClipAtPoint(clip, objectToplayOn.transform.position);
    }

	public void Musica1 (){
		audioSource1.gameObject.SetActive (false);

		audioSource2.gameObject.SetActive (true);
        if(!boss)
		    Invoke ("Musica2", 72);
	}

	public void Musica2 (){
		audioSource2.gameObject.SetActive (false);

		audioSource1.gameObject.SetActive (true);

        if (!boss)
		    Invoke ("Musica1", 18);
	}

    public void MusicaBoss()
    {
        //Desactivamos toda la música y ponemos la del boss
        audioSource1.gameObject.SetActive(false);
        audioSource2.gameObject.SetActive(false);
    }
}
