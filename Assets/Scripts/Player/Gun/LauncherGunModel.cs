using UnityEngine;

namespace Player.Gun
{
    [CreateAssetMenu(fileName = "GunModel", menuName = "Models/Player/Gun")]
    public class LauncherGunModel : ScriptableObject
    {
        [SerializeField] private float minLaunchForce;
        [SerializeField] private float maxLaunchForce;
        [SerializeField] private float launchUpgrade;
        [SerializeField] private float shootCooldown;

        public float MinLaunchForce
        {
            get => minLaunchForce;
            set => minLaunchForce = value;
        }

        public float MaxLaunchForce
        {
            get => maxLaunchForce;
            set => maxLaunchForce = value;
        }

        public float LaunchUpgrade
        {
            get => launchUpgrade;
            set => launchUpgrade = value;
        }

        public float ShootCooldown => shootCooldown;
    }
}