using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarVueltas : MonoBehaviour
{
    public float gradosSeg = 10f;
    public float centroX, centroZ;

    Vector3 centro, direccion;
    void Start()
    {
        centro = new Vector3(centroX, transform.position.y, centroZ);
    }

    void Update()
    {
        transform.RotateAround(centro, Vector3.up, gradosSeg * Time.deltaTime);
    }
}
