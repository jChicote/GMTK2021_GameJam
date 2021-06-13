using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Player
{
    public interface IPlayerWeaponControl
    {
        void FireWeapon();
        void HolsterWeapon();
    }

    public class PlayerWeaponControl : MonoBehaviour, IPlayerWeaponControl
    {
        public Animator animControl;

        private bool isHolstered = true;

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
        }
    }
}