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

    public interface IPlayerWeaponAnimRig
    {
        void DisplayWeaponToEquipConfig();
        void EngageArmRigIK();
        void UnEquipWeapon();
    }

    public class PlayerWeaponControl : MonoBehaviour, IPlayerWeaponControl, IPlayerWeaponAnimRig, IPausible
    {
        public Animator animControl;
        //public RigBuilder rigBuilder;
        //public TwoBoneIKConstraint rightArm;
        //public TwoBoneIKConstraint leftArm;
        //public Rig armRig;
        //public MultiAimConstraint aimConstraint;
        //public MultiParentConstraint m_Parent;
        //public WeightedTransformArray m_sourceObjects;

        public GameObject defaultBlaster;

        private ITargetingSystem targetingSystem;
        private IWeaponRigHandler weaponRigHandler;
        //private IPlayerAnimationRigging animRigging;
        public IWeapon weapon;

        public Transform weaponEquipTransform;
        public Transform modelTransform;
        public GameObject weaponHolster;
        
        GameObject spawnedInstance;
        //private float totalAnimWeight = 0;

        private bool isHolstered = true;
        //private bool updateArmRig = false;
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
            //totalAnimWeight = 0;
            //updateArmRig = true;
        }

        private void Update()
        {
            if (isPaused) return;
            /*if (updateArmRig)
            {
                Debug.LogWarning("Is running");
                armRig.weight = totalAnimWeight;
                updateArmRig = false;
            }*/


            /*m_sourceObjects = m_Parent.data.sourceObjects;
            m_sourceObjects.SetWeight(0, animState == AnimState.Armed ? 1 : 0);
            m_sourceObjects.SetWeight(1, animState == AnimState.Holster ? 1 : 0);
            m_Parent.data.sourceObjects = m_sourceObjects;*/

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

        public void DisplayWeaponToEquipConfig()
        {
            //weapon.MoveWeaponToTransform(weaponEquipTransform);
            weaponHolster.SetActive(false);
        }

        public void EngageArmRigIK()
        {
            //rightArm.data.target = weapon.GetIKTargets().rightHand;
            //leftArm.data.target = weapon.GetIKTargets().leftHand;
            /*animState = AnimState.Armed;
            updateArmRig = true;
            totalAnimWeight = 1f;*/
        }

        public void UnEquipWeapon() 
        {
            //weapon.MoveWeaponToTransform(weaponHolster.transform);
            /*weaponHolster.SetActive(true);
            animState = AnimState.Holster;
            updateArmRig = true;
            totalAnimWeight = 0;*/
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