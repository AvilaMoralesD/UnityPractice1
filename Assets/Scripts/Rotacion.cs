using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    public float angularVelocity = 100f;
    void Update()
    {
        transform.Rotate(Vector3.up * angularVelocity * Time.deltaTime);
    }
}
