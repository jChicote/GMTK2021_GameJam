using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Player
{
    public interface IPlayerMovement
    {
        void initialiseMovement();
    }

    public class PlayerMovement : MonoBehaviour, IPlayerMovement
    {
        public void initialiseMovement()
        {

        }

        public void FixedUpdate()
        {
            
        }

        public void SetMovementInput()
        {

        }
    }
}
