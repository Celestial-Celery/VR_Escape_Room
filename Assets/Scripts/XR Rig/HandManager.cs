using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRHand;

public class HandManager : MonoBehaviour
{
    public GameObject leftOVRControllerPrefab;
    public GameObject rightOVRControllerPrefab;

    public GameObject leftOVRHandPrefab;
    public GameObject rightOVRHandPrefab;

    public GameObject leftHandAnchor;
    public GameObject rightHandAnchor;

    private Transform _leftRayAnchor;
    private Transform _rightRayAnchor;

    private OVRHand _leftHand;
    private OVRHand _rightHand;

    private static bool leftPinching = false;
    private static bool rightPinching = false;

    private static bool handsActive = false;

    private void Start()
    {
        _leftRayAnchor = GameObject.Find("[LeftHandAnchor] Ray Origin").transform;
        _rightRayAnchor = GameObject.Find("[RightHandAnchor] Ray Origin").transform;

        _leftHand = leftOVRHandPrefab.GetComponent<OVRHand>();
        _rightHand = rightOVRHandPrefab.GetComponent<OVRHand>();        
    }

    private void Update()
    {
        updateAnchorPosition();
        checkFingerPinch();
    }

    private void updateAnchorPosition()
    {
        if (IsDisabled(leftOVRControllerPrefab) && IsDisabled(rightOVRControllerPrefab))
        {
            _leftRayAnchor.localEulerAngles = new Vector3(-60, 100, 0);//default -10 90 0 / 0.19 0.05 0
            _rightRayAnchor.localEulerAngles = new Vector3(60, -80, 0);//default 10 -90 0 / -0.19 -0.05 0

            _leftRayAnchor.localPosition = new Vector3(0.1f, 0.05f, 0.02f);
            _rightRayAnchor.localPosition = new Vector3(-0.1f, -0.05f, -0.02f);

            handsActive = true;
        }
        else if (!IsDisabled(leftOVRControllerPrefab) && !IsDisabled(rightOVRControllerPrefab))
        {
            _leftRayAnchor.localEulerAngles = Vector3.zero;
            _rightRayAnchor.localEulerAngles = Vector3.zero;

            _leftRayAnchor.localPosition = new Vector3(0, 0, 0.05220008f);
            _rightRayAnchor.localPosition = new Vector3(0, 0, 0.05220008f);

            handsActive = false;
        }
    }

    private void checkFingerPinch()
    {
        leftPinching = _leftHand.GetFingerIsPinching(HandFinger.Index);
        rightPinching = _rightHand.GetFingerIsPinching(HandFinger.Index);
    }

    private bool IsDisabled(GameObject controller)
    {
        for (int i = 0; i < controller.transform.childCount; i++)
        {
            if (controller.transform.GetChild(i).gameObject.activeSelf)
            {
                return false;                
            }
        }
        return true;
    }
}
