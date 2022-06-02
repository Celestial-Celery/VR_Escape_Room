using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

enum VisibilityState {Visible, Hidden}

public class ToggleControllerVisibility : MonoBehaviour
{
    public GameObject leftHandAnchor;
    public GameObject rightHandAnchor;
    public GameObject leftOVRControllerPrefab;
    public GameObject rightOVRControllerPrefab;

    private static VisibilityState _leftstate = VisibilityState.Visible;
    private static VisibilityState _rightstate = VisibilityState.Visible;
    private XRInteractorLineVisual _leftRay;
    private XRInteractorLineVisual _rightRay;
    private ActionBasedController _leftController;
    private ActionBasedController _rightController;
    private Vector3 _leftControllerPosition;
    private Vector3 _rightControllerPosition;

    void Awake()
    {
        _leftControllerPosition = leftHandAnchor.transform.position;
        _rightControllerPosition = rightHandAnchor.transform.position;
    }

    void Start()
    {
        _leftRay = leftHandAnchor.GetComponent<XRInteractorLineVisual>();
        _rightRay = rightHandAnchor.GetComponent<XRInteractorLineVisual>();

        _leftController = leftHandAnchor.GetComponent<ActionBasedController>();
        _rightController = rightHandAnchor.GetComponent<ActionBasedController>();
    }

    private void Update()
    {
        DisableInactiveControllers();
    }


    private void DisableInactiveControllers()
    {
        Vector3 leftControllerVelocity = (leftHandAnchor.transform.position - _leftControllerPosition) / Time.deltaTime;
        _leftControllerPosition = leftHandAnchor.transform.position;

        Vector3 rightControllerVelocity = (rightHandAnchor.transform.position - _rightControllerPosition) / Time.deltaTime;
        _rightControllerPosition = rightHandAnchor.transform.position;

        Debug.Log(rightControllerVelocity.magnitude);

        if (!HandManager.handsActive)
        {
            if (leftControllerVelocity.magnitude >= 0.001f)
            {
                leftOVRControllerPrefab.SetActive(true);
                _leftRay.enabled = true;
            }
            else
            {
                leftOVRControllerPrefab.SetActive(false);
                _leftRay.enabled = false;
            }

            if (rightControllerVelocity.magnitude >= 0.001f)
            {
                rightOVRControllerPrefab.SetActive(true);
                _rightRay.enabled = true;
            }
            else
            {
                rightOVRControllerPrefab.SetActive(false);
                _rightRay.enabled = false;
            }
        }
        else
        {
            _leftRay.enabled = true;
            _rightRay.enabled = true;
        }
    }

    public void toggleLeft()
    {
        if (!HandManager.handsActive)
        {
            switch (_leftstate)
            {
                case VisibilityState.Visible:
                    _leftRay.enabled = false;
                    leftOVRControllerPrefab.SetActive(false);
                    _leftstate = VisibilityState.Hidden;
                    break;
                case VisibilityState.Hidden:
                    _leftRay.enabled = true;
                    leftOVRControllerPrefab.SetActive(true);
                    _leftstate = VisibilityState.Visible;
                    break;
            }
        }        
    }
    public void toggleRight()
    {
        if (!HandManager.handsActive)
        {
            switch (_rightstate)
            {
                case VisibilityState.Visible:
                    _rightRay.enabled = false;
                    rightOVRControllerPrefab.SetActive(false);
                    _rightstate = VisibilityState.Hidden;
                    break;
                case VisibilityState.Hidden:
                    _rightRay.enabled = true;
                    rightOVRControllerPrefab.SetActive(true);
                    _rightstate = VisibilityState.Visible;
                    break;
            }
        }        
    }
}
