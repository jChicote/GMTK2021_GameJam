using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK2021.Enemy.States;

namespace GMTK2021.Enemy
{

    public interface IStateManager
    {
        void AddState<T>() where T : EnemyBaseState;
    }

    public class EnemyStateManager : MonoBehaviour, IStateManager
    {
        private EnemyBaseState currentState;

        public void AddState<T>() where T : EnemyBaseState
        {
            if (currentState != null)
                this.RemoveState();

            currentState = this.gameObject.AddComponent<T>();
            currentState.BeginState();
        }

        public void RemoveState()
        {
            if (currentState != null)
            {
                currentState.EndState();
                Destroy(currentState);
            }
        }
    }
}
