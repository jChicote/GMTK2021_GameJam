using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy
{
    public interface IMovementController
    {
        void AddForce(Vector3 force);
        void SetMovement(Vector3 velocity);
        void DampenMovementVelocity(float dampenMultiplier);
        void SetAngularVelocity(Vector3 angular);
        float TotalSqrMagnitude();
        Vector3 GetVelocity();
    }

    public class EnemyMovementController : MonoBehaviour, IMovementController
    {
        [SerializeField] private Rigidbody enemyRb;
        private Vector3 totalVelocity = Vector3.zero;

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

        public void SetAngularVelocity(Vector3 angular)
        {
            enemyRb.angularVelocity = angular;
        }

        public void DampenMovementVelocity(float dampenMultiplier)
        {
            if (GetVelocity().magnitude <= 0) return;

            enemyRb.velocity *= dampenMultiplier;
        }

        public float TotalSqrMagnitude()
        {
            return totalVelocity.sqrMagnitude;
        }

        public Vector3 GetVelocity()
        {
            return enemyRb.velocity;
        }
    }

}