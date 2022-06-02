using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeurositySDK;

public class Statue_Game : Game
{
    [Range(0, 1)]
    public float testValue;

    private Collider baseCollider;
    private Collider statueCollider;
    public GameObject statue;
    private float MappedValue = 0f;
    private float triggerLock = 3.317f;

    public bool activeGame = false;

    [SerializeField]
    private BCIManager bciManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (activeGame)
        {
            this.GameState = GameState.InProgress;
            //BciControl();

            activeGame = false;
        }
    }

    public void pushStatue(float probability)
    {
        MappedValue = Mathf.Lerp(3.078f, 3.37f, probability);

        if (this.GameState == GameState.InProgress)
        {
            if (MappedValue >= triggerLock)
            {
                statue.transform.Translate(new Vector3(0, 0, MappedValue));
                GameCompleted();
            }
            else
            {
                statue.transform.Translate(new Vector3(0, 0, MappedValue));
            }
        }
        else
        {
            return;
        }
    }

    /* Subscribe/Unsubscribe BCI command */
    private void BciControl()
    {
       if (this.GameState == GameState.InProgress)
       {
           string kinesisCommand = "pushKinesis";
           bciManager.SubscribeKinesis(kinesisCommand, new KinesisHandler
           {
               OnKinesisUpdated = (probability) => { pushStatue(probability); }
           });
       }
       else if (this.GameState == GameState.Completed)
       {
           //bciManager.UnsubscribeCalm()
           Debug.Log("Calm unsubscribed");
       }
    }

    /* Puzzle completed */
    private void GameCompleted()
    {
        this.GameState = GameState.Completed;
        Debug.Log("Flame challenge completed");
        //BciControl();
    }
}
