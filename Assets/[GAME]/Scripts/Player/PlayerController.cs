using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    #region Private-Public Variables
    [Header ("Movement")]
    public float forwardSpeed;
    public float turnSpeed;

    private float startMousePosition;
    private float lastMousePosition;

    private bool isGrounded = true;
    #endregion

    #region Monobehaviour
    void Start()
    {

    }

    void Update()
    {
        SwipeMovement();
        Grounded();
        if(!isGrounded)
        {
            Falling();
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y);
        }
        transform.position += transform.right * forwardSpeed * Time.deltaTime;
    }
    #endregion

    #region Movement
    void SwipeMovement()
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
                //transform.RotateAround(transform.position, Vector3.up, 10 * turnSpeed * Time.deltaTime);
                transform.RotateAroundLocal(transform.up, 10 * turnSpeed * Time.deltaTime);
            }
            else if (lastMousePosition < startMousePosition)
            {
                transform.RotateAroundLocal(-transform.up, 10 * turnSpeed * Time.deltaTime);
                //transform.RotateAround(transform.position, -Vector3.up, 10 * turnSpeed * Time.deltaTime);
            }
        }
        else
        {
            lastMousePosition = lastMousePosition;
        }
    }
    #endregion

    #region CheckGorunded
    void Grounded()
    {
        const float posY = 1.5f;
        if (transform.position.y > posY)
        {
            //Debug.Break();
            Debug.Log("Not Ground");
            isGrounded = false;
        }
    }

    void Falling()
    {
        const float posY = 1.5f;
        Quaternion pos = new Quaternion(0, 0, 0, transform.rotation.w);
        if (transform.position.y < posY)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, pos, .1f);
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(.3f);
        isGrounded = true;
    }
    #endregion
}
