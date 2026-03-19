using UnityEngine;

namespace VMCReplaceAvatar
{
    public class InitialTransform : MonoBehaviour
    {
        public Vector3 initialPosition;
        public Quaternion initialRotation;

        private void Start()
        {
            initialRotation = transform.localRotation;
            initialPosition = transform.localPosition;
        }
    }
}
