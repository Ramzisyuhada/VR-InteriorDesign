using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace YVR.Core
{
    public abstract partial class YVRPlugin
    {
        public enum EventType { ReboundFBOAttachments }

        public enum EventDataType
        {
            SetFoveationDataEvent,
            CreateSwapChainForLayer,
            UpdateContent2SwapChain,
            ReboundFBO2Layer,
            Count
        }

        public virtual void SetFoveation(int foveation) { }

        public virtual void UpdateTexture2SwapChain(int sourceTextureHandle, int layerID, int bufferIndex, bool useAndroidTexture, System.IntPtr androidRenderMutex) { }

        public virtual void CreateSwapChainForLayer(int layerId, int targetWidth, int targetHeight, int arraySize = 1, int bufferCount = 1) { }

        /// <summary>
        /// Should be called when unity has bound fbo, like in OnPostRender
        /// </summary>
        /// <param name="layerId"></param>
        public virtual void ReboundFBO2Layer(int layerId, int bufferIndex) { }

        public virtual IntPtr GetRenderEventAndDataFuncPtr()
        {
            return IntPtr.Zero;
        }
    }

    partial class YVRPluginAndroid : YVRPlugin
    {
        private CommandBuffer triggerEventCommand = new CommandBuffer();
        private GCHandle[] eventDataList = new GCHandle[(int)EventDataType.Count];

        public override void SetFoveation(int foveation)
        {
            IssueDataEvent(EventDataType.SetFoveationDataEvent, foveation);
        }

        public override void CreateSwapChainForLayer(int targetLayerId, int targetSwapchainTextureWidth, int targetSwapChainTextureHeight, int targetSwapChainArraySize = 1, int targetSwapChainBufferCount = 1)
        {
            CreateSwapChainForLayerData dataPack = new CreateSwapChainForLayerData()
            {
                targetLayerId = targetLayerId,
                targetSwapchainTextureWidth = targetSwapchainTextureWidth,
                targetSwapChainTextureHeight = targetSwapChainTextureHeight,
                targetSwapChainArraySize = targetSwapChainArraySize,
                targetSwapChainBufferCount = targetSwapChainBufferCount
            };

            IssueDataEvent(EventDataType.CreateSwapChainForLayer, dataPack);
        }

        public override void UpdateTexture2SwapChain(int sourceTextureHandle, int layerID, int bufferIndex, bool useAndroidTexture, System.IntPtr androidRenderMutex)
        {
            if (sourceTextureHandle == -1) return;

            UpdateTexture2NativeData dataPack = new UpdateTexture2NativeData()
            { sourceTextureHandle = sourceTextureHandle, destinationLayerID = layerID, destinationBufferIndex = bufferIndex, isAndroidTexture = (useAndroidTexture == true ? 1 : 0), androidRenderMutex = androidRenderMutex};

            IssueDataEvent(EventDataType.UpdateContent2SwapChain, dataPack);
        }

        public override void ReboundFBO2Layer(int layerId, int bufferIndex)
        {
            if (layerId == -1) return;

            ReboundFBO2LayerData dataPack = new ReboundFBO2LayerData() { targetLayerId = layerId, targetBufferIndex = bufferIndex };
            IssueDataEvent(EventDataType.ReboundFBO2Layer, dataPack);
        }

        private void IssueDataEvent(EventDataType eventType, System.Object data)
        {
            int eventIndex = (int)eventType;
            if (!eventDataList[eventIndex].IsAllocated)
                eventDataList[eventIndex] = GCHandle.Alloc(data, GCHandleType.Pinned);

            eventDataList[eventIndex].Target = data;
            IssueDataEvent(eventType, eventDataList[eventIndex].AddrOfPinnedObject());
        }

        public void IssueDataEvent(EventDataType eventType, IntPtr data)
        {
            triggerEventCommand.Clear();
            triggerEventCommand.IssuePluginEventAndData(GetRenderEventAndDataFunc(), (int)eventType, data);
            UnityEngine.Graphics.ExecuteCommandBuffer(triggerEventCommand);
        }
    }
}