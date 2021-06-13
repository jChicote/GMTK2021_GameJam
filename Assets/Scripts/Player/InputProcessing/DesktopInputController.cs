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

        private bool isPaused = false;

        public void InitialiseInputController()
        {
            playerMovement = this.GetComponent<IPlayerMovement>();
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

        private void OnFire(InputValue value)
        {
            if (isPaused) return;
        }

        private void OnModeShift(InputValue value)
        {
            if (isPaused) return;
            print(value.isPressed);
            playerMovement.SetMovementModeShift(value.isPressed);
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