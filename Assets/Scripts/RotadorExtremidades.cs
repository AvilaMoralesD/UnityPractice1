using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorExtremidades : MonoBehaviour
{
    private const float ANGMIN = -30;
    private const float ANGMAX = 30;
    private const float VEL = 150;
    public int dir = 1;
    private float angActual;
    private float cambioAng;
    public enum modo { rotaX, rotaY, rotaZ }
    public modo modoRotacion = modo.rotaX;


    void Start()
    {
        angActual = transform.rotation.x;
    }

    void Update()
    {
        if (angActual > ANGMAX)
            dir = -1;
        if (angActual < ANGMIN)
            dir = 1;

        cambioAng = dir * VEL * Time.deltaTime;
        angActual += cambioAng;
        
        switch (modoRotacion)
        {
            case modo.rotaX:
                transform.Rotate(Vector3.right * cambioAng);
                break;
            case modo.rotaY:
                transform.Rotate(Vector3.up * cambioAng);
                break;
            case modo.rotaZ:
                transform.Rotate(Vector3.forward * cambioAng);
                break;
        }
    }
}
