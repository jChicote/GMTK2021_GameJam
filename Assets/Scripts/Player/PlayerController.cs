using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK2021.Player.Input;

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
            InitialiseInputSystem();
            InitialiseMovementSystem();
        }

        private void InitialiseInputSystem()
        {
            IDesktopInputController desktopController = this.GetComponent<IDesktopInputController>();
            desktopController.InitialiseInputController();
        }

        private void InitialiseMovementSystem()
        {
            IPlayerMovement playerMovement = this.GetComponent<IPlayerMovement>();
            playerMovement.initialiseMovement();
        }
    }

    public interface IPausible
    {
        void Pause();
        void UnPause();
    }
}
