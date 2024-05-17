﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YVR.Core.XR;

namespace YVR.Core
{
    /// <summary>
    /// The general manager class of whole sdk which holds other managers and controls the lifecycle of the vr mode
    /// </summary>
    public class YVRManager : MonoBehaviorSingleton<YVRManager>
    {
        /// <summary>
        /// The instance of class @YVR.Core.YVRControllerRig
        /// </summary>
        public YVRControllerRig controllerRig { get; private set; }

        /// <summary>
        /// The instance of class @YVR.Core.YVRCameraRig
        /// </summary>
        public YVRCameraRig cameraRig { get; private set; }

        /// <summary>
        /// The instance of class @YVR.Core.YVRCameraRenderer
        /// </summary>
        public YVRCameraRenderer cameraRenderer = new YVRCameraRenderer();

        /// <summary>
        /// The instance of class @YVR.Core.YVRBoundary
        /// </summary>
        public YVRBoundary boundary = new YVRBoundary();

        /// <summary>
        /// The instance of class @YVR.Core.YVRQualityManager
        /// </summary>
        public YVRQualityManager qualityManager = new YVRQualityManager();

        /// <summary>
        /// The instance of class @YVR.Core.YVRTrackingStateManager
        /// </summary>
        public YVRTrackingStateManager trackingManager = new YVRTrackingStateManager();

        /// <summary>
        /// The instance of class @YVR.Core.YVRPerformanceManager
        /// </summary>
        public YVRPerformanceManager performanceManager = new YVRPerformanceManager();

        /// <summary>
        /// The instance of class @YVR.Core.YVRHMDManager
        /// </summary>
        public YVRHMDManager hmdManager = new YVRHMDManager();

        /// <summary>
        /// Occurs at the update function of every frame
        /// </summary>
        public event Action onUpdate = null;

        /// <summary>
        /// Occurs when head gained tracking.
        /// </summary>
        public event Action onTrackingAcquired = null;

        /// <summary>
        /// Occurs when head lost tracking.
        /// </summary>
        public event Action onTrackingLost = null;

        /// <summary>
        /// Occurs when an HMD is put on the user's head.
        /// </summary>
        public event Action onHMDMounted = null;

        /// <summary>
        /// Occurs when an HMD is taken off the user's head.
        /// </summary>
        public event Action onHMDUnMounted = null;

        /// <summary>
        /// Occurs when recenter occurred
        /// </summary>
        public event Action onRecenterOccurred = null;

        /// <summary>
        /// The position offset of the center eye
        /// </summary>
        public Vector3 headPoseRelativeOffsetTranslation { get; set; }

        /// <summary>
        /// The rotation offset of the center eye (in euler angle)
        /// </summary>
        public Vector3 headPoseRelativeOffsetRotation { get; set; }

        private bool wasHMDTracking = false;
        private bool wasUserPresent = false;
        private bool wasRecenterOccurred = false;


        #region External Settings

        /// <summary>
        /// Set or get current cpu level(0-4), see also @YVR.Core.YVRPerformanceManager
        /// </summary>
        public int cpuLevel
        {
            get { return performanceManager.cpuLevel; }
            set { performanceManager.cpuLevel = value; }
        }

        /// <summary>
        /// Set or get current gpu level(0-5), see also @YVR.Core.YVRPerformanceManager
        /// </summary>
        public int gpuLevel
        {
            get { return performanceManager.gpuLevel; }
            set { performanceManager.gpuLevel = value; }
        }

        /// <summary>
        /// Set or get current vSync count, see also @YVR.Core.YVRQualityManager
        /// </summary>
        public YVRQualityManager.VSyncCount vSyncCount
        {
            get { return qualityManager.vSyncCount; }
            set { qualityManager.vSyncCount = value; }
        }

        /// <summary>
        /// Set or get current fixed foveated rendering level, see also @YVR.Core.YVRQualityManager
        /// </summary>
        public YVRQualityManager.FixedFoveatedRenderingLevel fixedFoveatedRenderingLevel
        {
            get { return qualityManager.fixedFoveatedRenderingLevel; }
            set { qualityManager.fixedFoveatedRenderingLevel = value; }
        }

        /// <summary>
        /// Get whether fixed foveation rendering is enabled or not
        /// </summary>
        public bool FixedFoveationRenderingEnabled => fixedFoveatedRenderingLevel != YVRQualityManager.FixedFoveatedRenderingLevel.Off;

        /// <summary>
        /// Get whether to use recommend MSAA level, see also @YVR.Core.YVRQualityManager
        /// </summary>
        public bool useRecommendedMSAALevel => qualityManager.useRecommendedMSAALevel;

        /// <summary>
        /// Set or get whether to consider ipd camera's position tracking, see also @YVR.Core.YVRTrackingStateManager
        /// </summary>
        public bool useIPDInPositionTracking
        {
            get { return trackingManager.useIPDInPositionTracking; }
            set { trackingManager.useIPDInPositionTracking = value; }
        }

        public YVRTrackingStateManager.TrackingSpace trackingSpace
        {
            get { return trackingManager.trackingSpace; }
            set { trackingManager.trackingSpace = value; }
        }

        /// <summary>
        /// Get current battery level, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float batteryLevel => hmdManager.batteryLevel;

        /// <summary>
        /// Get current battery temperature, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float batteryTemperature => hmdManager.batteryTemperature;

        /// <summary>
        /// Get current battery status(charge or not), see also @YVR.Core.YVRHMDManager
        /// </summary>
        public int batteryStatus => hmdManager.batteryStatus;

        /// <summary>
        /// Get current volume level, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float volumeLevel => hmdManager.volumeLevel;

        /// <summary>
        /// Get whether currently is in power saving mode or not, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public bool isPowerSavingActive => hmdManager.isPowerSavingActive;

        /// <summary>
        /// Get whether user is currently wearing the display, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public bool isUserPresent => hmdManager.isUserPresent;

        /// <summary>
        /// Get current gpu utilizing level(0.0-1.0), see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float gpuUtilLevel => performanceManager.gpuUtilLevel;

        /// <summary>
        /// Get current cpu utilizing level（0.0-1.0）, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float cpuUtilLevel => performanceManager.cpuUtilLevel;

        #endregion

        /// <summary>
        /// The override function of @YVR.Core.MonoBehaviorSingleton`1.Init
        /// </summary>
        protected override void Init()
        {
            base.Init();

            cameraRig = this.AutoAddingGetComponent<YVRCameraRig>();
            controllerRig = this.AutoAddingGetComponent<YVRControllerRig>();

            trackingManager.Initialize();
            qualityManager.Initialize();
            cameraRig.Initialize(this);
            controllerRig.Initialize(this);
            cameraRenderer.Initialize(this);

            if (useRecommendedMSAALevel) QualitySettings.antiAliasing = qualityManager.recommendAntiAlisingLevel;
        }


        private void Update()
        {
            YVRInput.Update();

            onUpdate?.Invoke();

            TriggerTrackingEvent();
            TriggerUserPresentEvent();
            TriggerRecenterOccurredEvent();
        }

        private void TriggerTrackingEvent()
        {
            bool isPositionTracked = YVRCameraRig.GetPositionTracked();

            if (!wasHMDTracking && isPositionTracked)
                onTrackingAcquired?.SafeInvoke();
            if (wasHMDTracking && !isPositionTracked)
                onTrackingLost?.SafeInvoke();

            wasHMDTracking = isPositionTracked;
        }

        private void TriggerUserPresentEvent()
        {
            bool isUserPresent = hmdManager.isUserPresent;

            if (!wasUserPresent && isUserPresent)
                onHMDMounted?.SafeInvoke();
            if (wasUserPresent && !isUserPresent)
                onHMDUnMounted?.SafeInvoke();

            wasUserPresent = isUserPresent;
        }

        private void TriggerRecenterOccurredEvent()
        {
            bool isRecenterOccurred = YVRCameraRig.IsRecenterOccurred();
            if (!wasRecenterOccurred && isRecenterOccurred)
                onRecenterOccurred?.SafeInvoke();

            wasRecenterOccurred = isRecenterOccurred;
        }
    }
}