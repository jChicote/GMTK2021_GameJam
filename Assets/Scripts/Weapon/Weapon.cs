using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Weapons
{
    public interface IWeapon
    {
        void InitialiseWeapon();
        void FireWeapon();
        void ReloadWeapon();
        void MoveWeaponToTransform(Transform newTransform);
        WeaponIKTargets GetIKTargets();
    }

    public class Weapon : MonoBehaviour, IWeapon, IPausible
    {
        public Transform firingPoint;
        public GameObject projectile;
        public WeaponIKTargets ikTargets;

        protected bool isPaused = false;

        public virtual void InitialiseWeapon() { 
        }
        public virtual void FireWeapon() { }

        public virtual void ReloadWeapon() { }

        public virtual void MoveWeaponToTransform(Transform newTransform)
        {
            transform.parent = newTransform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public WeaponIKTargets GetIKTargets()
        {
            return ikTargets;
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void UnPause()
        {
            isPaused = false;
        }
    }

    [System.Serializable]
    public struct WeaponIKTargets
    {
        public Transform leftHand;
        public Transform rightHand;
    }
}
