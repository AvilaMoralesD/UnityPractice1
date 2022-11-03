using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorCabeza : MonoBehaviour
{
    GameObject target;
    Vector3 originPos, tgtPos, mov, palantePadre, palante;
    public float areaDistance = 5f;
    public float angleSpeed = 3f;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        originPos = transform.position;
        tgtPos = target.transform.position;
        tgtPos.y += 1.8f; //Ajuste altura ojos
        palante = transform.forward;
        palantePadre = transform.parent.forward;

        RaycastHit hit;
        Physics.Raycast(originPos, tgtPos - originPos, out hit, areaDistance);
        //Debug.DrawLine(originPos, tgtPos, Color.green, 0);

        if (hit.collider != null && hit.collider.gameObject == target)
        {
            mov = Vector3.RotateTowards(palante, tgtPos - originPos, angleSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(mov);
        }
        else if (Vector3.Angle(palante, palantePadre) > 0.01f)
        {
            mov = Vector3.RotateTowards(palante, palantePadre, angleSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(mov);
        }
    }
}
