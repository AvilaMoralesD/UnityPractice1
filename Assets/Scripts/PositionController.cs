using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    [Range(1f, 50f)]
    public float speed = 10f;
    //private float mult = 2;
    private static float CTEGRAV = -20f;
    private float gravity = CTEGRAV;

    //private float tiempoSpace, tiempoShift;
    private bool enSuelo;
    private float alturaMaxSalto = 6;
    private Vector3 movHor, movVer;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        //Ejercicio 2 Relacion anterior
        /*Debug.Log("A/D: " + Input.GetAxis("Horizontal").ToString("+0.00;-0.00")
         + " W/S: " + Input.GetAxis("Vertical").ToString("+0.00;-0.00")
         + "\n Space: " + tiempoSpace.ToString("+0.00;-0.00") +
          " Shift: " + tiempoShift.ToString("+0.00;-0.00"));*/
        //Ejericio 3
        //ws = Input.GetAxis("Vertical");

        if (enSuelo = characterController.isGrounded) //Aterrizaje
            movVer.y = 0;
        if (Input.GetKey(KeyCode.Space) && enSuelo)
        {
            movVer.y = 0;
            gravity = CTEGRAV;
            movVer.y += Mathf.Sqrt(alturaMaxSalto * -2f * gravity); //Le asigna su velocidad máxima Y
        }
        if (movVer.y < 2) //Cae más rápido de lo que sube
            gravity = 2 * CTEGRAV;
        movVer.y += gravity * Time.deltaTime; //La gravedad lo frena
        Vector3 movHor = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        characterController.Move(movHor * speed * Time.deltaTime);
        characterController.Move(movVer * Time.deltaTime);

        //Ejercicio 4 Relacion anterior
        /*if (Input.GetKey(KeyCode.Space))
        {
            tiempoSpace = tiempoSpace < 1 ? tiempoSpace + mult * Time.deltaTime : 1;
            transform.Translate(Vector3.up * tiempoSpace * speed * Time.deltaTime, Space.World);
        }
        tiempoSpace = Input.GetKeyUp(KeyCode.Space) ? 0 : tiempoSpace;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            tiempoShift = tiempoShift < 1 ? tiempoShift + mult * Time.deltaTime : 1;
            transform.Translate(Vector3.down * tiempoShift * speed * Time.deltaTime, Space.World);
        }
        tiempoShift = Input.GetKeyUp(KeyCode.LeftShift) ? 0 : tiempoShift;*/
    }
}
