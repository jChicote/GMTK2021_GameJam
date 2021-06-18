using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GMTK2021.Player.Camera
{
    public class PlayerCameraController1 : MonoBehaviour
    {
        public CinemachineBrain cameraBrain;

        public void Awake()
        {
            InitialiseController();
        }

        public void InitialiseController()
        {
            print(cameraBrain.ActiveVirtualCamera.Description);
        }
    }
}
