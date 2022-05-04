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

    void Start()
    {
        _leftRay = leftHandAnchor.GetComponent<XRInteractorLineVisual>();
        _rightRay = rightHandAnchor.GetComponent<XRInteractorLineVisual>();
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
    public void turnOffLeftRay()
    {
        _leftRay.enabled = false;
    }
    public void turnOnLeftRay()
    {
        _leftRay.enabled = true;
    }
    public void turnOffRightRay()
    {
        _rightRay.enabled = false;
    }
    public void turnOnRightRay()
    {
        _rightRay.enabled = true;
    }

}

