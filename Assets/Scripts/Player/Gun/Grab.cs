using System;
using UnityEngine;

namespace Player.Gun
{
    public class Grab : MonoBehaviour
    {
        [SerializeField] private Transform newObjectPosition;

        public bool _objectGrabbed;
        public bool _attempToCatch;
        private LauncherGun _launcherGun;

        private void Start()
        {
            _launcherGun = FindObjectOfType<LauncherGun>();
        }

        public void TryGrab()
        {
            _attempToCatch = true;
            Debug.Log("tried");
        }

        private void Catch(GameObject objectToGrab)
        {
            if (_objectGrabbed || !_attempToCatch || objectToGrab.layer != LayerMask.NameToLayer($"IsGrabable")) return;
            
            objectToGrab.transform.position = newObjectPosition.position;
            _launcherGun.objectToLaunch = objectToGrab;
            _objectGrabbed = true;
            _attempToCatch = false;
            Debug.Log("catched");
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("trigger");
            Catch(other.gameObject);
        }

        private void Update()
        {
            if (!_launcherGun.objectToLaunch)
            {
                _objectGrabbed = false;
            }

            if (_launcherGun.objectToLaunch)
            {
                _launcherGun.objectToLaunch.transform.position = newObjectPosition.position;
            }
        }
    }
}