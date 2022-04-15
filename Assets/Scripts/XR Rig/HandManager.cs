using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject leftOVRControllerPrefab;
    public GameObject rightOVRControllerPrefab;
    public GameObject leftHandAnchor;
    public GameObject rightHandAnchor;

    private Transform _leftRayAnchor;
    private Transform _rightRayAnchor;

    private static bool handsActive = false;

    private void Start()
    {
        _leftRayAnchor = GameObject.Find("[LeftHandAnchor] Ray Origin").transform;
        _rightRayAnchor = GameObject.Find("[RightHandAnchor] Ray Origin").transform;
    }

    private void Update()
    {
        updateAnchorPosition();
    }

    private void updateAnchorPosition()
    {
        if (IsDisabled(leftOVRControllerPrefab) && IsDisabled(rightOVRControllerPrefab))
        {
            _leftRayAnchor.localEulerAngles = new Vector3(-10, 90, 0);
            _rightRayAnchor.localEulerAngles = new Vector3(10, -90, 0);

            _leftRayAnchor.localPosition = new Vector3(0.19f, 0.05f, 0);
            _rightRayAnchor.localPosition = new Vector3(-0.19f, -0.05f, 0);

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

    private bool IsDisabled(GameObject controller)
    {
        for (int i = 0; i < controller.transform.childCount; i++)
        {
            if (controller.transform.GetChild(i).gameObject.activeSelf)
            {
                Debug.Log("Controllers active");
                return false;                
            }
        }
        return true;
    }
}
