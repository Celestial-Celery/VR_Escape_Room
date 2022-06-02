using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeurositySDK;

public class Flame_Game : Game
{
    public GameObject flame;
    private ParticleSystem flameParticles;
    private ParticleSystem smokeParticles;
    public bool activeGame = true; 

    [Range(1, 2)]
    public float sensitivity;

    public float MappedValue = 0;      //current mapped value
    public float triggerLock = 60;     //value to lock flame and end puzzle

    /* TODO: change depending final logic */
    [SerializeField]
    BCIManager bciManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (activeGame)
        {
            flameParticles = transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
            smokeParticles = transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
            flameParticles.Play();
            smokeParticles.Play();
            var em_f = flameParticles.emission;
            var em_s = flameParticles.emission;
            em_f.rateOverTime = 0f;         //starting rot for fire particles
            em_s.rateOverTime = 30f;        //starting rot for smoke particles

            this.GameState = GameState.InProgress;
            BciControl();

            activeGame = false;
        }
    }

    /* Triggered function on BCI value change */
    public void InfluenceFlame(float probability)
    {
        MappedValue = Mathf.Lerp(0, 100, (probability));

        if (this.GameState == GameState.InProgress)
        {
            if (MappedValue >= triggerLock)
            {
                var em = flameParticles.emission;
                em.rateOverTime = MappedValue;
                GameCompleted();
            }
            else
            {
                var em = flameParticles.emission;
                em.rateOverTime = MappedValue;
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
            bciManager.SubscribeCalm(new CalmHandler
            {
                OnCalmUpdated = (probability) => { InfluenceFlame(probability); }
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
        BciControl();
    }
}
