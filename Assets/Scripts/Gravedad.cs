using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravedad : MonoBehaviour
{
    private static float CTEGRAV = -20f;
    [Range(-30f, -5f)]
    public float gravity = CTEGRAV;
    Vector3 movVer;
    private CharacterController charCont;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<CharacterController>(out charCont);
    }

    // Update is called once per frame
    void Update()
    {
        movVer.y += gravity * Time.deltaTime;
        charCont.Move(movVer * Time.deltaTime);
    }
}
