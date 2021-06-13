using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using GMTK2021.Weapons;

namespace GMTK2021.Player
{
    public interface IPlayerWeaponControl
    {
        void InitialiseWeapons();
        void FireWeapon();
        void HolsterWeapon();
    }

    public interface IPlayerWeaponAnimRig
    {
        void DisplayWeaponToEquipConfig();
        void EngageArmRigIK();
        void UnEquipWeapon();
    }

    public class PlayerWeaponControl : MonoBehaviour, IPlayerWeaponControl, IPlayerWeaponAnimRig, IPausible
    {
        public Animator animControl;
        public TwoBoneIKConstraint rightArm;
        public TwoBoneIKConstraint leftArm;
        public Rig armRig;

        public GameObject defaultBlaster;

        public IWeapon weapon;

        public Transform weaponEquipTransform;
        public GameObject weaponHolster;

        private float totalAnimWeight = 0;

        private bool isHolstered = true;
        private bool updateArmRig = false;
        private bool isPaused = false;

        public void InitialiseWeapons()
        {
            // Initialise default weapon
            GameObject spawnedInstance = Instantiate(defaultBlaster, weaponHolster.transform);
            weapon = spawnedInstance.GetComponent<IWeapon>();
            weapon.InitialiseWeapon();
            totalAnimWeight = 0;
            updateArmRig = true;
        }

        private void Update()
        {
            if (isPaused) return;
            if (updateArmRig)
            {
                Debug.LogWarning("Is running");
                armRig.weight = totalAnimWeight;
                updateArmRig = false;
            }
        }

        public void HolsterWeapon()
        {
            isHolstered = !isHolstered;
            
            if (isHolstered)
            {
                animControl.SetTrigger("onHolster");
                animControl.SetBool("isArmed", false);
                UnEquipWeapon();
            } else
            {
                animControl.SetTrigger("onArm");
                animControl.SetBool("isArmed", true);
            }
        }

        public void DisplayWeaponToEquipConfig()
        {
            weapon.MoveWeaponToTransform(weaponEquipTransform);
            weaponHolster.SetActive(false);
        }

        public void EngageArmRigIK()
        {
            rightArm.data.target = weapon.GetIKTargets().rightHand;
            leftArm.data.target = weapon.GetIKTargets().leftHand;
            updateArmRig = true;
            totalAnimWeight = 1f;
        }

        public void UnEquipWeapon() 
        {
            weapon.MoveWeaponToTransform(weaponHolster.transform);
            weaponHolster.SetActive(true);

            updateArmRig = true;
            totalAnimWeight = 0;
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