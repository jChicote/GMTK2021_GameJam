using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.UI
{
    public interface IAimHUD
    {
        Vector2 GetCrossHairRecticlePosition();
    }

    public class AimHudUI : MonoBehaviour, IAimHUD
    {
        public RectTransform crossHairRecticle;

        public Vector2 GetCrossHairRecticlePosition()
        {
            return crossHairRecticle.position;
        }
    }
}
