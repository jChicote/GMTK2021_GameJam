using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK2021.Weapons;

namespace GMTK2021.Player.Animation
{
    public interface IWeaponRigHandler
    {
        void SetTargetHandPoints(WeaponIKTargets weaponTargets);
    }

    public class WeaponRigHandler : MonoBehaviour, IWeaponRigHandler
    {
        private WeaponIKTargets weaponBoneTagets;
        public Transform rightHandHandler;
        public Transform leftHandHandler;

        public void SetTargetHandPoints(WeaponIKTargets weaponTargets)
        {
            weaponBoneTagets = weaponTargets;
            rightHandHandler.localPosition = weaponTargets.rightHand.localPosition + weaponTargets.rightHand.parent.localPosition;
            leftHandHandler.localPosition = weaponTargets.leftHand.localPosition + weaponTargets.rightHand.parent.localPosition;

            leftHandHandler.localRotation = weaponTargets.leftHand.localRotation;
        }
    }
}