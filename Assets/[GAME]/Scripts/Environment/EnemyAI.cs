using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject driftPivotPoint;

    [SerializeField]
    private float driftSpeed = 5, carSpeed = 10;

    private bool inAir;
    private bool landing;
    private bool isDrifting;


    private Vector3 centrePos, playerPos, lookPos, destination;

    private Rigidbody m_Rigidbody;
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (landing) AirToGround();
        if (isDrifting) transform.RotateAround(driftPivotPoint.transform.position, Vector3.up, driftSpeed);
        if (!inAir)
        {
            if (!isDrifting)
                transform.rotation = Quaternion.Slerp(transform.rotation
                                      , Quaternion.LookRotation((playerPos + destination) - transform.position)
                                      , 1f * Time.deltaTime);

            /* Move at Player*/
            transform.position += transform.right * carSpeed * Time.deltaTime;
        }
    }
    void Update()
    {

        centrePos = GameObject.FindGameObjectWithTag("Center").transform.position;
        if (GameObject.FindGameObjectWithTag("PlayerCar"))
            playerPos = GameObject.FindGameObjectWithTag("PlayerCar").GetComponentInChildren<PlayerController>().transform.position;

        destination = (centrePos - playerPos).normalized * 7;

        if ((playerPos - transform.position).magnitude < 15)
        {
            isDrifting = true;
        }
        else isDrifting = false;

        if (transform.position.y < -10)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }

        if (transform.position.y > 0.5f && !landing)
        {
            inAir = true;
        }

    }

    private void AirToGround()
    {
        Vector3 carPos = transform.position;
        Vector3 targetPos = new Vector3(carPos.x, -0.05f, carPos.z);
        Quaternion targetRot = new Quaternion(0, transform.localRotation.y, 0, transform.localRotation.w);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, 0.5f);
    }

    private IEnumerator AirToGroundCounter()
    {
        inAir = false;
        landing = true;
        yield return new WaitForSeconds(0.3f);
        landing = false;
    }
}
