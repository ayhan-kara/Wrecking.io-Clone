using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCanvas : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.localRotation * -Vector3.back,
            Camera.main.transform.localRotation * -Vector3.down);
    }
}
