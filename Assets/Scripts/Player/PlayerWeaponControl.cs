using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using GMTK2021.Weapons;
using GMTK2021.Player.Animation;

namespace GMTK2021.Player
{
    public interface IPlayerWeaponControl
    {
        void InitialiseWeapons();
        void FireWeapon();
        void HolsterWeapon();
    }


    public class PlayerWeaponControl : MonoBehaviour, IPlayerWeaponControl, IPausible
    {
        // Inspector Accessible Fields
        [SerializeField] private Animator animControl;
        [SerializeField] private GameObject defaultBlaster;

        // Interfaces
        private ITargetingSystem targetingSystem;
        private IWeaponRigHandler weaponRigHandler;
        public IWeapon weapon;

        public Transform weaponEquipTransform;
        public Transform modelTransform;
        public GameObject weaponHolster;
        
        GameObject spawnedInstance;

        private bool isHolstered = true;
        private bool isPaused = false;

        public void InitialiseWeapons()
        {
            // Initialise default weapon
            spawnedInstance = Instantiate(defaultBlaster, modelTransform);
            spawnedInstance.transform.localPosition = Vector3.zero;
            weapon = spawnedInstance.GetComponent<IWeapon>();
            targetingSystem = this.GetComponent<ITargetingSystem>();
            targetingSystem.InitialiseTargetingSystem();

            weaponRigHandler = modelTransform.GetComponent<IWeaponRigHandler>();
            weaponRigHandler.SetTargetHandPoints(weapon.GetIKTargets());
            weapon.InitialiseWeapon();
        }

        private void Update()
        {
            if (isPaused) return;

            targetingSystem.RunTargetingSystem();
        }

        public void HolsterWeapon()
        {
            isHolstered = !isHolstered;
            
            if (isHolstered)
            {
                animControl.SetTrigger("onHolster");
                animControl.SetBool("isArmed", false);
                
            } else
            {
                animControl.SetTrigger("onArm");
                animControl.SetBool("isArmed", true);
            }
        }

        public void FireWeapon()
        {
            if (isHolstered) return;

            weapon.FireWeapon();
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        public void UnPause()
        {
            throw new System.NotImplementedException();
        }
    }
}