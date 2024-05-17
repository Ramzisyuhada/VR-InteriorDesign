﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR;

namespace YVR.Core
{
    /// <summary>
    /// Encapsulate all controllers rigid related operations and information
    /// </summary>
    public class YVRControllerRig : YVRBaseRig
    {
        private InputDevice _leftControllerDevice = default;
        private InputDevice leftControllerDevice => _leftControllerDevice == default ? (_leftControllerDevice = yvrXRDevices.Find(device => device.name == "LeftController")) : _leftControllerDevice;

        private InputDevice _rightControllerDevice = default;
        private InputDevice rightControllerDevice => _rightControllerDevice == default ? (_rightControllerDevice = yvrXRDevices.Find(device => device.name == "RightController")) : _rightControllerDevice;

        /// <summary>
        /// Transform of right controller
        /// </summary>
        public Transform leftControllerAnchor { get; set; }

        /// <summary>
        /// Transform of left controller
        /// </summary>
        public Transform rightControllerAnchor { get; set; }


        /// <summary>
        /// Override @YVR.Core.YVRBaseRig.InitializeAnchor
        /// </summary>
        protected override void InitializeAnchor()
        {
            base.InitializeAnchor();
            leftControllerAnchor = leftControllerAnchor ?? ConfigureAnchor(trackingSpace, "LeftControllerAnchor");
            rightControllerAnchor = rightControllerAnchor ?? ConfigureAnchor(trackingSpace, "RightControllerAnchor");
        }
        [ExcludeFromDocs]
        public override void UpdateAnchorPose()
        {
            UpdateControllerPose();

            leftControllerAnchor.localPosition = lControllerPose.position;
            leftControllerAnchor.localRotation = lControllerPose.orientation;

            rightControllerAnchor.localPosition = rControllerPose.position;
            rightControllerAnchor.localRotation = rControllerPose.orientation;
        }

        private void UpdateControllerPose()
        {
            if (leftControllerDevice.isValid)
            {
                leftControllerDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 lControllerPosition);
                leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion lControllerRotation);

                lControllerPose.position = lControllerPosition;
                lControllerPose.orientation = lControllerRotation;
            }
            else
                LoadSimulatedLeftControllerPose();

            if (rightControllerDevice.isValid)
            {
                rightControllerDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rControllerPosition);
                rightControllerDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rControllerRotation);

                rControllerPose.position = rControllerPosition;
                rControllerPose.orientation = rControllerRotation;
            }
            else
                LoadSimulatedRightControllerPose();
        }

        private void LoadSimulatedLeftControllerPose()
        {
            if (!YVRControllerEmulator.instance.isEmulatingLeftController) return;

            lControllerPose.position = YVRControllerEmulator.instance.currentControllerPosition;
            lControllerPose.orientation = Quaternion.Euler(YVRControllerEmulator.instance.currentControllerRotation);
        }

        private void LoadSimulatedRightControllerPose()
        {
            if (!YVRControllerEmulator.instance.isEmulatingRightController) return;
            rControllerPose.position = YVRControllerEmulator.instance.currentControllerPosition;
            rControllerPose.orientation = Quaternion.Euler(YVRControllerEmulator.instance.currentControllerRotation);
        }

        /// <summary>
        /// Whether controller's orientation is tracked
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> true if <paramref name="controllerType"/>'s orientation is tracked </returns>
        public static bool GetOrientationTracked(ControllerType controllerType)
        {
            switch (controllerType)
            {
                case ControllerType.LeftTouch:
                    return YVRPlugin.Instance.GetControllerOrientationTracked(0);
                case ControllerType.RightTouch:
                    return YVRPlugin.Instance.GetControllerOrientationTracked(1);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Whether controller's position is tracked
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> true if <paramref name="controllerType"/>'s position is tracked </returns>
        public static bool GetPositionTracked(ControllerType controllerType)
        {
            switch (controllerType)
            {
                case ControllerType.LeftTouch:
                    return YVRPlugin.Instance.GetControllerPositionTracked(0);
                case ControllerType.RightTouch:
                    return YVRPlugin.Instance.GetControllerPositionTracked(1);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Get controller's position
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s position </returns>
        public static Vector3 GetPosition(ControllerType controllerType)
        {
            bool succeed = yvrXRDevices[GetXRDeviceNodeID(controllerType)].TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get controller's velocity
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s velocity </returns>
        public static Vector3 GetVelocity(ControllerType controllerType)
        {
            bool succeed = yvrXRDevices[GetXRDeviceNodeID(controllerType)].TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get controller's acceleration
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s acceleration </returns>
        public static Vector3 GetAcceleration(ControllerType controllerType)
        {
            bool succeed = yvrXRDevices[GetXRDeviceNodeID(controllerType)].TryGetFeatureValue(CommonUsages.deviceAcceleration, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get controller's rotation
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s rotation </returns>
        public static Quaternion GetRotation(ControllerType controllerType)
        {
            bool succeed = yvrXRDevices[GetXRDeviceNodeID(controllerType)].TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion result);
            return succeed ? result : Quaternion.identity;
        }

        /// <summary>
        /// Get controller's angular angular velocity
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s angular velocity </returns>
        public static Vector3 GetAngularVelocity(ControllerType controllerType)
        {
            bool succeed = yvrXRDevices[GetXRDeviceNodeID(controllerType)].TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get controller's angular acceleration
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s acceleration </returns>
        public static Vector3 GetAngularAcceleration(ControllerType controllerType)
        {
            bool succeed = yvrXRDevices[GetXRDeviceNodeID(controllerType)].TryGetFeatureValue(CommonUsages.deviceAngularAcceleration, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        private static int GetXRDeviceNodeID(ControllerType controllerType)
        {
            return (int)(controllerType == ControllerType.LeftTouch ? YVRXRDeviceNode.LeftController : YVRXRDeviceNode.RightController);
        }
    }
}