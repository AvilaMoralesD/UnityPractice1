using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Range(1, 1000)] int maxSpawned = 5;
    [SerializeField][Range(0.5f, 2 * 60f)] float tiempoEntreSpawns = 10;
    [SerializeField][Range(0.5f, 5f)] float tiempoAnimacion = 3;
    [SerializeField][Range(0f, 5f)] float timepoEsperaIA = 1f;
    public GameObject spawnableObject;
    void Start() { StartCoroutine(SpawnEnemies()); }
    void Update(){transform.eulerAngles+=Vector3.up*14*Time.deltaTime; }
    private IEnumerator SpawnEnemies()
    {
        int numSpawned = 0;
        while (maxSpawned >= ++numSpawned)
        {
            StartCoroutine(SpawnEnemyV2());
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }
    private IEnumerator SpawnEnemyV2()
    {
        GameObject spawnedObject = spawnedObject = GameObject.Instantiate(spawnableObject); //Creación
        Gravedad scriptGravedad = spawnedObject.GetComponent<Gravedad>();
        EnemigoIA scriptIA = spawnedObject.GetComponent<EnemigoIA>();
        CharacterController characterController = spawnedObject.GetComponent<CharacterController>();
        scriptIA.enabled = false;
        characterController.enabled = false;
        scriptGravedad.enabled = false;
        Vector3 rotacionInicial = spawnedObject.transform.eulerAngles = Vector3.up * Random.Range(0, 360); //Rotación inicial
        float alturaObjeto = getMaxHeight(spawnedObject);
        Vector3 alturaInicio = spawnableObject.transform.position = transform.position - Vector3.up * alturaObjeto;
        Vector3 alturaObjetivo = alturaInicio + Vector3.up * alturaObjeto;
        for (float tiempoTranscurrido = -Time.deltaTime; tiempoTranscurrido < tiempoAnimacion; tiempoTranscurrido+=Time.deltaTime)
        {
            if (tiempoTranscurrido > tiempoAnimacion)
                tiempoTranscurrido = tiempoAnimacion; //Para que no se pase nada de la posición final
            float fraccionTiempo = tiempoTranscurrido / tiempoAnimacion;
            spawnedObject.transform.position = Vector3.Lerp(alturaInicio, alturaObjetivo, fraccionTiempo);
            spawnedObject.transform.eulerAngles = Vector3.Lerp(rotacionInicial, rotacionInicial - Vector3.up * 720, fraccionTiempo);
            yield return null;
        }
        yield return new WaitForSeconds(timepoEsperaIA);
        scriptIA.enabled = true;
        characterController.enabled = true;
        scriptGravedad.enabled = true;
    }
    float getMaxHeight(GameObject parent)
    {
        Bounds total = new Bounds(parent.transform.position, Vector3.zero);
        foreach (MeshRenderer child in parent.transform.GetComponentsInChildren<MeshRenderer>())
            total.Encapsulate(child.bounds);
        return total.size.y;
    }
}

