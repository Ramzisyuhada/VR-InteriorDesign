using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace YVR.Core
{
    /// <summary>
    /// Base class of rigid class
    /// </summary>
    public class YVRBaseRig : MonoBehaviour
    {
        /// <summary>
        /// Equals to MonoBehavior.tranform
        /// </summary>
        /// <remarks> To save the performance cost of MonoBehavior.transform </remarks>
        protected new Transform transform = null;

        /// <summary>
        /// Cache for YVRManager
        /// </summary>
        protected YVRManager yvrManager = null;

        /// <summary>
        /// The parent of all tracking anchors
        /// </summary>
        public Transform trackingSpace { get; private set; }

        [ExcludeFromDocs]
        protected enum YVRXRDeviceNode { Headset = 0, LeftController = 1, RightController = 2 }

        [ExcludeFromDocs]
        protected static PoseData hmdPose, leftEyePose, rightEyePose, lControllerPose, rControllerPose;
        [ExcludeFromDocs]
        protected static List<InputDevice> yvrXRDevices = null;

        /// <summary>
        /// Initialize function which will be triggered by @YVR.Core.YVRManager
        /// </summary>
        /// <param name="yvrManager">The @YVR.Core.YVRManager trigger  </param>
        public virtual void Initialize(YVRManager yvrManager)
        {
            this.transform = base.transform;
            this.yvrManager = yvrManager;
            yvrManager.onUpdate += UpdateAnchorPose;
            Application.onBeforeRender += UpdateAnchorPose;
            InitializeAnchor();

            if (yvrXRDevices == null)
            {
                yvrXRDevices = new List<InputDevice>();
                InputDevices.GetDevices(yvrXRDevices);
            }
        }

        /// <summary>
        /// Initialization function for derived classes to override
        /// </summary>
        protected virtual void InitializeAnchor()
        {
            trackingSpace = trackingSpace ?? ConfigureAnchor(null, "TrackingSpace");
        }
        /// <summary>
        /// Get the pose of the anchors and assign it to each anchor
        /// </summary>
        public virtual void UpdateAnchorPose() { }

        /// <summary>
        /// Configure an anchor
        /// </summary>
        /// <param name="parent"> Target anchor's parent </param>
        /// <param name="anchorName"> Target anchor's name </param>
        /// <returns> Configured transform </returns>
        protected Transform ConfigureAnchor(Transform parent, string anchorName)
        {
            Transform anchor = parent?.Find(anchorName);
            if (!anchor) anchor = transform.Find(anchorName);
            if (!anchor) anchor = new GameObject(anchorName).transform;

            anchor.name = anchorName;
            anchor.parent = parent ?? transform;
            anchor.localPosition = Vector3.zero;
            anchor.localRotation = Quaternion.identity;
            anchor.localScale = Vector3.one;

            return anchor;
        }
        /// <summary>
        /// Equals to MonoBehavior.OnDestroy
        /// </summary>
        protected virtual void OnDestroy()
        {
            yvrManager.onUpdate -= UpdateAnchorPose;
            Application.onBeforeRender -= UpdateAnchorPose;
        }
    }
}