using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy
{
    public interface ITargeting
    {
        bool IsTargeting { get; set; }
    }

    public class EnemyTargetingController : MonoBehaviour, ITargeting, IPausible
    {
        // Inspector Accessible Fields
        [SerializeField] private Transform mainTarget;
        [SerializeField] private Transform targetPointer;

        // Fields
        private bool isPaused = false;
        private bool isTargetting = false;

        // Accessors
        public bool IsTargeting
        {
            get { return isTargetting; }
            set { isTargetting = value; }
        }

        private void FixedUpdate()
        {
            if (isPaused) return;
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
