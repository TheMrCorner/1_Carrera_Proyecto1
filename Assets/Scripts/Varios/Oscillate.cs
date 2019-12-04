using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour {
	float posY;
    public float oscilacion = 0f;
	// Update is called once per frame
	void Update () {
		posY = transform.position.y;
		transform.position = new Vector2(transform.position.x, posY+=(Mathf.Sin(Time.unscaledTime * 2) * (oscilacion)));
	}
}
