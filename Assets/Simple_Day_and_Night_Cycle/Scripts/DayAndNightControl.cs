//2016 Spyblood Games

using UnityEngine;
using System.Collections;

[System.Serializable]
public class DayColors
{
	public Color skyColor;
	public Color equatorColor;
	public Color horizonColor;
}

public class DayAndNightControl : MonoBehaviour {
	public bool StartDay; //start game as day time
	public GameObject StarDome;
	public GameObject moonState;
	public GameObject moon;
	public DayColors dawnColors;
	public DayColors dayColors;
	public DayColors nightColors;
	public int currentDay = 0; //day 8287... still stuck in this grass prison... no esacape... no freedom...
	public Light directionalLight; //the directional light in the scene we're going to work with
	public float SecondsInAFullDay = 120f; //in realtime, this is about two minutes by default. (every 1 minute/60 seconds is day in game)
	[HideInInspector] public float currentTime_hidden = 0; //at default when you press play, it will be nightTime. (0 = night, 1 = day)
	[Range(0,1)]
	public float currentTime = 0; //at default when you press play, it will be nightTime. (0 = night, 1 = day)
	[HideInInspector]
	public float timeMultiplier = 1f; //how fast the day goes by regardless of the secondsInAFullDay var. lower values will make the days go by longer, while higher values make it go faster. This may be useful if you're siumulating seasons where daylight and night times are altered.
	public bool showUI;
	float lightIntensity; //static variable to see what the current light's insensity is in the inspector
	Material starMat;

	Camera targetCam;

	// Use this for initialization
	void Start () {
		RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
		foreach (Camera c in GameObject.FindObjectsOfType<Camera>())
		{
			if (c.isActiveAndEnabled) {
				targetCam = c;
			}
		}
		lightIntensity = directionalLight.intensity; //what's the current intensity of the light
		starMat = StarDome.GetComponentInChildren<MeshRenderer> ().material;
		if (StartDay) {
			currentTime_hidden = 0.3f; //start at morning
			starMat.color = new Color(1f,1f,1f,0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		currentTime_hidden = 0.25f + (0.78f - 0.25f) * ((currentTime - 0f) / (1f - 0f));
		UpdateLight();
	}

	void UpdateLight()
	{
		StarDome.transform.Rotate (new Vector3 (0, 2f * Time.deltaTime, 0));
		moon.transform.LookAt (targetCam.transform);
		directionalLight.transform.localRotation = Quaternion.Euler ((currentTime_hidden * 360f) - 90, 170, 0);
		moonState.transform.localRotation = Quaternion.Euler ((currentTime_hidden * 360f) - 100, 170, 0);
		//^^ we rotate the sun 360 degrees around the x axis, or one full rotation times the current time variable. we subtract 90 from this to make it go up
		//in increments of 0.25.

		//the 170 is where the sun will sit on the horizon line. if it were at 180, or completely flat, it would be hard to see. Tweak this value to what you find comfortable.

		float intensityMultiplier = 1;

		if (currentTime_hidden <= 0.23f || currentTime_hidden >= 0.75f) 
		{
			intensityMultiplier = 0; //when the sun is below the horizon, or setting, the intensity needs to be 0 or else it'll look weird
			starMat.color = new Color(1,1,1,Mathf.Lerp(1,0,Time.deltaTime));
		}
		else if (currentTime_hidden <= 0.25f) 
		{
			intensityMultiplier = Mathf.Clamp01((currentTime_hidden - 0.23f) * (1 / 0.02f));
			starMat.color = new Color(1,1,1,Mathf.Lerp(0,1,Time.deltaTime));
		}
		else if (currentTime_hidden <= 0.73f) 
		{
			intensityMultiplier = Mathf.Clamp01(1 - ((currentTime_hidden - 0.73f) * (1 / 0.02f)));
		}


		//change env colors to add mood

		if (currentTime_hidden <= 0.2f) {
			RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, nightColors.skyColor,Time.deltaTime);
			RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, nightColors.equatorColor, Time.deltaTime);
			RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, nightColors.horizonColor, Time.deltaTime);
		}
		if (currentTime_hidden > 0.2f && currentTime_hidden < 0.4f) {
			RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, dawnColors.skyColor, Time.deltaTime);
			RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, dawnColors.equatorColor, Time.deltaTime);
			RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, dawnColors.horizonColor, Time.deltaTime);
		}
		if (currentTime_hidden > 0.4f && currentTime_hidden < 0.75f) {
			RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, dayColors.skyColor, Time.deltaTime);
			RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, dayColors.equatorColor, Time.deltaTime);
			RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, dayColors.horizonColor, Time.deltaTime);
		}
		if (currentTime_hidden > 0.75f) {
			RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, dayColors.skyColor, Time.deltaTime);
			RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, dayColors.equatorColor, Time.deltaTime);
			RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, dayColors.horizonColor, Time.deltaTime);
		}

		directionalLight.intensity = lightIntensity * intensityMultiplier;
	}

	public string TimeOfDay ()
	{
	string dayState = "";
		if (currentTime_hidden > 0f && currentTime_hidden < 0.1f) {
			dayState = "Midnight";
		}
		if (currentTime_hidden < 0.5f && currentTime_hidden > 0.1f)
		{
			dayState = "Morning";

		}
		if (currentTime_hidden > 0.5f && currentTime_hidden < 0.6f)
		{
			dayState = "Mid Noon";
		}
		if (currentTime_hidden > 0.6f && currentTime_hidden < 0.8f)
		{
			dayState = "Evening";

		}
		if (currentTime_hidden > 0.8f && currentTime_hidden < 1f)
		{
			dayState = "Night";
		}
		return dayState;
	}
}
