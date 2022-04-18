using UnityEngine;

public class Pillow : MonoBehaviour
{
    public bool HasKey;


    void Awake()
    {
        HasKey = false;
    }    

    public void Select()
    {
        Couch.Select(this);
    }
}
