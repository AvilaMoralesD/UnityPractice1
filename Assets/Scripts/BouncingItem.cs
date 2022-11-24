using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingItem : MonoBehaviour
{
    [SerializeField][Range(0.01f, 1)] float amplitud = 0.25f;
    [SerializeField][Range(0.1f, 5f)] float tiempoMovimiento = 1f;
    [SerializeField][Range(1f, 50f)] float velGiro = 20f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LerpBounce());
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.up * velGiro * Time.deltaTime;
    }
    private IEnumerator LerpBounce()
    {
        int direccion = 1;
        float tiempoUsado = tiempoMovimiento/2-Time.deltaTime;
        Vector3 posInferior = transform.position - Vector3.up * amplitud;
        Vector3 posSuperior = posInferior + 2 * Vector3.up * amplitud;
        while (true)
        {
            tiempoUsado += Time.deltaTime;
            if (direccion == 1)
                transform.position = Vector3.Lerp(posInferior, posSuperior, tiempoUsado / tiempoMovimiento);
            if (direccion == -1)
                transform.position = Vector3.Lerp(posSuperior, posInferior, tiempoUsado / tiempoMovimiento);
            if (tiempoUsado > tiempoMovimiento)
            {
                transform.position = transform.position.y >= posSuperior.y ? posSuperior : posInferior;
                direccion *= -1;
                tiempoUsado = 0;
            }
            yield return null;
        }
    }
}
