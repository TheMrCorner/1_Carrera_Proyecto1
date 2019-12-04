using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cré3ditos : MonoBehaviour {
    public float velc;
    float x;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = this.GetComponent <RectTransform>();
        x = rectTransform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        rectTransform.position = new Vector2 (x, (velc++));
	}
}
