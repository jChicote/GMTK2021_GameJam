using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK2021.Player.Animation;

namespace GMTK2021.Player
{
    public class PlayerAnimationBehaviour : StateMachineBehaviour
    {

        private GameObject rootGameObject;

        private void GetRootGameObject(Animator animator)
        {
            rootGameObject = animator.transform.root.gameObject;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (rootGameObject == null) GetRootGameObject(animator);

            if (stateInfo.IsName("Equip_Motion"))
            {
                IPlayerAnimationRigging animRig = rootGameObject.GetComponent<IPlayerAnimationRigging>();
                animRig.OnAnimationRigArmed();
            }
        }
    }

}