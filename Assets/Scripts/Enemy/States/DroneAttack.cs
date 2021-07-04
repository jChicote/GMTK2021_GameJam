using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy.States
{
    public class DroneAttack : EnemyBaseState
    {
        public float angularDist = 4;
        public float macAngularDist = 7;

        private ITargeting targetingController;
        private IMovementController movementController;
        private IWeaponController weaponController;

        private SimpleTimer timer;
        private Vector3 currentVelocity;
        private Vector3 eulerRotation = Vector3.zero;
        private const float pi = 3.14159265f;
        private const float angularDuration = 5f;

        public override void BeginState()
        {
            targetingController = this.GetComponent<ITargeting>();
            movementController = this.GetComponent<IMovementController>();
            weaponController = this.GetComponent<IWeaponController>();

            timer = new SimpleTimer(angularDuration, Time.fixedDeltaTime);
            currentVelocity = movementController.GetVelocity();
        }

        private void FixedUpdate()
        {
            if (isPaused) return;

            targetingController.GetTargetPointFromPlane();
            CalculateRotation();
            ManeuverAroundPlayer();
        }

        private void ManeuverAroundPlayer()
        {
            //Vector3 angular = Vector3.Cross(targetingController.Target.position, Vector3.up);
            float sqrAngularDist = angularDist * angularDist;

            if (targetingController.GetTargetSqrDistance() < sqrAngularDist * 0.95)
            {
                print("is repelling");
                currentVelocity = CalcualteAttractionForce();
            } else if (targetingController.GetTargetSqrDistance() > (sqrAngularDist + sqrAngularDist * 0.2)) 
            {
                print("is attracting");
                currentVelocity = CalculateRepelForce();
            } else
            {
                print("is slowing");
                currentVelocity = Vector3.zero;
            }
               
            //print(currentVelocity);
            //Debug.DrawLine(transform.position, angular);

            if (currentVelocity != Vector3.zero)
            {
                movementController.SetMovement(currentVelocity);
            } else
            {
                movementController.DampenMovementVelocity(0.95f);
            }
            
            //transform.RotateAround(targetingController.Target.position, Vector3.up, 20 * Time.fixedDeltaTime);
        }

        private void CalculateRotation()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetingController.GetTargetDirection()), Time.fixedDeltaTime);
            ClampPitchRotation();
        }

        private void ClampPitchRotation()
        {
            if (transform.localEulerAngles.x > 10 && transform.localEulerAngles.x < (360 - 10))
            {
                eulerRotation = transform.localEulerAngles;
                eulerRotation.x = eulerRotation.x > 270 ? (360 - 10) : 10;
                transform.localEulerAngles = eulerRotation;
            }
        }

        private Vector3 CalculateRepelForce()
        {
            float sqrAngularDist = angularDist * angularDist;
            //if (targetingController.GetTargetSqrDistance() > sqrAngularDist) return Vector3.zero;

            Vector3 repel = (sqrAngularDist - (targetingController.GetTargetSqrDistance() / sqrAngularDist)) * targetingController.GetOppositeTargetDirectionFromPlane().normalized;
            return repel.normalized * 10;
        }

        private Vector3 CalcualteAttractionForce()
        {
            float sqrAngularDist = angularDist * angularDist;
            float maxSqrAngularDist = macAngularDist * macAngularDist;
           
            //if (targetingController.GetTargetSqrDistance() < sqrAngularDist) return Vector3.zero;

            Vector3 attract = targetingController.GetTargetDirection().normalized;//((targetingController.GetTargetSqrDistance() - sqrAngularDist) / (maxSqrAngularDist - sqrAngularDist)) * targetingController.GetTargetDirection().normalized;
            return attract.normalized * 10;
        }

        private void CheckIfCanAttack()
        {

        }

        private void CheckTargetVisibility()
        {

        }

        private void CheckOrbitalPlaneIsClear()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, angularDist * angularDist);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, (angularDist * angularDist) + (angularDist * angularDist) * 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, macAngularDist * macAngularDist);
        }
    }

}