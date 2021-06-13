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
    }

    public class PlayerCameraController : MonoBehaviour, ICameraController
    {
        public CinemachineVirtualCamera playerVirtualCam;
        public CinemachineVirtualCamera aimVirtualCamera;
        public CinemachineBrain cameraBrain;

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
    }

}