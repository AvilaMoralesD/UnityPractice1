using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField]
    GameObject shotCrack; //Meter a mano desde fuera del código el prefab aquí
    Camera cam;
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 1));
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit))
        {
            GameObject shotMark = Instantiate(shotCrack) as GameObject;
            //Para evitar que se esconda el quad en la superficie en la que colisiona se añade un poco del vector normal
            //Si no, se producirían efectos visuales extraños por Z-Fighting
            shotMark.transform.position = hit.point + hit.normal * 0.01f;
            //Punto - Vector = Extremo de -Vector desde Punto, el Quad solo se ve si miras desde el lado opuesto a +Z
            shotMark.transform.LookAt(hit.point - hit.normal);
            shotMark.transform.SetParent(hit.transform, true);

        }
    }

    void OnGUI()
    { // Método que se invoca para dibujar el interfaz de usuario
        float size = 12; // Tamaño de fuente para dibujar el asterisco
        float posX = cam.pixelWidth / 2 - size / 2; // Calculamos el punto central
        float posY = cam.pixelHeight / 2 - size / 2; // de la pantalla.
        GUI.Label(new Rect(posX, posY, size, size), "*"); // Se dibuja un asterisco en esa posición.
    }
}