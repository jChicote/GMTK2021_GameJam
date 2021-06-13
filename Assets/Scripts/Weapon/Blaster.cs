using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Weapons
{
    public class Blaster : Weapon
    {
        protected SimpleTimer timer;
        protected bool isReloading = false;
        public float fireRate;
        
        public override void InitialiseWeapon()
        {
            timer = new SimpleTimer(fireRate, Time.deltaTime);
        }

        public override void FireWeapon()
        {
            if (isPaused || isReloading) return;

            timer.TickTimer();
            if (!timer.CheckTimeIsUp()) return;

            GameObject projectileInstance = Instantiate(projectile, firingPoint.position, Quaternion.identity);

            timer.ResetTimer();
        }

        public override void ReloadWeapon()
        {
            base.ReloadWeapon();
        }
    }
}

public class SimpleTimer
{
    // Fields
    private float intervalLength;
    private float timeLeft;
    private float deltaTime;

    public SimpleTimer(float intervalLength, float deltaTime)
    {
        this.intervalLength = intervalLength;
        this.timeLeft = intervalLength;
        this.deltaTime = deltaTime;
    }

    public void TickTimer()
    {
        timeLeft -= deltaTime;
    }

    public bool CheckTimeIsUp()
    {
        return timeLeft <= 0;
    }

    public void ResetTimer()
    {
        timeLeft = intervalLength;
    }
}
