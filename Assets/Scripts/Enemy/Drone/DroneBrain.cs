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
            InitialiseMovementControl();
            IntialiseWeaponControl();
            InitialiseSenses();
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
            base.IntialiseWeaponControl();
        }

        protected void InitialiseSenses()
        {
            ISenses senses = this.GetComponent<ISenses>();
            senses.InitialiseSenses();
        }
    }
}
