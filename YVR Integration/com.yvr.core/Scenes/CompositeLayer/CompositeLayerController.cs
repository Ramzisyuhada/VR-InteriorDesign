using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YVR.Core;
using UnityEngine.Rendering;

namespace YVR.Core.Demo
{
    [ExcludeFromDocs]
    public class CompositeLayerController : MonoBehaviour
    {
        public YVRCompositeLayer underlay = null;

        private Camera compositeLayerCamera = null;

        private int bufferIndex = 0;
        private void Start()
        {
            compositeLayerCamera = GetComponent<Camera>();

            RenderTexture compositeLayerRT = new RenderTexture(1700, 1700, 24);
            compositeLayerRT.hideFlags = HideFlags.DontSave;
            compositeLayerRT.useMipMap = true;
            compositeLayerRT.filterMode = FilterMode.Trilinear;
            compositeLayerRT.antiAliasing = 0;
            compositeLayerRT.Create();

            compositeLayerCamera.allowMSAA = false;
            compositeLayerCamera.targetTexture = compositeLayerRT;

            underlay.texture = compositeLayerRT;

            if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset != null)
            {
                RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
            }
        }


        private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            if (camera != compositeLayerCamera) return;

            OnPostRender();
        }

        private void OnPostRender()
        {

            if (underlay.renderLayerID == -1) return;

            YVRPlugin.Instance.ReboundFBO2Layer(underlay.renderLayerID, (bufferIndex++) % 3);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                underlay.enabled = !underlay.enabled;
            }
        }
    }
}
