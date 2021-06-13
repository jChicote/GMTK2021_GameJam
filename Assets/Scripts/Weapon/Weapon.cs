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
    }

    public class Weapon : MonoBehaviour, IWeapon, IPausible
    {
        public Transform firingPoint;
        public GameObject projectile;

        protected bool isPaused = false;

        public virtual void InitialiseWeapon() { 
        }
        public virtual void FireWeapon() { }

        public virtual void ReloadWeapon() { }

        public void Pause()
        {
            isPaused = true;
        }

        public void UnPause()
        {
            isPaused = false;
        }
    }
}
