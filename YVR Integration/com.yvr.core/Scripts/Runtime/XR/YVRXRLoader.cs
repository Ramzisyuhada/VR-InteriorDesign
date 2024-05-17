using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.XR;
using UnityEngine.XR.Management;

namespace YVR.Core.XR
{
    public class YVRXRLoader : XRLoaderHelper
    {
        [DllImport("yvrplugin")]
        private static extern void YVRSetXRUserDefinedSettings(ref YVRXRUserDefinedSettings userDefinedSettings);

        private static List<XRDisplaySubsystemDescriptor> displaySubsystemDescriptors = new List<XRDisplaySubsystemDescriptor>();
        private static List<XRInputSubsystemDescriptor> inputSubsystemDescriptors = new List<XRInputSubsystemDescriptor>();

        public override bool Initialize()
        {
            YVRXRUserDefinedSettings userDefinedSettings = new YVRXRUserDefinedSettings();

            userDefinedSettings.stereoRenderingMode = YVRXRSettings.xrSettings.GetStereoRenderingMode();
            userDefinedSettings.eyeRenderScale = YVRXRSettings.xrSettings.eyeResolutionScale;
            userDefinedSettings.use16BitDepthBuffer = YVRXRSettings.xrSettings.use16BitDepthBuffer;
            userDefinedSettings.useMonoscopic = YVRXRSettings.xrSettings.useMonoscopic;
            userDefinedSettings.useLinearColorSpace = QualitySettings.activeColorSpace == ColorSpace.Linear;
            YVRSetXRUserDefinedSettings(ref userDefinedSettings);

            CreateSubsystem<XRDisplaySubsystemDescriptor, XRDisplaySubsystem>(displaySubsystemDescriptors, "Display");
            CreateSubsystem<XRInputSubsystemDescriptor, XRInputSubsystem>(inputSubsystemDescriptors, "Tracking");
            return true;
        }

        public override bool Start()
        {
            StartSubsystem<XRDisplaySubsystem>();
            StartSubsystem<XRInputSubsystem>();
            return true;
        }

        public override bool Stop()
        {
            StopSubsystem<XRDisplaySubsystem>();
            StopSubsystem<XRInputSubsystem>();
            return true;
        }

        public override bool Deinitialize()
        {
            DestroySubsystem<XRDisplaySubsystem>();
            DestroySubsystem<XRInputSubsystem>();
            return true;
        }
    }
}