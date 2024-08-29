using UnityEngine;

namespace Target
{
    public class TargetBreak : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("IsGrabable"))
            {
                Break();
            }
        }

        private void Break()
        {
            gameObject.SetActive(false);
        }
    }
}