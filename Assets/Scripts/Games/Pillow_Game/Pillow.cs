using UnityEngine;

public class Pillow : MonoBehaviour
{
    public bool HasKey;


    void Awake()
    {
        HasKey = false;
    }

    //TEMPORARY TO TEST OUT GAME
    public bool IsSelected;
    void Update()
    {
        if (IsSelected)
        {
            Select();
        }
    }

    public void Select()
    {
        Couch.Select(this);
    }
}
