using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        protected virtual void InitialiseEnemy() { }
        protected virtual void InitialiseMovementControl() { }
        protected virtual void IntialiseWeaponControl() { }
        protected virtual void InitialiseStateManager() { }
    }
}
