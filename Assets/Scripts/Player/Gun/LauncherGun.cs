using System;
using UnityEngine;

namespace Player.Gun
{
    public class LauncherGun : MonoBehaviour
    {
        [SerializeField] private float minLaunchForce;
        [SerializeField] public float maxLaunchForce;
        [SerializeField] private float launchUpgrade;
        [SerializeField] private float shootCooldown;
        [SerializeField] private GameObject pov;


        public float launchForce;
        private float _coolDownTimer;
        public bool isShooting;
        public bool isUpgrading;
        public GameObject objectToLaunch;

        private Rigidbody _rb;
        private GroundCheck _groundCheck;

// Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponentInChildren<Rigidbody>();
            _groundCheck = GetComponentInChildren<GroundCheck>();
        }

        public void ResetValue()
        {
            isUpgrading = true;
        }

        private void UpgradeValue()
        {
            if (!(launchForce < maxLaunchForce)) return;

            launchForce += launchUpgrade * Time.deltaTime;

            if (launchForce > maxLaunchForce)
            {
                launchForce = maxLaunchForce;
            }
        }

        private void Launch()
        {
            if (_groundCheck.IsOnGround())
            {
                var oppositeDirection = -pov.transform.forward;
                _rb.AddForce(oppositeDirection * launchForce, ForceMode.Impulse);
            }
            else
            {
                if (objectToLaunch)
                {
                    var direction = pov.transform.forward * launchForce;
                    if (direction.y is < 18.0f and > -4.0f)
                    {
                        direction.y = 18.0f;
                    }
                    objectToLaunch.GetComponent<Rigidbody>().AddForce(direction, ForceMode.VelocityChange);
                    
                    objectToLaunch = null;

                    Debug.Log(direction);
                    Debug.Log("Shooted");
                }
            }

            _coolDownTimer = 0;
            isShooting = false;
            launchForce = minLaunchForce;
        }

        private void Update()
        {
            if (shootCooldown > _coolDownTimer)
            {
                _coolDownTimer += Time.deltaTime;
            }

            if (isUpgrading)
            {
                UpgradeValue();
            }
        }

        private void FixedUpdate()
        {
            if (shootCooldown < _coolDownTimer && isShooting)
            {
                Launch();
            }
        }
    }
}