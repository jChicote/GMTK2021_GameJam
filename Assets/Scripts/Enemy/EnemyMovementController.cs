using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy
{
    public interface IMovementController
    {
        void AddForce(Vector3 force);
        void SetMovement(Vector3 velocity);
    }

    public class EnemyMovementController : MonoBehaviour, IMovementController
    {
        [SerializeField] private Rigidbody enemyRb;
        private Vector3 totalVelocity;

        public void AddForce(Vector3 force)
        {
            totalVelocity += force;
        }

        public void SetMovement(Vector3 velocity)
        {
            totalVelocity += velocity;
            enemyRb.velocity = totalVelocity;
            totalVelocity = Vector3.zero;
        }
    }

}