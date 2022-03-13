using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    public bool hasKey = false;
    public bool isSelected = false;

    void Awake()
    {
        hasKey = false;
        isSelected = false;
    }

    public void Select()
    {
        isSelected = true;
    }
}
