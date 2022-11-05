using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorExtremidades : MonoBehaviour
{
    [SerializeField]
    private bool activo = false;
    private const float ANGMIN = -30;
    private const float ANGMAX = 30;
    private const float VEL = 150;
    public int dir = 1;
    private float angActual, angInicial;
    private float cambioAng;

    void Start()
    {
        angInicial = angActual = transform.rotation.x;
    }

    void Update()
    {
        if (!activo)
            return;
        if (angActual > ANGMAX)
            dir = -1;
        if (angActual < ANGMIN)
            dir = 1;

        cambioAng = dir * VEL * Time.deltaTime;
        angActual += cambioAng;
        transform.Rotate(Vector3.right * cambioAng);

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

    public void IniciarAnimacion() { activo = true; }
    public void PararAnimacion()
    {
        activo = false;
        transform.localEulerAngles = new Vector3(angInicial, 0, 0);
    }


}

