using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy
{
    public interface ITargeting
    {
        void InitialiseTargetingController();
        bool IsTargeting { get; set; }
        Transform Target { get; }
        float GetTargetSqrDistance();
        Vector3 GetTargetDirection();
        Vector3 GetTargetPointFromPlane();
        Vector3 GetOppositeTargetDirectionFromPlane();
        Vector3 GetTargetDirectionFromPlane();
    }

    public class EnemyTargetingController : MonoBehaviour, ITargeting, IPausible
    {
        // Inspector Accessible Fields
        [SerializeField] private Transform mainTarget;
        [SerializeField] private Transform targetPointer;

        private IEnemySight enemySight;

        // Fields
        private bool isPaused = false;
        private bool isTargetting = false;

        private Vector3 relativePosition;

        // Accessors
        public bool IsTargeting
        {
            get { return isTargetting; }
            set { isTargetting = value; }
        }

        public Transform Target => mainTarget;

        public void InitialiseTargetingController()
        {
            enemySight = this.GetComponent<IEnemySight>();
        }

        private void FixedUpdate()
        {
            if (isPaused) return;
            if (!enemySight.IsPercievable) return;
            targetPointer.position = mainTarget.position;
        }

        /// <summary>
        /// Calculates distance of the target through the sqrmagnitude between
        /// both points.
        /// </summary>
        public float GetTargetSqrDistance()
        {
            return (mainTarget.position - transform.position).sqrMagnitude;
        }

        public float GetRelativeTargetSqrDistance()
        {
            return (relativePosition - transform.position).sqrMagnitude;
        }

        public Vector3 GetTargetPointFromPlane()
        {
            relativePosition = mainTarget.position;
            relativePosition.y = transform.position.y;
            return relativePosition;
        }

        public Vector3 GetOppositeTargetDirectionFromPlane()
        {
            return transform.position - relativePosition;
        }

        public Vector3 GetTargetDirectionFromPlane()
        {
            return relativePosition - transform.position;
        }

        public Vector3 GetTargetDirection()
        {
            return targetPointer.position - transform.position;
        }

        public Vector3 GetOppositeTargetDirection()
        {
            return transform.position - targetPointer.position;
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void UnPause()
        {
            isPaused = false;
        }

        // ---------------------------------------------------------
        //                  TOOL GIZMOS
        // ---------------------------------------------------------

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 2f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(mainTarget.position, 1f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(relativePosition, 1f);
        }
    }
}
