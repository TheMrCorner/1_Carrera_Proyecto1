using UnityEngine;
using System.Collections;

public class FirePointRotater : MonoBehaviour
{
    public int rotationOffset = 90;
    public Transform objetivo;
    Transform actual;
    void Start()
    {
        actual = objetivo;
    }
    // Update is called once per frame
    void Update()
    {
        if (actual.position != objetivo.position)
        {
            Vector3 difference = objetivo.position - transform.position;
            difference.Normalize();		// normalizing the vector. Meaning that all the sum of the vector will be equal to 1

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;	// find the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }
}
