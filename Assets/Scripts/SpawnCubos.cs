using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desplazador : MonoBehaviour
{
    List<object> cubos;
    void Start()
    {
        cubos = new List<object>();
        StartCoroutine(Desplazar());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Desplazar()
    {
        Vector3 pos = transform.position;
        float x1 = pos.x;
        float x2 = pos.x + 3;
        float v = 1;

        for (float x = x1; x < x2; x += Time.deltaTime * v)
        {

            transform.position = new Vector3(x, pos.y, pos.z);
            yield return null;

        }
    }
    

    IEnumerator SpawnerDeCubos()
    {
        GameObject cubo;
        int maxCubo = 10;
        while (maxCubo >= cubos.Count)
        {

            cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubo.transform.position = new Vector3(
                Random.Range(0, 10),
                0,
                Random.Range(0, 10)
            );
            cubos.Add(cubo);

            yield return new WaitForSeconds(2);
        }
    }
}