
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CabezaEstado { MirandoAlante = 0, GiroAObjetivo = 1, MirandoObjetivo = 2, GiroHaciaAlante = 3 }

public class CabezaControl
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


public class RotadorCabeza : MonoBehaviour
{
    GameObject target;
    public float duracionGiro = 0.25f;
    public CabezaEstado estado = CabezaEstado.MirandoAlante;
    bool bEnteringState = false;
    CabezaControl control;
    float timeInState = 0;
    Vector3 ultimaPosEnQueSeVioElObjetivo;
    float distanciaUltimaPosEnQueSeVioElObjetivo;

    void Start() { target = GameObject.FindWithTag("Player"); }
    void Update()
    {
        updateState();
        applyState();
    }

    void updateState()
    {
        CabezaControl c = control = getControlData();

        // General state
        switch (estado)
        {
            case CabezaEstado.MirandoAlante:
                if (c.bObjetivoDentro) { makeStateTransition(CabezaEstado.GiroAObjetivo); break; }
                makeStateTransition(estado);
                break;

            case CabezaEstado.GiroAObjetivo:
                if (c.bGiroHaciaObjetivoFinalizado) { makeStateTransition(CabezaEstado.MirandoObjetivo); break; }
                if (c.bObjetivoFuera) { makeStateTransition(CabezaEstado.GiroHaciaAlante); break; }
                makeStateTransition(estado);
                break;

            case CabezaEstado.MirandoObjetivo:
                if (c.bObjetivoFuera) { makeStateTransition(CabezaEstado.GiroHaciaAlante); break; }
                makeStateTransition(estado);
                break;

            case CabezaEstado.GiroHaciaAlante:
                if (c.bGiroHaciaAlanteFinalizado) { makeStateTransition(CabezaEstado.MirandoAlante); break; }
                makeStateTransition(estado);
                break;
        }
    }

    CabezaControl getControlData()
    {
        CabezaControl c = new CabezaControl();

        c.posicionCabeza = transform.position;
        c.posicionCuerpo = transform.parent.position;
        c.posicionObjetivo = target.transform.position + Vector3.up * 1.5f;

        c.direccionCuerpo = transform.parent.forward;
        c.direccionCabeza = transform.forward;

        c.direccionHaciaObjetivo = (c.posicionObjetivo - c.posicionCabeza).normalized;

        c.alineacionCabezaObjetivo = Vector3.Dot(c.direccionCabeza, c.direccionHaciaObjetivo);
        c.alineacionCabezaCuerpo = Vector3.Dot(c.direccionCuerpo, c.direccionCabeza);
        c.alineacionCuerpoObjetivo = Vector3.Dot(c.direccionCuerpo, c.direccionHaciaObjetivo);

        c.bGiroHaciaAlanteFinalizado = Mathf.Approximately(c.alineacionCabezaCuerpo, 1);
        c.bGiroHaciaObjetivoFinalizado = Mathf.Approximately(c.alineacionCabezaObjetivo, 1);
        c.bObjetivoFuera = c.alineacionCuerpoObjetivo < 0.3;

        //Debug.Log($"alineacionCabezaCuerpo={c.alineacionCabezaCuerpo}");
        //Debug.Log($"alineacionCuerpoObjetivo={c.alineacionCuerpoObjetivo}");
        c.bObjetivoDentro = !c.bObjetivoFuera;

        return c;
    }

    void applyState()
    {
        switch (estado)
        {
            case CabezaEstado.MirandoAlante:
                transform.LookAt(control.posicionCabeza + control.direccionCabeza * 5);
                break;

            case CabezaEstado.GiroAObjetivo:
                transform.LookAt(Vector3.Lerp(
                    control.posicionCabeza + control.direccionCuerpo,
                    control.posicionObjetivo,
                    timeInState / duracionGiro
                ));
                break;

            case CabezaEstado.MirandoObjetivo:
                transform.LookAt(control.posicionObjetivo);
                break;

            case CabezaEstado.GiroHaciaAlante:
                if (bEnteringState)
                {
                    ultimaPosEnQueSeVioElObjetivo = control.posicionObjetivo;
                    distanciaUltimaPosEnQueSeVioElObjetivo = (ultimaPosEnQueSeVioElObjetivo - control.posicionCabeza).magnitude;
                }
                transform.LookAt(Vector3.Lerp(
                    ultimaPosEnQueSeVioElObjetivo,
                    control.posicionCabeza + control.direccionCuerpo * distanciaUltimaPosEnQueSeVioElObjetivo,
                    timeInState / duracionGiro
                ));
                break;
        }

        timeInState += Time.deltaTime;
    }


    void makeStateTransition(CabezaEstado nuevoEstado)
    {
        bEnteringState = nuevoEstado != estado;
        if (bEnteringState)
        {
            //Debug.Log($"{estado} -> {nuevoEstado}");
            timeInState = 0;
        }
        estado = nuevoEstado;
    }

}
