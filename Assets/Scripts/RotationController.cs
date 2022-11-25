using UnityEngine;

public class RotationController : MonoBehaviour
{
    [Range(1f, 800f)]
    public float velocidadRotacion = 200f;
    public float anguloXMax = 85;
    public float anguloXMin = -70;
    private float xAngle, yAngle = 0;
    private bool ignorarLectura, bloqueoCamara = true;
    private float cantidadX, cantidadY;
    public enum EjesRotacion { MouseX, MouseY, MouseXY };
    public EjesRotacion modoRotacion = EjesRotacion.MouseY;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bloqueoCamara = !bloqueoCamara;
            if (bloqueoCamara == false)
            {
                ignorarLectura = true; //Si se desbloquea quiero que ignore el primer frame otra vez
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true; ;
            }
        }
        if (ignorarLectura)
        {
            ignorarLectura = false;
            return;
        }
        if (!bloqueoCamara)
        {
            xAngle = Mathf.Clamp(xAngle - velocidadRotacion * Time.deltaTime * Input.GetAxis("Mouse Y"), anguloXMin, anguloXMax);
            yAngle += velocidadRotacion * Time.deltaTime * Input.GetAxis("Mouse X");
            if (modoRotacion == EjesRotacion.MouseX)
                transform.localEulerAngles = new Vector3(0, yAngle, 0);
            if (modoRotacion == EjesRotacion.MouseY)
                transform.localEulerAngles = new Vector3(xAngle, 0, 0);
            if (modoRotacion == EjesRotacion.MouseXY)
                transform.localEulerAngles = new Vector3(xAngle, yAngle, 0);
        }
    }
}

