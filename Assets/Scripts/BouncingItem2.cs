using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingItem2 : MonoBehaviour
{
    [SerializeField][Range(0.01f, 1)] float amplitud = 0.25f;
    [SerializeField][Range(0.1f, 5f)] float tiempoMovimiento = 1f;
    [SerializeField][Range(1f, 50f)] float velGiro = 20f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SmoothDampBounce());
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.up * velGiro * Time.deltaTime;
    }
    private IEnumerator SmoothDampBounce()
    {
        int direccion = 1;
        Vector3 vel = Vector3.zero;
        Vector3 posInferior = transform.position - Vector3.up * amplitud;
        Vector3 posSuperior = posInferior + 2 * Vector3.up * amplitud;
        while (true)
        {
            Vector3 objetivo = direccion == 1 ? posSuperior : posInferior;
            transform.position = Vector3.SmoothDamp(transform.position, objetivo, ref vel, tiempoMovimiento);
            if (Vector3.Distance(transform.position, objetivo) < 0.05f){
                direccion *= -1;
            }
            yield return null;
        }

    }
}

