using System.Collections.Generic;
using UnityEngine;

namespace VMCReplaceAvatar
{
    public class BlendShapeSync : MonoBehaviour
    {
        public Renderer sourceRenderer;
        public VRMAvatarMeshSetting meshSetting;

        private InitialShapes _initialShapes;
        private void Start()
        {
            _initialShapes = GetComponent<InitialShapes>();
            if ( _initialShapes == null )
                Debug.LogError("InitialShapes component not found on the GameObject.");
        }

        private void Update()
        {
            if (sourceRenderer == null || meshSetting == null || _initialShapes == null) return;

            SkinnedMeshRenderer sourceSkinnedMeshRenderer = sourceRenderer as SkinnedMeshRenderer;
            SkinnedMeshRenderer targetSkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

            if (sourceSkinnedMeshRenderer == null || targetSkinnedMeshRenderer == null) return;

            int blendShapeCount = sourceSkinnedMeshRenderer.sharedMesh.blendShapeCount;
            for (int i = 0; i < blendShapeCount; i++)
            {
                string blendShapeName = sourceSkinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
                int targetBlendShapeIndex = targetSkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName);
                if (targetBlendShapeIndex != -1)
                {
                    if (_initialShapes.initialBlendShapes[i].initialWeight == 0f || !meshSetting.ignoreSyncInitialValue)
                    {
                        float weight = sourceSkinnedMeshRenderer.GetBlendShapeWeight(i);
                        targetSkinnedMeshRenderer.SetBlendShapeWeight(targetBlendShapeIndex, weight);
                    }
                    else
                    {
                        targetSkinnedMeshRenderer.SetBlendShapeWeight(targetBlendShapeIndex, _initialShapes.initialBlendShapes[i].initialWeight);
                    }
                }
            }
        }
    }
}
