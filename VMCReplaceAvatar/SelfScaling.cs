using UnityEngine;

namespace VMCReplaceAvatar
{
    public class SelfScaling : MonoBehaviour
    {
        public bool AvatarSelfScaling = false;
        public Config config;

        private void Start()
        {
            if (config != null)
                AvatarSelfScaling = config.avatarSelfScaling;
        }

        private void Update()
        {
            if (config != null)
            {
                if (config.avatarSelfScaling != AvatarSelfScaling)
                {
                    AvatarSelfScaling = config.avatarSelfScaling;
                }
            }
        }

    }
}
