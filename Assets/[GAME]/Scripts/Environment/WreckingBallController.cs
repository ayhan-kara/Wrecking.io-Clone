using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBallController : MonoBehaviour
{
    public float hitPower;

    public Rigidbody rb;

    [SerializeField] TrailRenderer ropeTrail;

    private void Start()
    {
        ropeTrail.emitting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyCar"))
        {
            float velocity = rb.velocity.magnitude;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * velocity * hitPower);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * velocity * hitPower / 4);

        }
    }
}
