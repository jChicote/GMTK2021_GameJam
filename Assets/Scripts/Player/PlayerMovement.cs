using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Player
{
    public interface IPlayerMovement
    {
        void initialiseMovement();
        void SetMovementInput(Vector2 inputVector);
        void SetLookInput(Vector2 inputVetor);
        void SetMovementModeShift(bool state);
        void TriggerJump();
    }

    public class PlayerMovement : MonoBehaviour, IPlayerMovement, IPausible
    {
        public Animator animController;
        public CharacterController controller;
        private Transform playerTransform;
        public Transform footPosition;
        private Rigidbody playerRB;

        private Vector3 movementVector;
        private Vector3 currentMovement;
        private Vector3 verticalMovement;
        private Vector3 forwardVector;
        private Vector3 rightVector;
        private Vector3 currentRotation;
        private Quaternion playerRotation;

        private float forwardDot = 0;
        private float rightDot = 0;

        private bool isPaused = false;
        private bool isGrounded = false;
        private bool isModeShifting = false;

        public float gravity = 9.8f;
        public float jumpForce = 10f;
        public float runSpeed;
        public float walkSpeed;

        RaycastHit hit;
        public float mouseSensitivity = 100f;

        public void initialiseMovement()
        {
            playerTransform = this.transform;
            playerRB = this.GetComponent<Rigidbody>();

            currentMovement = Vector3.zero;
            currentRotation = playerTransform.rotation.eulerAngles;
            playerRotation = Quaternion.Euler(Vector3.zero);
        }

        public void FixedUpdate()
        {
            if (isPaused) return;

            SetMovement();
            SetLookRotation();
        }

        private void SetMovement()
        {
            isGrounded = CheckIsGrounded();
            animController.SetBool("isGrounded", isGrounded);
            forwardVector = DetermineMovmentSpeed() * movementVector.z * playerTransform.forward;
            rightVector = DetermineMovmentSpeed() * movementVector.x * playerTransform.right;
            currentMovement = forwardVector - -rightVector;
            currentMovement += verticalMovement;
            RunPlayerGravityCheck();
            controller.Move(currentMovement * Time.deltaTime);
            AnimateMovement();
            print((currentMovement * Time.deltaTime).sqrMagnitude);
        }

        private void RunPlayerGravityCheck()
        {
            if (!isGrounded)
            {
                ApplyGravity();
                return;
            } 
        }

        private void ApplyGravity()
        {
            currentMovement += gravity * Vector3.down * Time.fixedDeltaTime;
            verticalMovement += gravity * Vector3.down * Time.fixedDeltaTime; // The vertical upward force needs to decrease to sim smooth gravity!!!!!
            //verticalMovement.y = Mathf.Clamp(verticalMovement.y, 0, jumpForce);
        }

        private bool CheckIsGrounded()
        {
            // Returns true if the raycast touches the ground
            if (Physics.Raycast(footPosition.position, Vector3.down, out hit, 0.5f))
            {
                animController.SetBool("isJumping", false);
                return true;
            }

            return false;
            //return Physics.Raycast(footPosition.position, Vector3.down, out hit, 0.5f);
        }

        private void AnimateMovement()
        {
            forwardDot = Mathf.Lerp(forwardDot, Vector3.Dot(playerTransform.forward, currentMovement.normalized), 0.1f);
            rightDot = Mathf.Lerp(rightDot, Vector3.Dot(playerTransform.right, currentMovement.normalized), 0.1f);
            animController.SetFloat("forwardBlend", forwardDot * 2);
            animController.SetFloat("rightBlend", rightDot * 2);
        }

        private void SetLookRotation()
        {
            playerRB.MoveRotation(playerRotation);
        }

        private float DetermineMovmentSpeed()
        {
            return isModeShifting ? runSpeed : walkSpeed;
        }

        public void SetMovementInput(Vector2 inputVector)
        {
            animController.SetBool("isMoving", inputVector != Vector2.zero);
            movementVector = new Vector3(inputVector.x, 0, inputVector.y);
        }

        public void SetLookInput(Vector2 inputVetor)
        {
            currentRotation.y += inputVetor.x * mouseSensitivity * Time.fixedDeltaTime;
            playerRotation = Quaternion.Euler(currentRotation);
        }

        public void SetMovementModeShift(bool state)
        {
            isModeShifting = state;
            animController.SetBool("isModeShifting", isModeShifting);
        }

        public void TriggerJump()
        {
            if (!isGrounded) return;
            verticalMovement += jumpForce * Vector3.up;
            animController.SetBool("isJumping", true);
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
