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
    }
}
