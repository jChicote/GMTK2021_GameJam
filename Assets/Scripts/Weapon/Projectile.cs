using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody projectileRB;
    public float speed;

    private void Awake()
    {
        projectileRB = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        projectileRB.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        Destroy(gameObject);
    }
}
