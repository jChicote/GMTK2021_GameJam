using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy
{
    public interface ILocalRotator
    {
        void SetLocalRotation(float angleRotation);
    }

    public class DroneLocalRotator : MonoBehaviour, ILocalRotator
    {
        public Transform modelTransform;

        public void SetLocalRotation(float angleRotation)
        {
            modelTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angleRotation));
        }
    }

}