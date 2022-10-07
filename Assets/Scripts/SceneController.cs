using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int totalTranscurrido = 0;
    public float fraccionSegundo = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hola mundo");
    }

    // Update is called once per frame
    void Update()
    {
        if ((fraccionSegundo += Time.deltaTime) >= 1)
        {
            Debug.Log(++totalTranscurrido);
            fraccionSegundo -= 1;
        }

    }
}
