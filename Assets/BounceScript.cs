using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    public float bounce_force;
    void OnCollisionEnter2D(Collision2D collision)
    {
        bounce_force = 80f * (collision.relativeVelocity * transform.up).magnitude;
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(bounce_force * transform.up);
    }
}
