using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private-Public Variables
    [Header ("Movement")]
    public float forwardSpeed;
    public float turnSpeed;

    private float startMousePosition;
    private float lastMousePosition;

    private bool isGrounded = true;
    public bool isBonus = false;
    #endregion

    #region Monobehaviour

    void Update()
    {
        if (!GameManager.Instance.isStarted)
            return;
        SwipeMovement();
        Grounded();
        Fail();
        if(!isGrounded)
        {
            Falling();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isStarted)
            return;
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
                transform.RotateAroundLocal(transform.up, 10 * turnSpeed * Time.deltaTime);
            }
            else if (lastMousePosition < startMousePosition)
            {
                transform.RotateAroundLocal(-transform.up, 10 * turnSpeed * Time.deltaTime);
            }
        }
    }
    #endregion

    #region CheckGorunded
    void Grounded()
    {
        const float posY = 1.5f;
        if (transform.position.y > posY)
        {
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

    void Fail()
    {
        const float posY = .5f;
        if (transform.position.y < posY)
        {
            Debug.LogError("Fail");
            Destroy(transform.parent.gameObject);
            //fail panel active
            GameManager.Instance.FailGame();
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(.3f);
        isGrounded = true;
    }
    #endregion

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BonusBox"))
        {
            Destroy(collision.gameObject);
            isBonus = true;
        }
    }
    #endregion

    #region Bonus Wrecking
    public void BonusWrecking()
    {
        StartCoroutine(Bonus());
    }

    IEnumerator Bonus()
    {
        yield return new WaitForSeconds(5f);
        isBonus = false;
    }
    #endregion
}
