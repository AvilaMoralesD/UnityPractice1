using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    //Ejercicio 9
    [Range(1f, 800f)]
    public float velocidadRotacion = 200f;
    public float anguloXMax = 85;
    public float anguloXMin = -70;
    private float xAngle, yAngle = 0;
    private float cantidadX, cantidadY;
    public enum EjesRotacion { MouseX, MouseY, MouseXY };
    public EjesRotacion modoRotacion = EjesRotacion.MouseY;
    void Start()
    {
        //Ejercicio 4 Relacion anterior
        //Debug.Log("Posición: " + transform.position + "\nRotación" + transform.rotation.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        //Ejercicio 5 Relacion anterior
        // Debug.Log("Mouse X: " + Input.GetAxis("Mouse X").ToString("+0.00;-0.00")
        // + " Mouse Y: " + Input.GetAxis("Mouse Y").ToString("+0.00;-0.00"));

        //Ejercicios 9-11 Relacion anterior
        //transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0)
        // * velocidadRotacion * Time.deltaTime);

        xAngle = Mathf.Clamp(xAngle - velocidadRotacion * Time.deltaTime * Input.GetAxis("Mouse Y"), anguloXMin, 
        anguloXMax);
        yAngle += velocidadRotacion * Time.deltaTime * Input.GetAxis("Mouse X");
        
        //Ejercicio 12 Relacion anterior
        //transform.localEulerAngles = new Vector3(xAngle, yAngle, 0);

        //Ejercicio 13 Relacion anterior
        if (modoRotacion == EjesRotacion.MouseX)
            transform.localEulerAngles = new Vector3(0, yAngle, 0);
        if (modoRotacion == EjesRotacion.MouseY)
            transform.localEulerAngles = new Vector3(xAngle, 0, 0);
        if (modoRotacion == EjesRotacion.MouseXY)
            transform.localEulerAngles = new Vector3(xAngle, yAngle, 0);
    }
}

