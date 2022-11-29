using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed;
    public float turnSpeed;

    private float startMousePosition;
    private float lastMousePosition;

    private bool isMove = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition.x;

            lastMousePosition = startMousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            lastMousePosition = Input.mousePosition.x;
            if (lastMousePosition > startMousePosition)
            {
                Debug.Log("Right");
                transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            }
            else if (lastMousePosition < startMousePosition)
            {
                Debug.LogWarning("Left");
                transform.Rotate(-Vector3.up * turnSpeed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.right * forwardSpeed * Time.deltaTime;
    }
}
