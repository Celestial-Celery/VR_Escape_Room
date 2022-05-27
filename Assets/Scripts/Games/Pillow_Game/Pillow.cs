using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pillow : MonoBehaviour
{
    public bool HasKey;
    public Couch Couch;

    private bool fadeOut = false;
    private float opacity = 1;

    void Awake()
    {
        HasKey = false;
    }

    private void Update()
    {
        if (fadeOut && opacity >= 0f)
        {
            opacity -= 1.5f * Time.deltaTime;
            this.gameObject.GetComponent<Renderer>().material.SetFloat("_Opacity", opacity);
        }
        if (opacity < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Release()
    {
        if (!HandManager.handsActive)
        {
            this.gameObject.GetComponent<Renderer>().material.SetFloat("_Opacity", 1f);
            this.gameObject.transform.parent = this.Couch.gameObject.transform;
        }        
    }

    public void Select()
    {
        Couch.Select(this);

        if (HandManager.handsActive)
        {
            fadeOut = true;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.SetFloat("_Opacity", 0.7f);
        }
    }


}
