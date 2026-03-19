using UnityEngine;

namespace VMCReplaceAvatar
{
    public class ScaleSync : MonoBehaviour
    {
        public Transform TargetTransform;
        public Config config;

        private void Update()
        {
            if (TargetTransform != null && config != null)
            {
                if (config.avatarSelfScaling)
                    transform.localScale = new Vector3(1 / TargetTransform.localScale.x, 1 / TargetTransform.localScale.y, 1 / TargetTransform.localScale.z);
                else
                    transform.localScale = Vector3.one;
            }
        }
    }
}
