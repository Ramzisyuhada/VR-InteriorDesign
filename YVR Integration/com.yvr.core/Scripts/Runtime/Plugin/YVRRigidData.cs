using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public enum DeviceNode
    {
        /// <summary>
        /// 
        /// </summary>
        None = -1,
        /// <summary>
        /// Node representing the left hand.
        /// </summary>
        ControllerLeft,
        /// <summary>
        /// Node representing the right hand.
        /// </summary>
        ControllerRight,
        /// <summary>
        /// Node representing a point between the left and right eyes.
        /// </summary>
        EyeCenter,
        /// <summary>
        ///  Node representing the left eye.
        /// </summary>
        EyeLeft,
        /// <summary>
        ///  Node representing the right eye.
        /// </summary>
        EyeRight,
        /// <summary>
        /// The Count of DeviceNode
        /// </summary>
        Count
    }
    [ExcludeFromDocs]
    public enum Step
    {
        Render = -1,
        Physics = 0,
    }

    [ExcludeFromDocs]
    [StructLayout(LayoutKind.Sequential)]
    public struct PoseData
    {
        public Quaternion orientation;
        public Vector3 position;
    }
}