using UnityEngine;

public class Pillow : MonoBehaviour
{
    public bool HasKey;

    public Couch Couch;

    void Awake()
    {
        HasKey = false;
    }    

    public void Release()
    {
        this.gameObject.GetComponent<Renderer>().material.SetFloat("_Opacity", 1f);
    }

    public void Select()
    {
        this.gameObject.GetComponent<Renderer>().material.SetFloat("_Opacity", 0.7f);
        Couch.Select(this, GameObject.Find("Key_Couch").GetComponent<Key>(), this.Couch);
    }
}
