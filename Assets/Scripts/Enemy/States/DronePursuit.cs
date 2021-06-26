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
        public float minimumApproachDistance = 4f;
        public float minimumHoverHeigh = 2f;

        private SimpleTimer intervalTimer;
        private Vector3 lastTargetPosition = Vector3.zero;
        private Vector3 direction = Vector3.zero;
        private Vector3 currentVelocity = Vector3.zero;

        public override void BeginState()
        {
            movementController = this.GetComponent<IMovementController>();
            targetingController = this.GetComponent<ITargeting>();
            intervalTimer = new SimpleTimer(1, Time.fixedDeltaTime);
        }

        private void FixedUpdate()
        {
            if (isPaused) return;

            DetermineDirection();
            CalculateTargetPosition();
            FlyTowardsPlayer();
            CalculateRotation();
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
        }

        private void FlyTowardsPlayer()
        {
            if ((transform.position - lastTargetPosition).sqrMagnitude < minimumApproachDistance * minimumApproachDistance) return;

            currentVelocity = direction.normalized * 6f;
            movementController.SetMovement(currentVelocity);
        }

        private void HoverOnNearGround()
        {

        }



        private void FlyAroundPlayer()
        {

        }
    }

}