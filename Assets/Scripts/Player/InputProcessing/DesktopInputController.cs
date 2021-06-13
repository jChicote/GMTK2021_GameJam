using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK2021.Player.Input
{
    public interface IDesktopInputController
    {
        void InitialiseInputController();
    }

    public class DesktopInputController : MonoBehaviour, IDesktopInputController, IPausible
    {
        private IPlayerMovement playerMovement;
        private IPlayerWeaponControl playerWeapons;
        private ICameraController cameraController;

        private bool isPaused = false;

        public void InitialiseInputController()
        {
            playerMovement = this.GetComponent<IPlayerMovement>();
            playerWeapons = this.GetComponent<IPlayerWeaponControl>();
            cameraController = this.GetComponent<ICameraController>();
        }

        private void OnMove(InputValue value)
        {
            if (isPaused) return;

            playerMovement.SetMovementInput(value.Get<Vector2>());
        }

        private void OnLook(InputValue value)
        {
            if (isPaused) return;

            playerMovement.SetLookInput(value.Get<Vector2>());
        }

        private void OnJump(InputValue value)
        {
            if (isPaused) return;
            //if (!value.isPressed) return;
            //print("triggered jump");
            playerMovement.TriggerJump();
        }

        private void OnAim(InputValue value)
        {
            if (isPaused) return;

            if (value.isPressed)
            {
                cameraController.SwitchToAimCam();
            } else
            {
                cameraController.SwitchToMainFollowCam();
            }
        }

        private void OnFire(InputValue value)
        {
            if (isPaused) return;
            playerWeapons.FireWeapon();
        }

        private void OnModeShift(InputValue value)
        {
            if (isPaused) return;
            print(value.isPressed);
            playerMovement.SetMovementModeShift(value.isPressed);
        }

        private void OnHolster(InputValue value)
        {
            if (isPaused) return;
            playerWeapons.HolsterWeapon();
        }

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