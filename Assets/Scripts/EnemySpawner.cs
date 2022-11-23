using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(1,1000)]
    public int maxSpawned = 5;
    [Range(0.5f,2*60f)]
    public float secondsBetweenSpawns = 2;
    private int numSpawned = 0;
    public GameObject spawnableObject;
    void Start()
    {
        StartCoroutine(SpawnEnemyV2());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator SpawnEnemyV2()
    {
        GameObject spawnedObject = GameObject.Instantiate(spawnableObject);
        spawnedObject.transform.eulerAngles = Vector3.up * Random.Range(0, 360);
        spawnableObject.transform.position = transform.position - Vector3.up*getMaxHeight(spawnedObject);
        
        yield return new WaitForSeconds(1);

    }
    private IEnumerator SpawnEnemies()
    {
        while (maxSpawned >= numSpawned)
        {
            GameObject spawnedObject = GameObject.Instantiate(spawnableObject);
            spawnedObject.transform.Translate(Vector3.forward);
            yield return new WaitForSeconds(10.4f);
        }
    }
    float getMaxHeight(GameObject parent)
    {
        Bounds total = new Bounds(parent.transform.position, Vector3.zero);
        foreach (MeshRenderer child in parent.transform.GetComponentsInChildren<MeshRenderer>())
            total.Encapsulate(child.bounds);
        return total.size.y;
    }
}

