using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK2021.Enemy.States;

namespace GMTK2021.Enemy
{
    public class DroneBrain : EnemyBrain
    {
        private void Awake()
        {
            InitialiseEnemy();
        }

        protected override void InitialiseEnemy()
        {
            InitialiseStateManager();
            InitialiseSenses();
            InitialiseMovementControl();
            IntialiseWeaponControl();
        }

        protected override void InitialiseMovementControl()
        {
            base.InitialiseMovementControl();
        }

        protected override void InitialiseStateManager()
        {
            IStateManager stateManager = this.GetComponent<IStateManager>();
            stateManager.AddState<DroneFollowPath>();
        }

        protected override void IntialiseWeaponControl()
        {
            ITargeting targeting = this.GetComponent<ITargeting>();
            targeting.InitialiseTargetingController();
        }

        protected void InitialiseSenses()
        {
            ISenses senses = this.GetComponent<ISenses>();
            senses.InitialiseSenses();
            
        }
    }
}
