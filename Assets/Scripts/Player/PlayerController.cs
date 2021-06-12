using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Player
{
    public class PlayerController : MonoBehaviour
    {
        private void Awake()
        {
            InitialisePlayerComponents();
        }

        private void InitialisePlayerComponents()
        {
            IPlayerMovement playerMovement = this.GetComponent<IPlayerMovement>();
            playerMovement.initialiseMovement();
        }
    }
}
