using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace GMTK2021.Enemy
{
    public interface IPerceptionSense
    {
        void InitialiseSense();
        void RunSense();
    }

    public interface IEnemyInterestMarkers
    {
        Transform[] GetPointsOfInterest();
    }

    public interface ISenses
    {
        void InitialiseSenses();
    }

    public interface IWhiskers
    {

    }

    public class DroneSenses : MonoBehaviour, ISenses, IPausible, IEnemyInterestMarkers
    {
        public Transform[] pointsOfInterest;
        public Whisker[] whiskerPoints;

        public float repelStrength = 1;

        private IMovementController movementController;
        private IPerceptionSense[] senses;

        private RaycastHit hitResult;
        private bool isPaused = false;

        private Vector3 repelDirection;

        public void InitialiseSenses()
        {
            movementController = this.GetComponent<IMovementController>();
            senses = this.GetComponents<IPerceptionSense>();
        }

        public Transform[] GetPointsOfInterest()
        {
            return pointsOfInterest;
        }

        private void FixedUpdate()
        {
            if (isPaused) return;

            ShootWhiskers();
            foreach (IPerceptionSense sense in senses)
            {
                sense.RunSense();
            }
        }

        // ------------------------------------------------------------
        //                           WHISKERS
        // ------------------------------------------------------------

        private void ShootWhiskers()
        {
            foreach (Whisker whisker in whiskerPoints)
            {
                if (Physics.Raycast(whisker.point.position, whisker.point.forward, out hitResult, whisker.rayLength))
                {
                    movementController.AddForce(CalculateRepelForce(hitResult.point));
                }
            }
        }

        private Vector3 CalculateRepelForce(Vector3 whiskerPoint)
        {
            repelDirection = transform.position - whiskerPoint;
            return repelDirection.normalized * repelStrength;
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void UnPause()
        {
            isPaused = false;
        }


        // ------------------------------------------------------------
        //                           GIZMOS
        // ------------------------------------------------------------

        private void OnDrawGizmos()
        {
            if (pointsOfInterest == null) return;
            if (pointsOfInterest.Length == 0) return;

            foreach(Transform point in pointsOfInterest)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(point.position, 0.2f);
            }

            if (whiskerPoints.Length == 0) return;
            foreach (Whisker whisker in whiskerPoints)
            {
                if (whisker.point == null) return;
                Gizmos.DrawRay(whisker.point.position,  whisker.point.forward * whisker.rayLength);
            }
        }
    }

    [Serializable]
    public struct Whisker
    {
        public Transform point;
        public float rayLength;
    }
}
