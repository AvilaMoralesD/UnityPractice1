using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    public enum EstadoEnemigo { Parado = 0, Andando = 1 }
    private string[] extrem;
    public EstadoEnemigo estEnem = EstadoEnemigo.Parado;
    private RotadorExtremidades[] rotadores;
    private CharacterController characterController;
    [Range(5, 20)]
    public float velocidad = 5f;
    float duracionRayo = 4f;

    void Start()
    {
        rotadores = GetComponentsInChildren<RotadorExtremidades>();
        TryGetComponent<CharacterController>(out characterController);
        IniciarAnimacion();
    }

    // Update is called once per frame
    void Update()
    {
        updateState();
        applyState();
    }
    void updateState()
    {
        switch (estEnem)
        {
            case EstadoEnemigo.Parado:
                //Condicion inexistente para ponerlo a andar
                IniciarAnimacion();
                break;

            case EstadoEnemigo.Andando:
                //Sin condición de parada
                //PararAnimacion();
                break;
        }
    }

    void applyState()
    {
        switch (estEnem)
        {
            case EstadoEnemigo.Parado:
                //Nada que hacer
                break;

            case EstadoEnemigo.Andando:
                characterController.Move(transform.forward * Time.deltaTime * velocidad);
                Ray rayo = new Ray(transform.position + Vector3.up, transform.forward);
                RaycastHit hit;
                if (Physics.SphereCast(rayo, 0.25f, out hit) && hit.distance < 0.7f)
                {
                    Vector3 nuevaDireccion = Vector3.Reflect(transform.forward, hit.normal);
                    Debug.DrawLine(transform.position + Vector3.up, hit.point, Color.green, duracionRayo);
                    Debug.DrawRay(hit.point, hit.normal, Color.blue, duracionRayo);
                    Debug.DrawRay(hit.point, nuevaDireccion, Color.red, duracionRayo);
                    transform.LookAt(transform.position + new Vector3(nuevaDireccion.x, 0, nuevaDireccion.z)); //Soluciono el problema de muros diagonales
                }
                break;
        }
    }

    void IniciarAnimacion()
    {
        estEnem = EstadoEnemigo.Andando;
        foreach (var item in rotadores)
            item.IniciarAnimacion();
    }
    void PararAnimacion()
    {
        estEnem = EstadoEnemigo.Parado;
        foreach (var item in rotadores)
            item.PararAnimacion();
    }
}
