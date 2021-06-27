using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy.States
{
    public class DronePursuit : EnemyBaseState
    {
        // Interfaces
        private ITargeting targetingController;
        private IMovementController movementController;
        private IEnemySight enemySight;
        public float minimumApproachDistance = 8f;
        public float minimumHoverHeigh = 2f;
        public float orbitRadialDist = 6f;

        private SimpleTimer intervalTimer;
        private Vector3 lastTargetPosition = Vector3.zero;
        private Vector3 direction = Vector3.zero;
        private Vector3 currentVelocity = Vector3.zero;
        private Vector3 verticalDir;
        private Vector3 relativeHoverPos;
        private Vector3 eulerRotation = Vector3.zero;

        private Vector2 dronePos;
        private Vector2 targetPos;

        private float relativeHoverHeight = 0;


        public override void BeginState()
        {
            movementController = this.GetComponent<IMovementController>();
            targetingController = this.GetComponent<ITargeting>();
            enemySight = this.GetComponent<IEnemySight>();
            intervalTimer = new SimpleTimer(0.3f, Time.fixedDeltaTime);
        }

        private void FixedUpdate()
        {
            if (isPaused) return;

            DetermineDirection();
            CalculateTargetPosition();
            LevelDroneHeightToPlayer();
            FlyTowardsPlayer();
            
            CalculateRotation();

            movementController.SetMovement(currentVelocity);
        }

        private bool CheckForTargetPosition()
        {
            intervalTimer.TickTimer();

            if (intervalTimer.CheckTimeIsUp())
            {
                intervalTimer.ResetTimer();
                return true;
            }

            return false;
        }

        private void DetermineDirection()
        {
            direction = lastTargetPosition - transform.position;
        }

        private void CalculateTargetPosition()
        {
            if (!CheckForTargetPosition()) return;

            lastTargetPosition = targetingController.Target.position;
        }

        private void CalculateRotation()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.fixedDeltaTime);
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

        private float CalculatePlaneDistanceXZ()
        {
            dronePos = new Vector2(transform.position.x, transform.position.z);
            targetPos = new Vector2(lastTargetPosition.x, lastTargetPosition.z);

            return (dronePos - targetPos).sqrMagnitude;
        }

        private void FlyTowardsPlayer()
        {
            if (CalculatePlaneDistanceXZ() < minimumApproachDistance * minimumApproachDistance)
            {
                if (verticalDir != Vector3.zero)
                    currentVelocity = (verticalDir * 500).normalized * 1.2f;
                SlowDroneApproach();
                return;
            }

            currentVelocity = direction.normalized * 6f + verticalDir * 4f;
            currentVelocity.y = verticalDir == Vector3.zero ? 0 : currentVelocity.y;
        }

        private void SlowDroneApproach()
        {
            currentVelocity *= 0.93f;
        }

        private void LevelDroneHeightToPlayer()
        {
            relativeHoverHeight = transform.position.y + (lastTargetPosition.y + 3);
            relativeHoverPos = new Vector3(transform.position.x, lastTargetPosition.y + 7, transform.position.z);
            verticalDir = transform.position.y > relativeHoverPos.y ? Vector3.down : Vector3.up;

            if (transform.position.y > relativeHoverPos.y && transform.position.y < relativeHoverPos.y + 4)
                verticalDir = Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(relativeHoverPos, 1);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(lastTargetPosition, 1);
        }
    }

}