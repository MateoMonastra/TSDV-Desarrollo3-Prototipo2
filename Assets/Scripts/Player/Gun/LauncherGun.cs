using System;
using UnityEngine;

namespace Player.Gun
{
public class LauncherGun : MonoBehaviour
{
    [Header("Fuerza")]
    [SerializeField] private float minLaunchForce = 5f;
    [SerializeField] public float maxLaunchForce = 20f;
    [SerializeField] private float launchUpgrade = 10f;

    [Header("Cooldown")]
    [SerializeField] private float shootCooldown = 1f;
    
    [Header("Referencia camara")]
    [SerializeField] private GameObject pov;

    public float launchForce;
    private float _coolDownTimer;
    public bool isShooting;
    public bool isUpgrading;
    public GameObject objectToLaunch;

    private Rigidbody _rb;
    private GroundCheck _groundCheck;

    private void Start()
    {
        _rb = GetComponentInChildren<Rigidbody>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        launchForce = minLaunchForce;
    }

    public void ResetValue()
    {
        isUpgrading = true;
    }

    private void UpgradeValue()
    {
        if (launchForce < maxLaunchForce)
        {
            launchForce += launchUpgrade * Time.deltaTime;
            launchForce = Mathf.Min(launchForce, maxLaunchForce);
        }
    }

    private void Launch()
    {
        Vector3 launchDirection = pov.transform.forward;
        
        if (launchDirection.y < -0.1f)
        {
            launchDirection.y = 0; 
            launchDirection.Normalize();
        }

        if (_groundCheck.IsOnGround())
        {
            
            Vector3 force = -launchDirection * launchForce;
            
            if (force.y < 0.5f)
            {
                force.y = 0.5f * launchForce;
            }

            _rb.AddForce(force, ForceMode.VelocityChange);
        }
        else if (objectToLaunch)
        {
            var rb = objectToLaunch.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(launchDirection * launchForce, ForceMode.VelocityChange);
                
                Vector3 force = -launchDirection * launchForce;

                _rb.AddForce(force, ForceMode.Impulse);
            }

            Debug.Log($"Direction: {launchDirection}, Force: {launchForce}");
            Debug.Log("Shooted");

            objectToLaunch = null;
        }

        // Reset estado
        _coolDownTimer = 0;
        isShooting = false;
        launchForce = minLaunchForce;
    }

    private void Update()
    {
        if (_coolDownTimer < shootCooldown)
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
        if (_coolDownTimer >= shootCooldown && isShooting)
        {
            Launch();
        }
    }
}

}