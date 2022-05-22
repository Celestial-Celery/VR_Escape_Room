using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
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

    private bool leftPinching = false;
    private bool rightPinching = false;

    private bool canRightSelect = true;
    private bool canLeftSelect = true;

    private SelectEnterEventArgs _leftSelectEnterArgs;
    private SelectEnterEventArgs _rightSelectEnterArgs;
    private ActivateEventArgs _leftActivateEventArgs;
    private ActivateEventArgs _rightActivateEventArgs;


    public static bool handsActive = false;
    

    private void Start()
    {
        _leftSelectEnterArgs = new SelectEnterEventArgs();
        _rightSelectEnterArgs = new SelectEnterEventArgs();
        _leftActivateEventArgs = new ActivateEventArgs();
        _rightActivateEventArgs = new ActivateEventArgs();

        _leftRayAnchor = GameObject.Find("[LeftHandAnchor] Ray Origin").transform;
        _rightRayAnchor = GameObject.Find("[RightHandAnchor] Ray Origin").transform;

        _leftHand = leftOVRHandPrefab.GetComponent<OVRHand>();
        _rightHand = rightOVRHandPrefab.GetComponent<OVRHand>();
    }

    private void Update()
    {
        updateHandState();
        updateAnchorPosition();
        checkFingerPinch();
        leftSelect();
        rightSelect();
    }

    private void updateAnchorPosition()
    {
        if (handsActive)
        {
            _leftRayAnchor.localEulerAngles = new Vector3(-60, 100, 0);//default -10 90 0 / 0.19 0.05 0
            _rightRayAnchor.localEulerAngles = new Vector3(60, -80, 0);//default 10 -90 0 / -0.19 -0.05 0

            _leftRayAnchor.localPosition = new Vector3(0.1f, 0.05f, 0.02f);
            _rightRayAnchor.localPosition = new Vector3(-0.1f, -0.05f, -0.02f);
        }
        else if (!handsActive)
        {
            _leftRayAnchor.localEulerAngles = Vector3.zero;
            _rightRayAnchor.localEulerAngles = Vector3.zero;

            _leftRayAnchor.localPosition = new Vector3(0, 0, 0.05220008f);
            _rightRayAnchor.localPosition = new Vector3(0, 0, 0.05220008f);
        }
    }

    private void updateHandState()
    {
        handsActive = _leftHand.IsTracked || _rightHand.IsTracked;
    }


    private void checkFingerPinch()
    {
        leftPinching = _leftHand.GetFingerIsPinching(HandFinger.Index);
        rightPinching = _rightHand.GetFingerIsPinching(HandFinger.Index);

        if (!rightPinching)
        {
            canRightSelect = true;
        }
        if (!leftPinching)
        {
            canLeftSelect = true;
        }
    }

    private void leftSelect()
    {
        IXRSelectInteractable _xRLeftSelectedObject = performLeftRaycast();

        if (leftPinching && canLeftSelect && _xRLeftSelectedObject != null)
        {
            _leftSelectEnterArgs.manager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
            _leftSelectEnterArgs.manager.SelectEnter(GameObject.Find("LeftHandAnchor").GetComponent<IXRSelectInteractor>(), _xRLeftSelectedObject);
            GameObject.Find("LeftHandAnchor").GetComponent<XRRayInteractor>().selectEntered = new SelectEnterEvent();
        }
    }

    private void rightSelect()
    {
        IXRSelectInteractable _xRRightSelectedObject = performRightRaycast();

        if (rightPinching && canRightSelect && _xRRightSelectedObject != null)
        {
            canRightSelect = false;
            _rightSelectEnterArgs.manager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
            _rightSelectEnterArgs.manager.SelectEnter(GameObject.Find("RightHandAnchor").GetComponent<IXRSelectInteractor>(), _xRRightSelectedObject);
            GameObject.Find("RightHandAnchor").GetComponent<XRRayInteractor>().selectEntered = new SelectEnterEvent();
        }
    }

    private IXRSelectInteractable performLeftRaycast()
    {
        IXRSelectInteractable _xRLeftSelectedObject = null;
        RaycastHit leftHit;

        if (Physics.Raycast(_leftRayAnchor.position, _leftRayAnchor.TransformDirection(Vector3.forward), out leftHit))
        {
            try
            {
                _xRLeftSelectedObject = leftHit.transform.gameObject.GetComponent<IXRSelectInteractable>();
            }
            catch{}
        }
        return _xRLeftSelectedObject;
    }    

    private IXRSelectInteractable performRightRaycast()
    {
        IXRSelectInteractable _xRRightSelectedObject = null;
        RaycastHit rightHit;

        if (Physics.Raycast(_rightRayAnchor.position, _rightRayAnchor.TransformDirection(Vector3.forward), out rightHit))
        {
            try
            {
                _xRRightSelectedObject = rightHit.transform.gameObject.GetComponent<IXRSelectInteractable>();
            }
            catch{}
        }
        return _xRRightSelectedObject;
    }

    private IXRActivateInteractable performLeftActivateRaycast()
    {
        IXRActivateInteractable _xRLeftSelectedObject = null;
        RaycastHit leftHit;

        if (Physics.Raycast(_leftRayAnchor.position, _leftRayAnchor.TransformDirection(Vector3.forward), out leftHit))
        {
            try
            {
                _xRLeftSelectedObject = leftHit.transform.gameObject.GetComponent<IXRActivateInteractable>();
            }
            catch { }
        }
        return _xRLeftSelectedObject;
    }

    private IXRActivateInteractable performRightActivateRaycast()
    {
        IXRActivateInteractable _xRRightSelectedObject = null;
        RaycastHit rightHit;

        if (Physics.Raycast(_rightRayAnchor.position, _rightRayAnchor.TransformDirection(Vector3.forward), out rightHit))
        {
            try
            {
                _xRRightSelectedObject = rightHit.transform.gameObject.GetComponent<IXRActivateInteractable>();
            }
            catch { }
        }
        return _xRRightSelectedObject;
    }
}
