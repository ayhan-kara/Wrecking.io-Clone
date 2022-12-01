using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWreckingBallController : MonoBehaviour
{
    public float hitPower;

    public Rigidbody rb;

    [SerializeField] TrailRenderer ropeTrail;
    [SerializeField] LineRenderer ropeLine;
    [SerializeField] EnemyController enemyController;
    [SerializeField] Transform bonusPos;
    [SerializeField] Transform carPos;


    private void Start()
    {
        ropeTrail.emitting = false;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isStarted)
            return;
        TurnAround();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerCar"))
        {
            float velocity = rb.velocity.magnitude;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * velocity * hitPower * 2);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * velocity * hitPower / 4);

        }
    }

    void TurnAround()
    {
        ConfigurableJoint joint = GetComponent<ConfigurableJoint>();

        if (enemyController.isBonus)
        {
            Destroy(joint);
            transform.RotateAround(carPos.position, Vector3.up, 10f);
            ropeTrail.emitting = true;
            ropeLine.enabled = false;
            enemyController.BonusWrecking();
        }
        else
        {
            ropeTrail.enabled = false;
            ropeLine.enabled = true;
            if (!transform.GetComponent<ConfigurableJoint>())
                transform.AddComponent<ConfigurableJoint>().connectedBody = carPos.GetComponent<Rigidbody>();

                ConfigurableJoint newJoint = GetComponent<ConfigurableJoint>();

                newJoint.xMotion = ConfigurableJointMotion.Limited;
                newJoint.yMotion = ConfigurableJointMotion.Limited;
                newJoint.zMotion = ConfigurableJointMotion.Limited;
                newJoint.autoConfigureConnectedAnchor = false;
                newJoint.connectedAnchor = new Vector3(-1.76f, 0, 0);
                newJoint.anchor = new Vector3(0, 0, 0);

                SoftJointLimit limit = new SoftJointLimit();
                limit.limit = 1;
                newJoint.linearLimit = limit;
        }
    }
}
