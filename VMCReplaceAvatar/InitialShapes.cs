using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VMCReplaceAvatar
{
    public class InitialShapes : MonoBehaviour
    {
        public List<BlendShapeData> initialBlendShapes = new List<BlendShapeData>();

        private void Awake() 
        {
            var skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer != null)
            {
                int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
                for (int i = 0; i < blendShapeCount; i++)
                {
                    string blendShapeName = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
                    float initialWeight = skinnedMeshRenderer.GetBlendShapeWeight(i);
                    initialBlendShapes.Add(new BlendShapeData
                    {
                        blendShapeIndex = i,
                        blendShapeName = blendShapeName,
                        initialWeight = initialWeight
                    });
                }
            }
        }
    }
}
