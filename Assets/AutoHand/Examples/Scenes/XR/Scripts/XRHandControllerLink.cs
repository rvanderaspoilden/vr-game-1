using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Autohand.Demo{
    public enum CommonButton {
        gripButton,
        menuButton,
        primaryButton,
        secondaryButton,
        triggerButton,
        primary2DAxisClick,
        primary2DAxisTouch,
        secondary2DAxisClick,
        secondary2DAxisTouch,
        primaryTouch,
        secondaryTouch,

    }
    
    public enum CommonAxis {
        trigger,
        grip
    }

    public enum Common2DAxis {
        primaryAxis,
        secondaryAxis
    }

    public class XRHandControllerLink : MonoBehaviour{
        public Hand hand;
        public CommonButton grabButton = CommonButton.triggerButton;
        public CommonAxis grabAxis = CommonAxis.trigger;
        public CommonButton squeezeButton = CommonButton.gripButton;

        XRNode role;
        bool squeezing;
        bool grabbing;
        InputDevice device;
        List<InputDevice> devices;

        private void Start(){
            if(grabButton == squeezeButton) {
                Debug.LogError("AUTOHAND: You are using the same button for grab and squeeze on HAND CONTROLLER LINK, this may create conflict or errors", this);
            }

            if(hand.left)
                role = XRNode.LeftHand;
            else
                role = XRNode.RightHand;
            devices = new List<InputDevice>();
        }

        void Update(){
            InputDevices.GetDevicesAtXRNode(role, devices);
            if(devices.Count > 0)
                device = devices[0];

            if(device != null && device.isValid){
                //Sets hand fingers wrap
                if(device.TryGetFeatureValue(GetCommonAxis(grabAxis), out float triggerOffset)) {
                    hand.SetGrip(triggerOffset);
                }

                //Grip input
                if(device.TryGetFeatureValue(GetCommonButton(grabButton), out bool grip)) {
                    if(grabbing && !grip){
                        hand.Release();
                        hand.gripOffset -= 0.8f;
                        grabbing = false;
                    }
                    else if(!grabbing && grip){
                        hand.Grab();
                        hand.gripOffset += 0.8f;
                        grabbing = true;
                    }
                }
                //Grip input
                if(device.TryGetFeatureValue(GetCommonButton(squeezeButton), out bool squeeze)) {
                    if(squeezing && !squeeze){
                        hand.Unsqueeze();
                        squeezing = false;
                    }
                    else if(!squeezing && squeeze){
                        hand.Squeeze();
                        squeezing = true;
                    }
                }
            }
        }
        public bool ButtonPressed(CommonButton button) {
            if(device.TryGetFeatureValue(GetCommonButton(button), out bool pressed)) {
                return pressed;
            }

            return false;
        }

        public float GetAxis(CommonAxis axis){
            if(device.TryGetFeatureValue(GetCommonAxis(axis), out float axisValue)) {
                return axisValue;
            }
            return 0;
        }

        public Vector2 GetAxis2D(Common2DAxis axis) {
            if(device.TryGetFeatureValue(GetCommon2DAxis(axis), out Vector2 axisValue)) {
                return axisValue;
            }
            return Vector2.zero;
        }
        

        public static InputFeatureUsage<bool> GetCommonButton(CommonButton button) {
            if(button == CommonButton.gripButton)
                return CommonUsages.gripButton;
            if(button == CommonButton.menuButton)
                return CommonUsages.menuButton;
            if(button == CommonButton.primary2DAxisClick)
                return CommonUsages.primary2DAxisClick;
            if(button == CommonButton.primary2DAxisTouch)
                return CommonUsages.primary2DAxisTouch;
            if(button == CommonButton.primaryButton)
                return CommonUsages.primaryButton;
            if(button == CommonButton.primaryTouch)
                return CommonUsages.primaryTouch;
            if(button == CommonButton.secondary2DAxisClick)
                return CommonUsages.secondary2DAxisClick;
            if(button == CommonButton.secondary2DAxisTouch)
                return CommonUsages.secondary2DAxisTouch;
            if(button == CommonButton.secondaryButton)
                return CommonUsages.secondaryButton;
            if(button == CommonButton.secondaryTouch)
                return CommonUsages.secondaryTouch;
            
            return CommonUsages.triggerButton;
        }

        public static InputFeatureUsage<float> GetCommonAxis(CommonAxis axis) {
            if(axis == CommonAxis.grip)
                return CommonUsages.grip;
            else
                return CommonUsages.trigger;
        }

        public static InputFeatureUsage<Vector2> GetCommon2DAxis(Common2DAxis axis) {
            if(axis == Common2DAxis.primaryAxis)
                return CommonUsages.primary2DAxis;
            else
                return CommonUsages.secondary2DAxis;
        }
    }
}
