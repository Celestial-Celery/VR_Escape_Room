using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFingertips : MonoBehaviour
{
    public Transform HandBase;
    public Transform Thumb;
    public Transform Index;
    public Transform Middle;
    public Transform Ring;
    public Transform Pinky;

    public OVRSkeleton Hand;

    void Update()
    {
        if (HandManager.handsActive)
        {
            HandBase.transform.position = Hand.Bones[0].Transform.position;
            Thumb.transform.position = Hand.Bones[19].Transform.position;
            Index.transform.position = Hand.Bones[20].Transform.position;
            Middle.transform.position = Hand.Bones[21].Transform.position;
            Ring.transform.position = Hand.Bones[22].Transform.position;
            Pinky.transform.position = Hand.Bones[23].Transform.position;
        }        
    }
}
