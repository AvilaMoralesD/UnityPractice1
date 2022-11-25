using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shotCrack; //Meter a mano desde fuera del código el prefab aquí
    [SerializeField][Range(0.1f, 10)] float fadeTime = 2f; // Este parámetro se debe poder configurar desde el editor, hazlo
    [SerializeField][Range(0.1f, 20)] float bulletSpeed = 3f;
    Camera cam;
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 1));
        RaycastHit hit;
        if ((Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && Physics.Raycast(ray, out hit))
            StartCoroutine(PlaceShotMark(hit));
    }

    private IEnumerator PlaceShotMark(RaycastHit hit)
    {
        bool botonPulsado = Input.GetMouseButtonDown(1); //Lo guardo para saber cual pulsé
        GameObject bulInst = Instantiate(bullet);
        bulInst.transform.position = cam.transform.position + cam.transform.forward * 0.3f;
        bulInst.transform.LookAt(hit.point);
        while (Vector3.Distance(bulInst.transform.position, hit.point) > 0.1f)
        {
            bulInst.transform.position += bulInst.transform.forward * Time.deltaTime * bulletSpeed;
            Debug.Log(bulInst.transform.position+ "Distancia: "+ Vector3.Distance(bulInst.transform.position, hit.point));
            yield return null;
        }
        Destroy(bulInst);
        GameObject shotMark = Instantiate(shotCrack);
        shotMark.transform.position = hit.point + hit.normal * 0.01f;
        shotMark.transform.LookAt(hit.point - hit.normal);
        shotMark.transform.SetParent(hit.transform, true);
        /*//Apartado4.4
        yield return new WaitForSeconds(5);
        Destroy(shotMark);*/
        Material m = shotMark.GetComponent<Renderer>().material;
        Color alphaColor = m.color;
        if (botonPulsado) //Con click derecho disparo lerps
            for (float s = 0; s < fadeTime; s += Time.deltaTime)
            {
                alphaColor.a = Mathf.Lerp(1, 0, s / fadeTime);
                m.color = alphaColor;
                yield return null;
            }
        else //Con boton central disparo smoothdamps
        {
            float vel = 0; // La usaremos para almacenar la velocidad devuelta por SmoothDamp
            while (alphaColor.a > 0.001)
            {
                alphaColor.a = Mathf.SmoothDamp(alphaColor.a, 0, ref vel, fadeTime);
                m.color = alphaColor;
                yield return null;
            }
        }
        Destroy(shotMark);
    }

    void OnGUI()
    { // Método que se invoca para dibujar el interfaz de usuario
        float size = 12; // Tamaño de fuente para dibujar el asterisco
        float posX = cam.pixelWidth / 2 - size / 4; // Calculamos el punto central
        float posY = cam.pixelHeight / 2 - size/1.5f; // de la pantalla.
        GUI.Label(new Rect(posX, posY, size, size), "*"); // Se dibuja un asterisco en esa posición.
    }
}