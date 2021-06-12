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
    }

    public class PlayerMovement : MonoBehaviour, IPlayerMovement, IPausible
    {
        private Transform playerTransform;
        private Rigidbody playerRB;
        private Vector3 currentMovement;
        private Vector3 currentRotation;
        private Quaternion playerRotation;

        private bool isPaused = false;

        public float movementSpeed;
        public float mouseSensitivity = 100f;

        public void initialiseMovement()
        {
            playerTransform = this.transform;
            playerRB = this.GetComponent<Rigidbody>();

            currentMovement = Vector3.zero;
            currentRotation = playerTransform.rotation.eulerAngles;
        }

        public void FixedUpdate()
        {
            if (isPaused) return;

            SetMovement();
            SetLookRotation();

        }

        private void SetMovement()
        {
            playerRB.position += currentMovement;
        }

        private void SetLookRotation()
        {
            playerRB.rotation = playerRotation;
        }

        public void SetMovementInput(Vector2 inputVector)
        {
            print(inputVector);
            //currentMovement.z = inputVector.normalized.y * movementSpeed * Time.deltaTime;
           // currentMovement.x = inputVector.normalized.x * movementSpeed * Time.deltaTime;

            currentMovement = playerTransform.forward * inputVector.y * movementSpeed * Time.deltaTime;
        }

        public void SetLookInput(Vector2 inputVetor)
        {
            currentRotation.y += inputVetor.x * mouseSensitivity * Time.deltaTime;
            playerRotation = Quaternion.Euler(currentRotation);
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
