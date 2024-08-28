using Player.Gun;
using UnityEngine;
using UnityEngine.UI;
public class BarManager : MonoBehaviour
{
    [Tooltip("Slider for controlling the master volume.")]
    [SerializeField] private Scrollbar launchForce;
    
    private LauncherGun _launcher;

    private void Start()
    {
        _launcher = FindObjectOfType<LauncherGun>();    
        
        launchForce.size = _launcher.launchForce / _launcher.maxLaunchForce;
    }

    private void Update()
    {
        launchForce.size = _launcher.launchForce / _launcher.maxLaunchForce;
    }
}