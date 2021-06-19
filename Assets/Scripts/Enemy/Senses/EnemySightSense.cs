using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Enemy
{

    public interface IEnemySight
    {

    }

    public class EnemySightSense : MonoBehaviour, IEnemySight, IPerceptionSense
    {
        public void InitialiseSense()
        {
            throw new System.NotImplementedException();
        }

        public void RunSense()
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            
        }
    }
}
