using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using NeurositySDK;

public class BCIManager : MonoBehaviour
{
    [SerializeField]
    private Device _device;
    FirebaseController _firebaseController;
    Notion _notionSdk;

    private void OnEnable()
    {
        if (_device == null)
        {
            Debug.LogError("Provide a device device instance. Assets -> Create -> Device", this);
            return;
        }
        if (!_device.IsValid)
        {
            Debug.LogError("Provide a valid device.", this);
            return;
        }
    }

    private async void OnDisable()
    {
        if (_notionSdk == null) return;
        if (!_notionSdk.IsLoggedIn) return;

        // Wrapping because Logout is meant to be invoked and forgotten about for use in button callbacks.
        await Task.Run(() => Logout());
        Debug.Log($"Logged out from {nameof(OnDisable)}");
    }

    public async void Login()
    {
        _firebaseController = new FirebaseController();
        await _firebaseController.Initialize();
        // Debug.Log("------");

        _notionSdk = new Notion(_firebaseController);
        await _notionSdk.Login(_device);

        Debug.Log("Logged in");
    }

    public async void Logout()
    {
        await _notionSdk.Logout();
        _firebaseController = null;
        _notionSdk = null;

        Debug.Log("Logged out");
    }

    public async void GetDevices()
    {
        if (!_notionSdk.IsLoggedIn) return;
        var devices = await _notionSdk.GetDevices();
        Debug.Log(JsonConvert.SerializeObject(devices));
    }

    public void GetStatus()
    {
        if (!_notionSdk.IsLoggedIn) return;
        Debug.Log(JsonConvert.SerializeObject(_notionSdk.Status));
    }

    public void SubscribeCalm(CalmHandler calmHandler)
    {
        if (!_notionSdk.IsLoggedIn) return;
        _notionSdk.Subscribe(calmHandler);
        Debug.Log("Subscribed to calm");
    }

    // public void SubscribeFocus()
    // {
    //     if (!_notionSdk.IsLoggedIn) return;
    //     _notionSdk.Subscribe(new FocusHandler());
    //     Debug.Log("Subscribed to focus");
    // }

    // public void SubscribeBrainwaves()
    // {
    //     if (!_notionSdk.IsLoggedIn) return;
    //     _notionSdk.Subscribe(new BrainwavesRawHandler());
    //     Debug.Log("Subscribed to raw brainwaves");
    // }

    // public void SubscribeAccelerometer()
    // {
    //     if (!_notionSdk.IsLoggedIn) return;
    //     _notionSdk.Subscribe(new AccelerometerHandler());
    //     Debug.Log("Subscribed to accelerometer");
    // }

    /// <summary>
    /// Add kinesisLabel based on the thought you're training.
    /// For instance: leftArm, rightArm, leftIndexFinger, etc
    /// </summary>
    /// <param name="kinesisLabel"></param>
    public void SubscribeKinesis(string kinesisLabel, KinesisHandler kinesisHandler)
    {
       if (!_notionSdk.IsLoggedIn) return;

        _notionSdk.Subscribe(kinesisHandler);
    //    _notionSdk.Subscribe(new KinesisHandler
    //    {
    //        Label = kinesisLabel,
    //        OnKinesisUpdated = (probability) =>
    //        {
    //            _textKinesisProbability.text = $"{kinesisLabel} : {probability}";
    //        }
    //    });
    }
}