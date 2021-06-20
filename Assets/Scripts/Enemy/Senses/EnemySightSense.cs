using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy
{

    public interface IEnemySight
    {
        bool IsPercievable { get; }
        bool CanSeeTarget { get; }
    }

    public class EnemySightSense : MonoBehaviour, IEnemySight, IPerceptionSense
    {
        public Transform target;
        public Transform eye;
        public float detectionRadius;

        private bool isPercievable = false;
        private bool canSeeTarget = false;

        private RaycastHit hitResult;

        // Accessors
        public bool IsPercievable => isPercievable;
        public bool CanSeeTarget => canSeeTarget;

        public void InitialiseSense()
        {
           
        }

        public void RunSense()
        {
            //print(Vector3.Dot(transform.TransformDirection(Vector3.forward), target.position - transform.position));

            isPercievable = CheckIfWithinDetectionRange() && CheckIfWithinView();

            if (!isPercievable) return;

            SearchForTarget();
        }

        public bool CheckIfWithinDetectionRange()
        {
            return (transform.position - target.position).sqrMagnitude < (detectionRadius * detectionRadius);
        }

        public bool CheckIfWithinView()
        {
            return Vector3.Dot(transform.TransformDirection(Vector3.forward), target.position - transform.position) > 0;
        }

        public void SearchForTarget()
        {
            Debug.DrawRay(eye.position, target.position - transform.position, Color.cyan);
            if (Physics.Raycast(eye.position, target.position - transform.position, out hitResult, detectionRadius)) {
                canSeeTarget = hitResult.collider.CompareTag("Player");
                //print(hitResult.collider.name);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            if (target == null) return;

            if (!canSeeTarget) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
