using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public partial class YVRPluginWin : YVRPlugin
    {
        public static YVRPluginWin Create()
        {
            return new YVRPluginWin();
        }

        public override void SetVSyncCount(YVRQualityManager.VSyncCount vSyncCount)
        {
            QualitySettings.vSyncCount = (int)vSyncCount;
        }

        public override void GetEyeResolution(ref Vector2 resolution)
        {
            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }

        public override void GetEyeFov(int eyeSide, ref YVRCameraRenderer.EyeFov eyeFov)
        {
            eyeFov.UpFov = eyeFov.DownFov = eyeFov.LeftFov = eyeFov.RightFov = 45;
        }

        public override ControllerState GetControllerState(uint controllerMask)
        {
            if (YVRControllerEmulator.instance && (((uint)YVRControllerEmulator.instance.targetController == controllerMask)))
                return YVRControllerEmulator.instance.controllerState;
            return new ControllerState();
        }

        public override bool GetControllerConnected(uint controllerMask)
        {
            return true;
        }
    }
}