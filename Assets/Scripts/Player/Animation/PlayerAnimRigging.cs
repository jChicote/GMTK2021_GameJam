using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace GMTK2021.Player.Animation
{
    public interface IPlayerAnimationRigging
    {
        void InitialiseAnimationRigging();
        void ModifyRigForEquipMotion();
        void ModifyRigIKForUnequipMotion();
        void OnAnimationRigArmed();
        void OnAnimationRigHolstered();
    }

    public class PlayerAnimRigging : MonoBehaviour, IPlayerAnimationRigging, IPausible
    {
        public TwoBoneIKConstraint leftArm;
        public Rig armRig;
        public Rig weaponPoseRig;
        public MultiAimConstraint weaponAimConstraint;
        public MultiPositionConstraint weaponPositionConstraint;
        public MultiParentConstraint weaponParentConstraint;
        private WeightedTransformArray m_sourceObjects;
        public AnimState animState;

        private float weaponPoseWeight = 1;
        private float leftArmWeight = 1;
        private float armRigWeight = 0;

        private bool isPaused = false;

        public void InitialiseAnimationRigging()
        {

        }

        private void Update()
        {
            if (isPaused) return;

            UpdateArmRig();
            UpdateWeaponSourceData();
        }

        private void UpdateArmRig()
        {
            armRig.weight = armRigWeight;
        }

        private void UpdateWeaponSourceData()
        {
            leftArm.weight = leftArmWeight;
            weaponAimConstraint.weight = weaponPoseWeight;
            weaponPositionConstraint.weight = weaponPoseWeight;

            m_sourceObjects = weaponParentConstraint.data.sourceObjects;
            m_sourceObjects.SetWeight(0, animState == AnimState.Armed ? 1 : 0);
            m_sourceObjects.SetWeight(1, animState == AnimState.Holster ? 1 : 0);
            m_sourceObjects.SetWeight(2, animState == AnimState.Unequipping || animState == AnimState.Equipping ? 1 : 0);
            weaponParentConstraint.data.sourceObjects = m_sourceObjects;
        }

        public void ModifyRigIKForUnequipMotion()
        {
            print(" Unequiip Motion active");
            animState = AnimState.Unequipping;
            leftArmWeight = 0;
            weaponPoseWeight = 0;

            print("Unequiip Motion active " + animState + " , " + leftArmWeight + " , " + weaponPoseWeight);
        }

        public void ModifyRigForEquipMotion()
        {
            animState = AnimState.Equipping;
            leftArmWeight = 1;
            weaponPoseWeight = 1;
            print("Equipping Motion active " + animState + " , " + leftArmWeight + " , " + weaponPoseWeight);
        }

        public void OnAnimationRigArmed()
        {
            animState = AnimState.Armed;
            armRigWeight = 1f;
        }

        public void OnAnimationRigHolstered()
        {
            animState = AnimState.Holster;
            armRigWeight = 0;
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void UnPause()
        {
            isPaused = false;
        }

        public enum AnimState
        {
            Holster,
            Armed,
            Unequipping,
            Equipping
        }
    }
}
