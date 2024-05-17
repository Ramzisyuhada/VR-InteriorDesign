using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public partial class YVRPluginAndroid : YVRPlugin
    {
        [DllImport("yvrplugin")]
        private static extern IntPtr GetRenderEventFunc();

        [DllImport("yvrplugin")]
        private static extern IntPtr GetRenderEventAndDataFunc();

        [DllImport("yvrplugin")]
        private static extern void YVRSetVSyncCount(int vSyncCount);

        [DllImport("yvrplugin")]
        private static extern void YVRAddRenderLayer(int compositionDepth);

        [DllImport("yvrplugin")]
        private static extern void YVRRecenterPose();

        [DllImport("yvrplugin")]
        private static extern int YVRSetTrackingSpace(int trackingSpace);

        [DllImport("yvrplugin")]
        private static extern void YVRGetControllerState(uint controllerMask, ref ControllerState state);

        [DllImport("yvrplugin")]
        private static extern bool YVRGetControllerConnected(uint controllerMask, int frameCount);

        [DllImport("yvrplugin")]
        private static extern bool YVRGetControllerPositionTracked(uint controllerMask);

        [DllImport("yvrplugin")]
        private static extern bool YVRGetControllerOrientationTracked(uint controllerMask);

        [DllImport("yvrplugin")]
        private static extern bool YVRGetHeadsetPositionTracked();

        [DllImport("yvrplugin")]
        private static extern bool YVRGetHeadsetOrientationTracked();

        [DllImport("yvrplugin")]
        private static extern void YVRSetControllerVibration(uint controllerMask, float frequency, float amplitude);

        [DllImport("yvrplugin")]
        private static extern float YVRGetBatteryLevel();

        [DllImport("yvrplugin")]
        private static extern float YVRGetBatteryTemperature();

        [DllImport("yvrplugin")]
        private static extern int YVRGetBatteryStatus();

        [DllImport("yvrplugin")]
        private static extern float YVRGetVolumeLevel();

        [DllImport("yvrplugin")]
        private static extern bool YVRIsPowerSavingActive();

        [DllImport("yvrplugin")]
        private static extern float YVRGetGpuUtilization();

        [DllImport("yvrplugin")]
        private static extern float YVRGetCpuUtilization();

        [DllImport("yvrplugin")]
        private static extern void YVRSetPerformanceLevel(int cpulevel, int gpuLevel);

        [DllImport("yvrplugin")]
        private static extern int YVRGetCpuLevel();

        [DllImport("yvrplugin")]
        private static extern int YVRGetGpuLevel();

        [DllImport("yvrplugin")]
        private static extern void YVRSetUsingIPDInPositionTracking(bool usingIPD);

        [DllImport("yvrplugin")]
        private static extern float YVRGetDisplayFrequency();

        [DllImport("yvrplugin")]
        private static extern void YVRSetDisplayFrequency(float freshRate);

        [DllImport("yvrplugin")]
        private static extern int YVRGetDisplayAvailableFrequenciesNum();

        [DllImport("yvrplugin")]
        private static extern void YVRGetDisplayAvailableFrequencies(float[] frequenciesArray);

        [DllImport("yvrplugin")]
        private static extern void YVRGetLatencyData(ref YVRCameraRenderer.LatencyData latencyData);

        [DllImport("yvrplugin")]
        private static extern void YVRGetEyeResolution(ref Vector2 resolution);

        [DllImport("yvrplugin")]
        private static extern void YVRGetEyeFov(int eyeSide, ref YVRCameraRenderer.EyeFov eyeFov);

        [DllImport("yvrplugin")]
        private static extern bool YVRIsUserPresent();

        [DllImport("yvrplugin")]
        private static extern bool YVRIsRecenterOccurred();

        [DllImport("yvrplugin")]
        private static extern bool YVRGetBoundaryConfigured();

        [DllImport("yvrplugin")]
        private static extern void YVRTestBoundaryNode(YVRBoundary.BoundaryNode targetNode, ref YVRBoundary.BoundaryTestResult testResult);

        [DllImport("yvrplugin")]
        private static extern void YVRTestBoundaryPoint(Vector3 targetPoint, ref YVRBoundary.BoundaryTestResult testResult);

        [DllImport("yvrplugin")]
        private static extern Vector3 YVRGetBoundaryDimensions();

        [DllImport("yvrplugin")]
        private static extern bool YVRGetBoundaryVisible();

        [DllImport("yvrplugin")]
        private static extern void YVRSetBoundaryVisible(bool visible);

        [DllImport("yvrplugin")]
        private static extern int YVRGetBoundaryGeometryPointsCount();

        [DllImport("yvrplugin")]
        private static extern void YVRGetBoundaryGeometry(Vector3[] geometry);


        //---------------------------------------------------------------------------------------------

        public static YVRPluginAndroid Create()
        {
            return new YVRPluginAndroid();
        }

        public override void SetTrackingSpace(YVRTrackingStateManager.TrackingSpace trackingSpace)
        {
            YVRSetTrackingSpace((int)trackingSpace);
        }

        public override void SetVSyncCount(YVRQualityManager.VSyncCount vSyncCount)
        {
            YVRSetVSyncCount((int)vSyncCount);
        }

        public override void RecenterTracking()
        {
            YVRRecenterPose();
        }

        public override void SetUsingIPDInPositionTracking(bool usingIPD)
        {
            YVRSetUsingIPDInPositionTracking(usingIPD);
        }

        public override ControllerState GetControllerState(uint controllerMask)
        {
            ControllerState state = new ControllerState();
            YVRGetControllerState(controllerMask, ref state);
            return state;
        }

        public override bool GetControllerConnected(uint controllerMask)
        {
            return YVRGetControllerConnected(controllerMask, Time.frameCount);
        }

        public override bool GetControllerPositionTracked(uint controllerMask)
        {
            return YVRGetControllerPositionTracked(controllerMask);
        }

        public override bool GetControllerOrientationTracked(uint controllerMask)
        {
            return YVRGetControllerOrientationTracked(controllerMask);
        }

        public override bool GetHeadsetPositionTracked()
        {
            return YVRGetHeadsetPositionTracked();
        }

        public override bool GetHeadsetOrientationTracked()
        {
            return YVRGetHeadsetOrientationTracked();
        }

        public override void SetControllerVibration(uint controllerMask, float frequency, float amplitude)
        {
            YVRSetControllerVibration(controllerMask, frequency, amplitude);
        }

        public override float GetBatteryLevel()
        {
            return YVRGetBatteryLevel();
        }

        public override float GetBatteryTemperature()
        {
            return YVRGetBatteryTemperature();
        }

        public override int GetBatteryStatus()
        {
            return YVRGetBatteryStatus();
        }

        public override float GetVolumeLevel()
        {
            return YVRGetVolumeLevel();
        }

        public override bool IsPowerSavingActive()
        {
            return YVRIsPowerSavingActive();
        }

        public override float GetGPUUtilLevel()
        {
            return YVRGetGpuUtilization();
        }

        public override float GetCPUUtilLevel()
        {
            return YVRGetCpuUtilization();
        }

        public override int GetCPULevel()
        {
            return (int)YVRGetCpuLevel();
        }

        public override int GetGPULevel()
        {
            return (int)YVRGetGpuLevel();
        }

        public override void SetPerformanceLevel(int cpuLevel, int gpuLevel)
        {
            YVRSetPerformanceLevel(cpuLevel, gpuLevel);
        }

        public override void GetEyeResolution(ref Vector2 resolution)
        {
            YVRGetEyeResolution(ref resolution);
        }

        public override void GetEyeFov(int eyeSide, ref YVRCameraRenderer.EyeFov eyeFov)
        {
            YVRGetEyeFov(eyeSide, ref eyeFov);
        }

        private float[] cacheAvailableFrequencies = null;

        public override float[] GetDisplayFrequenciesAvailable()
        {
            if (cacheAvailableFrequencies == null)
            {
                int availableFrequenciesNum = YVRGetDisplayAvailableFrequenciesNum();
                cacheAvailableFrequencies = new float[availableFrequenciesNum];
                YVRGetDisplayAvailableFrequencies(cacheAvailableFrequencies);
            }

            return cacheAvailableFrequencies;
        }

        public override float GetDisplayFrequency() { return YVRGetDisplayFrequency(); }
        public override void SetDisplayFrequency(float displayFrequency) { YVRSetDisplayFrequency(displayFrequency); }


        public override YVRCameraRenderer.LatencyData GetLatencyData()
        {
            YVRCameraRenderer.LatencyData latencyData = new YVRCameraRenderer.LatencyData();
            YVRGetLatencyData(ref latencyData);
            return latencyData;
        }

        public override bool IsUserPresent()
        {
            return YVRIsUserPresent();
        }

        public override bool IsRecenterOccurred()
        {
            return YVRIsRecenterOccurred();
        }

        public override bool GetBoundaryConfigured()
        {
            return YVRGetBoundaryConfigured();
        }

        public override void TestBoundaryNode(YVRBoundary.BoundaryNode targetNode, ref YVRBoundary.BoundaryTestResult testResult)
        {
            YVRTestBoundaryNode(targetNode, ref testResult);
        }

        public override void TestBoundaryPoint(Vector3 targetPoint, ref YVRBoundary.BoundaryTestResult testResult)
        {
            YVRTestBoundaryPoint(targetPoint, ref testResult);
        }

        public override Vector3 GetBoundaryDimensions()
        {
            return YVRGetBoundaryDimensions();
        }

        public override bool GetBoundaryVisible()
        {
            return YVRGetBoundaryVisible();
        }

        public override void SetBoundaryVisible(bool visible)
        {
            YVRSetBoundaryVisible(visible);
        }

        public override void AddRenderLayer(int compositionDepth)
        {
            YVRAddRenderLayer(compositionDepth);
        }

        public override Vector3[] GetBoundaryGeometry()
        {
            int pointsCount = YVRGetBoundaryGeometryPointsCount();
            Vector3[] result = new Vector3[pointsCount];
            if (pointsCount > 0)
                YVRGetBoundaryGeometry(result);

            return result;
        }
    }
}