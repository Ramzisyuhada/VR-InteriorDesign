using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace YVR.Core
{
    /// <summary>
    /// Used to represent composite layer, which contains overlay / underlay
    /// </summary>
    public class YVRCompositeLayer : MonoBehaviour
    {
        [DllImport("yvrplugin")]
        private static extern int YVRCreateRenderLayer(int compositeDepth);

        [DllImport("yvrplugin")]
        private static extern void YVRSetLayerMatrix(int layerID, EyeSide eyeSide, ref Matrix4x4 layerVertices);

        [DllImport("yvrplugin")]
        private static extern void YVRAddActiveLayer(int layerID);

        [DllImport("yvrplugin")]
        private static extern void YVRRemoveActiveLayer(int layerID);

        [DllImport("yvrplugin")]
        private static extern void YVRSetLayerBufferIndex(int layerID, EyeSide eyeSide, int bufferIndex);

        /// <summary>
        /// The displayed texture on composite layer
        /// </summary>
        public Texture texture = null;

        /// <summary>
        /// Composite layer depth.
        /// If depth less-than 0, the layer will work as underlayer, otherwise, the layer will works as overlay
        /// </summary>
        [SerializeField] private int compositionDepth = 1;

        /// <summary>
        /// Render scale for composite layer resolution.
        /// While render scale is 1.0, composite layer resolution will equal to the resolution of [texture](xref: YVR.Core.YVRCompositeLayer.texture)
        /// </summary>
        [SerializeField] private float renderScale = 1.0f;

        [SerializeField] private bool isDynamic = false;

        /// <summary>
        /// Should update composite layer texture to native automatically
        /// </summary>
        [SerializeField] private bool autoUpdateContent = false;

        /// <summary>
        /// Should init native compoiste layer automatically
        /// </summary>
        [SerializeField] private bool autoInitLayer = true;

        /// <summary>
        /// Use android texture or not.
        /// </summary>
        public bool useAndroidTexture = false;
        /// <summary>
        /// The texture id of android texture.
        /// </summary>
        public int androidTexture = -1;
        /// <summary>
        /// The width of android texture (only use in @YVRCompositeLayer.InitCompositeLayer)
        /// </summary>
        public int androidTextureWidth = 0;
        /// <summary>
        /// The height of android texture (only use in @YVRCompositeLayer.InitCompositeLayer)
        /// </summary>
        public int androidTextureHeight = 0;
        [ExcludeFromDocs]
        public System.IntPtr androidRenderMutex = IntPtr.Zero;

        private Texture cachedTexture = null;
        private int _textureHandle = -1;
        private int textureHandle
        {
            get
            {
                if (cachedTexture != texture && texture != null)
                {
                    _textureHandle = (int)texture.GetNativeTexturePtr();
                    cachedTexture = texture;

                }
                return _textureHandle;
            }
        }

        private int _renderLayerID = -1;
        /// <summary>
        /// The mask id of render layer
        /// </summary>
        public int renderLayerID
        {
            get => _renderLayerID;
            private set { _renderLayerID = value; }
        }

        private Matrix4x4 leftInvViewModelMatrix = Matrix4x4.identity;
        private Matrix4x4 rightInvViewModelMatrix = Matrix4x4.identity;
        private new Transform transform = null;

        #region View Matrix Cache
        private int leftViewMatrixCachedFrame = -1;
        private Matrix4x4 _leftViewMatrix;
        private Matrix4x4 leftViewMatrix
        {
            get
            {
                if (Time.frameCount != leftViewMatrixCachedFrame)
                {
                    _leftViewMatrix = YVRManager.instance.cameraRenderer.leftEyeCamera.worldToCameraMatrix;
                    leftViewMatrixCachedFrame = Time.frameCount;
                }
                return _leftViewMatrix;
            }
        }

        private int rightViewMatrixCachedFrame = -1;
        private Matrix4x4 _rightViewMatrix;
        private Matrix4x4 rightViewMatrix
        {
            get
            {
                if (Time.frameCount != rightViewMatrixCachedFrame)
                {
                    _rightViewMatrix = YVRManager.instance.cameraRenderer.rightEyeCamera.worldToCameraMatrix;
                    rightViewMatrixCachedFrame = Time.frameCount;
                }
                return _rightViewMatrix;
            }
        }

        private int bufferIndex = 0;

        #endregion

        private void OnEnable()
        {
            YVRAddActiveLayer(renderLayerID);
        }

        private void Start()
        {
            this.transform = base.transform;
            if (autoInitLayer && texture != null)
                InitCompositeLayer(compositionDepth);
        }

        /// <summary>
        /// Init native composite layer, register composite layer update operations.
        /// </summary>
        /// <param name="depth">The depth of the composite layer</param>
        public void InitCompositeLayer(int depth = int.MinValue)
        {
            StartCoroutine(InitCompositeLayerCoroutineFunction(depth));
        }

        private IEnumerator InitCompositeLayerCoroutineFunction(int depth)
        {
            yield return null;
            if (depth != int.MinValue)
            {
                compositionDepth = depth;
            }
            int width = 0;
            int height = 0;

            if (useAndroidTexture == true)
            {
                width = androidTextureWidth;
                height = androidTextureHeight;
            }
            else
            {
                width = texture.width;
                height = texture.height;
            }

            renderLayerID = YVRCreateRenderLayer(compositionDepth);
            YVRPlugin.Instance.CreateSwapChainForLayer(renderLayerID, (int)(width * renderScale), (int)(height * renderScale), 1, isDynamic ? 3 : 1);
            UpdateCompositeLayerContent(); // At least update once

            Application.onBeforeRender += UpdateCompositeLayerMatrixes;
            if (isDynamic && autoUpdateContent) Application.onBeforeRender += UpdateCompositeLayerContent;
        }

        /// <summary>
        /// Update Composite layer content
        /// </summary>
        public void UpdateCompositeLayerContent()
        {
            YVRSetLayerBufferIndex(renderLayerID, EyeSide.Both, bufferIndex);

            if (useAndroidTexture == true)
            {
                YVRPlugin.Instance.UpdateTexture2SwapChain(androidTexture, renderLayerID, bufferIndex, useAndroidTexture, androidRenderMutex);
            }
            else
            {
                YVRPlugin.Instance.UpdateTexture2SwapChain(textureHandle, renderLayerID, bufferIndex, useAndroidTexture, androidRenderMutex);
            }

            bufferIndex = (bufferIndex + 1) % 3;
        }

        private void UpdateCompositeLayerMatrixes()
        {
            // Unity Matrix is stored in col-major while native is row-major, thus required tranposition
            leftInvViewModelMatrix = (leftViewMatrix * transform.localToWorldMatrix).inverse.transpose;
            rightInvViewModelMatrix = (rightViewMatrix * transform.localToWorldMatrix).inverse.transpose;

            YVRSetLayerMatrix(renderLayerID, EyeSide.Left, ref leftInvViewModelMatrix);
            YVRSetLayerMatrix(renderLayerID, EyeSide.Right, ref rightInvViewModelMatrix);
        }

        private void OnDisable()
        {
            YVRRemoveActiveLayer(renderLayerID);
        }

        private void OnDestroy()
        {
            Application.onBeforeRender -= UpdateCompositeLayerMatrixes;
            if (isDynamic && autoUpdateContent) Application.onBeforeRender -= UpdateCompositeLayerContent;
        }
    }
}
