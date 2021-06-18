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
            Cursor.visible = false;
            InitialisePlayerComponents();
        }

        private void InitialisePlayerComponents()
        {
            InitialiseInputSystem();
            InitialiseMovementSystem();
            InitialiseWeaponSystem();
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

        private void InitialiseWeaponSystem()
        {
            IPlayerWeaponControl weaponControl = this.GetComponent<IPlayerWeaponControl>();
            weaponControl.InitialiseWeapons();

            ITargetingSystem targetingSystem = this.GetComponent<ITargetingSystem>();
            targetingSystem.RunTargetingSystem();
        }
    }
}

public interface IPausible
{
    void Pause();
    void UnPause();
}
