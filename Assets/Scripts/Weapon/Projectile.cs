using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject particlePrefab;
    public Rigidbody projectileRB;
    public float speed;
    public float life;

    private float timeLeft;

    private void Awake()
    {
        projectileRB = this.GetComponent<Rigidbody>();
        timeLeft = life;
    }

    private void FixedUpdate()
    {
        projectileRB.velocity = transform.forward * speed;
        timeLeft -= Time.fixedDeltaTime;

        if (timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ApplyForce(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Interactive"))
        {
            //ApplyForce(collision);
        }

        Quaternion directionRot = Quaternion.FromToRotation(Vector3.forward, collision.GetContact(0).normal);
        ParticleSystem effect = Instantiate(particlePrefab, collision.GetContact(0).point, directionRot).GetComponent<ParticleSystem>();
        effect.Play();

        Destroy(effect.gameObject, 5);
        Destroy(gameObject);
    }
}
