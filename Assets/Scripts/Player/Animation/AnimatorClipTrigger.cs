using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Player.Animation
{
    public class AnimatorClipTrigger : MonoBehaviour
    {
        private IPlayerAnimationRigging animRigging;

        private void Awake()
        {
            animRigging = transform.root.GetComponent<IPlayerAnimationRigging>();
        }

        public void TriggerAnimUnequip()
        {
            animRigging.ModifyRigIKForUnequipMotion();
        }

        public void TriggerAnimHolster()
        {
            animRigging.OnAnimationRigHolstered();
        }

        public void TriggerAnimEquip()
        {
            animRigging.ModifyRigForEquipMotion();
        }
    }
}

