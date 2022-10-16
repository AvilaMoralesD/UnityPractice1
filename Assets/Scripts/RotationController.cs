using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log((transform.rotation.eulerAngles.x.ToString("(000.00), ")+
transform.rotation.eulerAngles.y.ToString("(000.00), ")+
transform.rotation.eulerAngles.z.ToString("(000.00)")));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
