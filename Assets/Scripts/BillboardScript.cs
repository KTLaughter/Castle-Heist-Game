using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public Camera mainCam;

    void Update()
    {
        transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.back, mainCam.transform.rotation * Vector3.up);
    }
}
