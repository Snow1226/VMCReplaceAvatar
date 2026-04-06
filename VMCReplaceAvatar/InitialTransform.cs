using UnityEngine;

namespace VMCReplaceAvatar
{
    public class InitialTransform : MonoBehaviour
    {
        public Vector3 initialPosition;
        public Quaternion initialRotation;

        private void Awake()
        {
            initialRotation = transform.localRotation;
            initialPosition = transform.localPosition;
        }
    }
}
