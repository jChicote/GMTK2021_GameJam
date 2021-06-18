using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GMTK2021.Player
{

    public interface ICameraController
    {
        void SwitchToAimCam();
        void SwitchToMainFollowCam();
        void SetCameraVerticalOffset(float verticalOffset, float mouseSensitivity);
    }

    public class PlayerCameraController : MonoBehaviour, ICameraController
    {
        public CinemachineVirtualCamera playerVirtualCam;
        public CinemachineVirtualCamera aimVirtualCamera;
        public CinemachineBrain cameraBrain;
        public Transform cameraLookTarget;
        public float maximumTurnAngle = 40;
        public float inputSensitivity = 50;
        public float speed;

        private CinemachineVirtualCamera activeCamera;
        private Vector3 currentRotation = Vector3.zero;

        public void SwitchToAimCam()
        {
            aimVirtualCamera.enabled = true;
            playerVirtualCam.enabled = false;
        }

        public void SwitchToMainFollowCam()
        {
            aimVirtualCamera.enabled = false;
            playerVirtualCam.enabled = true;
        }

        private void Update()
        {
            DetectActiveCamera();
        }

        private void DetectActiveCamera()
        {
            activeCamera = playerVirtualCam.isActiveAndEnabled ? playerVirtualCam : aimVirtualCamera;
        }

        public void SetCameraVerticalOffset(float verticalOffset, float mouseSensitivity)
        {
            //cameraLookTarget.Rotate(-verticalOffset * Time.deltaTime * 10, 0, 0);
            //print(cameraLookTarget.localEulerAngles.x);

            currentRotation.x += -verticalOffset * inputSensitivity; // * Time.deltaTime * 50;
            if (verticalOffset == 0) LerpToStop();
            //print(cameraLookTarget.localEulerAngles);
            //print(verticalOffset);
            print(currentRotation.x);
            cameraLookTarget.Rotate(currentRotation.x, 0, 0);

            // constraint rotational axis to 360 degrees
            //float modalAngle = (currentRotation.x > 180) ? currentRotation.x - 360 : currentRotation.x;
            if (cameraLookTarget.localEulerAngles.x > maximumTurnAngle && cameraLookTarget.localEulerAngles.x < (360 - maximumTurnAngle))
            {
                currentRotation.x = 0;
                cameraLookTarget.localEulerAngles = new Vector3(cameraLookTarget.localEulerAngles.x > 270 ? (360 - maximumTurnAngle) : maximumTurnAngle, 0, 0);

            }
        }

        private void LerpToStop()
        {
            currentRotation.x = Mathf.Lerp(currentRotation.x, 0, 0.1f);
        }
    }
}