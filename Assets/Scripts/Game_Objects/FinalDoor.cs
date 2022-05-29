using UnityEngine;

public class FinalDoor : Door
{
    [SerializeField] Transform keyHole1;
    [SerializeField] Transform keyHole2;
    [SerializeField] Transform keyHole3;

    private bool key1Added, key2Added, key3Added = false;

    private void CheckIfOpen()
    {
        if (key1Added && key2Added && key3Added)
        {
            Open();
        }
    }

    public void AddKey1(GameObject key)
    {
        if (!key1Added)
        {
            key.transform.rotation = Quaternion.Euler(90, -90, -90);
            key.transform.position = keyHole1.transform.position + new Vector3(0.02f, 0, 0);
            key.transform.parent = gameObject.transform;
            key1Added = true;
        }
        CheckIfOpen();
    }

    public void AddKey2(GameObject key)
    {
        if (!key2Added)
        {
            key.transform.rotation = Quaternion.Euler(90, -90, -90);
            key.transform.position = keyHole2.transform.position + new Vector3(0.02f, 0, 0);
            key.transform.parent = gameObject.transform;
            key2Added = true;
        }
        CheckIfOpen();
    }

    public void AddKey3(GameObject key)
    {
        if (!key3Added)
        {
            key.transform.rotation = Quaternion.Euler(90, -90, -90);
            key.transform.position = keyHole3.transform.position + new Vector3(0.02f, 0, 0);
            key.transform.parent = gameObject.transform;
            key3Added = true;
        }
        CheckIfOpen();
    }
}
