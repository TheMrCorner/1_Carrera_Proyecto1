using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovFlecha : MonoBehaviour {

    public int moveSpeed = 230;
    [HideInInspector]
    public float range = 1;

	// Update is called once per frame
	void Update () {
        transform.Translate (Vector3.right * (Time.deltaTime * moveSpeed));
        Destroy(gameObject, range);
	}
}
