using UnityEngine;

public class RotadorExtremidades : MonoBehaviour
{
    [SerializeField]
    private bool activo = false;
    public int dir = 1;
    private const float ANGMIN = -30;
    private const float ANGMAX = 30;
    private const float VEL = 150;
    private float angActual, angInicial, cambioAng;
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

    public void IniciarAnimacion() { activo = true; }
    public void PararAnimacion()
    {
        activo = false;
        transform.localEulerAngles = new Vector3(angInicial, 0, 0);
    }
}