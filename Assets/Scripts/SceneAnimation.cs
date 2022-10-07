using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject sol;
    public GameObject mercurio, venus, tierra, luna, marte, jupiter, saturno, urano, neptuno;
    public float angularVelocity = 100f;
    public float multSol = 0.2f;
    public float multMercurio = 0.5f;
    public float multVenus = 0.465f;
    public float multTierra = 0.423f;
    public float multLuna = 2f;
    public float multMarte = 0.344f;
    public float multJupiter = 0.314f;
    public float multSaturno = 0.214f;
    public float multUrano = 0.125f;
    public float multNeptuno = 0.10f;

    void Update()
    {
        sol.transform.Rotate(Vector3.up * angularVelocity * multSol * Time.deltaTime);
        mercurio.transform.Rotate(Vector3.up * angularVelocity * multMercurio * Time.deltaTime);
        venus.transform.Rotate(Vector3.up * angularVelocity * multVenus * Time.deltaTime);
        tierra.transform.Rotate(Vector3.up * angularVelocity * multTierra * Time.deltaTime);
        luna.transform.Rotate(Vector3.up * angularVelocity * multLuna * Time.deltaTime);
        marte.transform.Rotate(Vector3.up * angularVelocity * multMarte * Time.deltaTime);
        jupiter.transform.Rotate(Vector3.up * angularVelocity * multJupiter * Time.deltaTime);
        saturno.transform.Rotate(Vector3.up * angularVelocity * multSaturno * Time.deltaTime);
        urano.transform.Rotate(Vector3.up * angularVelocity * multUrano * Time.deltaTime);
        neptuno.transform.Rotate(Vector3.up * angularVelocity * multNeptuno * Time.deltaTime);
   }
}
