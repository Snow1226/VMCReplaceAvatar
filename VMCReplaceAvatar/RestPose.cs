using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VMCReplaceAvatar
{
    public class RestPose :MonoBehaviour
    {
        public bool isRestPose;
        public List<BoneConstraint> boneConstraints;

        private void Awake()
        {
            isRestPose = false;
            boneConstraints = new List<BoneConstraint>();
        }


        public void TransformToRestPose()
        {
            isRestPose = true;
            foreach (var bone in boneConstraints)
            {
                bone.transform.localRotation = bone.initialRotation;
            }
        }
    }
}
