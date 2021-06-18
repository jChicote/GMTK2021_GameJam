using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy.States
{
    public abstract class EnemyBaseState : MonoBehaviour, IPausible
    {
        protected bool isPaused = false;

        public abstract void BeginState();
        public virtual void EndState() { }

        public void Pause()
        {
            isPaused = true;
        }

        public void UnPause()
        {
            isPaused = false;
        }
    }

}