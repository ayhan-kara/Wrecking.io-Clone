using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Private-Public Variables
    [Header("Movement")]
    public float forwardSpeed;
    public float turnSpeed;

    Vector3 destination;
    public Transform playerDestination;

    private bool isGrounded = true;
    public bool isBonus = false;
    #endregion

    #region Monobehaviour
    void Update()
    {
        if (!GameManager.Instance.isStarted)
            return;
        Grounded();
        Fail();
        if (!isGrounded)
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
        EnemyCarMovement();
    }
    #endregion


    #region AI Movement
    void EnemyCarMovement()
    {

        if ((playerDestination.position - transform.position).magnitude < 10)
        {
            //spin
            transform.RotateAroundLocal(transform.up, 10 * turnSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation
              , Quaternion.LookRotation(playerDestination.position - transform.position)
              , 3f * Time.deltaTime);
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
            EnemyCarsManager.Instance.enemyCars.Remove(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(.3f);
        isGrounded = true;
    }
    #endregion

}
