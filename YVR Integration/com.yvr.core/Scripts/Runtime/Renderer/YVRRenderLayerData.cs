using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public enum EyeSide { Left = 1 << 0, Right = 1 << 1, Both = Left | Right }

    [ExcludeFromDocs]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UpdateTexture2NativeData
    {
        public int sourceTextureHandle;
        public int destinationLayerID;
        public int destinationBufferIndex;
        public int isAndroidTexture;
        public System.IntPtr androidRenderMutex;
    }


    [ExcludeFromDocs]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CreateSwapChainForLayerData
    {
        public int targetLayerId;
        public int targetSwapchainTextureWidth;
        public int targetSwapChainTextureHeight;
        public int targetSwapChainArraySize;
        public int targetSwapChainBufferCount;
    }

    [ExcludeFromDocs]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ReboundFBO2LayerData
    {
        public int targetLayerId;
        public int targetBufferIndex;
    }
}
