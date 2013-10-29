﻿using Microsoft.Kinect;
using System.Diagnostics;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{
    public class BothSwipeUpSegment0 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            // either hand is gripped.
            if (userInfo.HandPointers[0].HandEventType == InteractionHandEventType.Grip
                || userInfo.HandPointers[1].HandEventType == InteractionHandEventType.Grip)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
    }

    /// <summary>
    /// The first part of the both hands swipe up gesture for the right hand
    /// </summary>
    public class BothSwipeUpSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // both hands below head
            if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y
                && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
            {
                // right hand is right to the shoulder center. left hand is left to the shoulder center
                if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderCenter].Position.X
                    && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderCenter].Position.X)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }


    /// <summary>
    /// The second part of the both hands swipe up gesture for the right hand
    /// </summary>
    public class BothSwipeUpSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            // right hand is right to the shoulder center and head. left hand is left to the shoulder center and head.
            if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderCenter].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderCenter].Position.X)
            {
                // both hands above head
                if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.Head].Position.Y
                    && skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.Head].Position.Y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
    }

}

namespace KinectAdapter.Fizbin.Gestures
{
    public class BothSwipeUpGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "BothSwipeUp";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] bothSwipeUpSegments = new IRelativeGestureSegment[4];
            bothSwipeUpSegments[0] = new BothSwipeUpSegment0();
            bothSwipeUpSegments[1] = new BothSwipeUpSegment0();
            bothSwipeUpSegments[2] = new BothSwipeUpSegment1();
            bothSwipeUpSegments[3] = new BothSwipeUpSegment2();
            return bothSwipeUpSegments;
        }
    }
}