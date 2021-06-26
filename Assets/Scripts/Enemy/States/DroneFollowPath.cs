using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy.States
{
    public class DroneFollowPath : EnemyBaseState
    {
        // Interfaces 
        private IMovementController movementController;
        private IEnemySight enemySight;
        private IStateManager stateManager;

        // Fields
        private Transform[] pointsOfInterest;
        private SimpleTimer simpleTimer;
        private Transform droneTransform;
        private Transform primaryPointOfInterest;
        private Vector3 velocity;
        private Vector3 direction;

        private float distToPoint = 0;
        private float startingSlowdownDist = 0;

        public override void BeginState()
        {
            IEnemyInterestMarkers interestMarkers = this.GetComponent<IEnemyInterestMarkers>();
            pointsOfInterest = interestMarkers.GetPointsOfInterest();
            movementController = this.GetComponent<IMovementController>();
            enemySight = this.GetComponent<IEnemySight>();
            stateManager = this.GetComponent<IStateManager>();

            simpleTimer = new SimpleTimer(10, Time.deltaTime);
            droneTransform = transform;
            SelectPointOfInterest();
        }

        private void FixedUpdate()
        {
            if (isPaused) return;

            if (!enemySight.IsPercievable)
            {
                stateManager.AddState<DronePursuit>();
            }

            CalculateDistanceToPoint();
            CalculateRotation();
            MoveToDestination();
            simpleTimer.TickTimer();

            if (simpleTimer.CheckTimeIsUp())
            {
                SelectPointOfInterest();
                simpleTimer.ResetTimer();
                return;
            }
        }

        private void SelectPointOfInterest()
        {
            int selection = Random.Range(0, pointsOfInterest.Length);

            while (primaryPointOfInterest == pointsOfInterest[selection])
            {
                selection = Random.Range(0, pointsOfInterest.Length);
            }

            primaryPointOfInterest = pointsOfInterest[selection];
            startingSlowdownDist = Vector3.Magnitude(primaryPointOfInterest.position - droneTransform.position) * 0.3f;
        }

        private void CalculateDistanceToPoint()
        {
            distToPoint = Vector3.Magnitude(primaryPointOfInterest.position - droneTransform.position);
        }

        private void CalculateRotation()
        {
            direction = primaryPointOfInterest.position - droneTransform.position;
            droneTransform.rotation = Quaternion.Slerp(droneTransform.rotation, Quaternion.LookRotation(direction), Time.fixedDeltaTime);
        }

        private float VelocityProximityModifier()
        {
            return distToPoint / startingSlowdownDist;
        }

        private void SlowDownOnApproach()
        {
            if (distToPoint < startingSlowdownDist)
                velocity *= distToPoint < 0.1f ? 0 : VelocityProximityModifier();
        }

        private void MoveToDestination()
        {
            velocity = direction.normalized * 4f;
            SlowDownOnApproach();
            movementController.SetMovement(velocity);

            //Debug.DrawLine(transform.position, dir * 2, Color.yellow);
        }

        // ---------------------------------------------------------
        //                  TOOL GIZMOS
        // ---------------------------------------------------------

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(primaryPointOfInterest.position, Vector3.one);
        }
    }
}
