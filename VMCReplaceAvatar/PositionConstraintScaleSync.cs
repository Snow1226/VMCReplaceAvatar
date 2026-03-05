using UnityEngine;

namespace VMCReplaceAvatar
{
    public class PositionConstraintScaleSync : MonoBehaviour
    {
        public GameObject TargetScaleReferenceObject;
        public GameObject TargetConstraintObject;
        private bool IsSync = false;
        public Config config;
        public bool IsLocal = true;
        private Vector3 _currentPosition = Vector3.zero;

        private void Update()
        {
            if(config != null && config.avatarSelfScaling)
                IsSync = true;
            else
                IsSync = false;

            if (IsSync && TargetScaleReferenceObject != null)
            {
                if (IsLocal)
                {
                    _currentPosition.x = TargetConstraintObject.transform.localPosition.x * (1 / TargetScaleReferenceObject.transform.localScale.x);
                    _currentPosition.y = TargetConstraintObject.transform.localPosition.y * (1 / TargetScaleReferenceObject.transform.localScale.y);
                    _currentPosition.z = TargetConstraintObject.transform.localPosition.z * (1 / TargetScaleReferenceObject.transform.localScale.z);
                }
                else
                {
                    _currentPosition.x = TargetConstraintObject.transform.position.x * (1 / TargetScaleReferenceObject.transform.localScale.x);
                    _currentPosition.y = TargetConstraintObject.transform.position.y * (1 / TargetScaleReferenceObject.transform.localScale.y);
                    _currentPosition.z = TargetConstraintObject.transform.position.z * (1 / TargetScaleReferenceObject.transform.localScale.z);
                }
            }
            else
            {
                if (IsLocal)
                    _currentPosition = TargetConstraintObject.transform.localPosition;
                else
                    _currentPosition = TargetConstraintObject.transform.position;
            }
            if (IsLocal)
                transform.localPosition = _currentPosition;
            else
                transform.position = _currentPosition;
        
            transform.localRotation = TargetConstraintObject.transform.localRotation;
        }
    }
}
