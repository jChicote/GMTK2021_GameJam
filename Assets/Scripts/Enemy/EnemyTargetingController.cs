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
            Gizmos.DrawSphere(targetPointer.position, 0.3f);
        }
    }
}
