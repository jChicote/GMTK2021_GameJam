using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GMTK2021.UI;

namespace GMTK2021.Player
{
    public interface ITargetingSystem
    {
        void InitialiseTargetingSystem();
        void RunTargetingSystem();
    }


    public class PlayerTargetingSystem : MonoBehaviour, ITargetingSystem
    {
        private Vector2 targetUIRecticlePosition;
        private Vector3 targetPosition;
        private IAimHUD aimHud;
        public LayerMask layerMask;

        public Transform targetPoint;

        public void InitialiseTargetingSystem()
        {
            GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UI");
            aimHud = uiObjects.Where(x => x.GetComponent<IAimHUD>() != null).First().GetComponent<IAimHUD>();
        }

        public void RunTargetingSystem()
        {
            DetermineAimPosition();
        }

        public void DetermineAimPosition()
        {
            targetUIRecticlePosition = aimHud.GetCrossHairRecticlePosition();
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(targetUIRecticlePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50f, layerMask))
            {
                //draw invisible ray cast/vector
                Debug.DrawLine(ray.origin, hit.point);
                targetPosition = hit.point;
                targetPoint.position = hit.point;
                return;
            }

            targetPosition = ray.origin + ray.direction * 50f;
            targetPoint.position = targetPosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(targetPosition, 0.3f);
        }
    }

}