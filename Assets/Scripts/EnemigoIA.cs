using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl
{
    public bool bObjetivoDentro = false;
    public bool bObjetivoFuera = false;
    public bool bGiroHaciaAlanteFinalizado = false;
    public bool bGiroHaciaObjetivoFinalizado = false;
    public float alineacionCabezaCuerpo = 0;
    public float alineacionCabezaObjetivo = 0;
    public float alineacionCuerpoObjetivo = 0;
    public Vector3 direccionCuerpo, direccionCabeza, direccionHaciaObjetivo;
    public Vector3 posicionCuerpo, posicionCabeza, posicionObjetivo;
}

public class EnemigoIA : MonoBehaviour
{
    enum EstadoEnemigo { Parado = 0, Andando = 1 }
    private string[] extrem;
    private EstadoEnemigo estEnem = EstadoEnemigo.Parado;
    private RotadorExtremidades[] rotadores;
    void Start()
    {
        rotadores = GetComponentsInChildren<RotadorExtremidades>();
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
        ObjectControl c = control = getControlData();

        // General state
        switch (estEnem)
        {
            case EstadoEnemigo.Parado:
                //Condicion inexistente para 
                IniciarAnimacion();
                break;

            case EstadoEnemigo.Andando:
                //Sin condición de parada
                //PararAnimacion();
                break;
        }
    }

    ObjectControl getControlData()
    {
        ObjectControl c = new ObjectControl();

        c.posicionCabeza = transform.position;
        c.posicionCuerpo = transform.parent.position;

        c.direccionCuerpo = transform.parent.forward;
        c.direccionCabeza = transform.forward;

        c.direccionHaciaObjetivo = (c.posicionObjetivo - c.posicionCabeza).normalized;

        c.alineacionCabezaObjetivo = Vector3.Dot(c.direccionCabeza, c.direccionHaciaObjetivo);
        c.alineacionCabezaCuerpo = Vector3.Dot(c.direccionCuerpo, c.direccionCabeza);
        c.alineacionCuerpoObjetivo = Vector3.Dot(c.direccionCuerpo, c.direccionHaciaObjetivo);

        c.bGiroHaciaAlanteFinalizado = Mathf.Approximately(c.alineacionCabezaCuerpo, 1);
        c.bGiroHaciaObjetivoFinalizado = Mathf.Approximately(c.alineacionCabezaObjetivo, 1);
        c.bObjetivoFuera = c.alineacionCuerpoObjetivo < 0.3;

        c.bObjetivoDentro = !c.bObjetivoFuera;

        return c;
    }

    void applyState()
    {
        switch (estEnem)
        {
            case EstadoEnemigo.Parado:
                //Nada que hacer
                break;

            case EstadoEnemigo.Andando:
                transform.position = transform.forward;
                Ray rayo = new Ray(
transform.position + Vector3.up + transform.forward, //TODO poner el pivote en pies
transform.forward
);
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
