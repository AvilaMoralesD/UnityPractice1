using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionX : MonoBehaviour
{
    public int velocidad = 30;
    private int dir = 1;
    private float xActual;
    private const int LIMSUP = 50;
    private const int LIMINF = 5;

    void Update()
    {
        xActual = transform.localEulerAngles.x;
        if (xActual >= LIMSUP)
            dir = -1;
        if (xActual <= LIMINF)
            dir = 1;
        transform.Rotate(Vector3.right * Time.deltaTime * dir * velocidad);
    }
}
