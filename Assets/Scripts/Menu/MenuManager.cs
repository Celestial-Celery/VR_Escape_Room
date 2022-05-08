using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject settingsCanvas;
    public GameObject howToPlayCanvas;
    public GameObject howToPlayControllerCanvas;
    public GameObject howToPlayBCICanvas;
    public GameObject creditsCanvas;

    public Toggle dutchToggle;
    public Toggle englishToggle;

    public Toggle easyDifficultyToggle;
    public Toggle normalDifficultyToggle;
    public Toggle hardDifficultyToggle;

    public Toggle normalControlsToggle;
    public Toggle bciControlsToggle;

    public Toggle standingToggle;
    public Toggle sittingToggle;




    private bool canChange = false;

    private void Start()
    {
        StartCoroutine(LateStart(1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SetButtonValues();
    }

    public IEnumerator setLanguage()
    {
        yield return LocalizationSettings.InitializationOperation;

        if (PlayerPrefs.GetInt("dutch") == 1)
        {
            Debug.Log("Setting language to dutch");
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        }
        else
        {
            Debug.Log("Setting language to english");
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        }
    }

    private void SetButtonValues()
    {
        dutchToggle.isOn = PlayerPrefs.GetInt("dutch") == 1;
        englishToggle.isOn = PlayerPrefs.GetInt("english") == 1;

        easyDifficultyToggle.isOn = PlayerPrefs.GetInt("easy") == 1;
        normalDifficultyToggle.isOn = PlayerPrefs.GetInt("normal") == 1;
        hardDifficultyToggle.isOn = PlayerPrefs.GetInt("hard") == 1;

        normalControlsToggle.isOn = PlayerPrefs.GetInt("normalcontrols") == 1;
        bciControlsToggle.isOn = PlayerPrefs.GetInt("bcicontrols") == 1;

        standingToggle.isOn = PlayerPrefs.GetInt("standing") == 1;
        sittingToggle.isOn = PlayerPrefs.GetInt("sitting") == 1;

        StartCoroutine(setLanguage());

        canChange = true;
    }

    public void ChangeLanguageValue()
    {
        if (canChange)
        {
            PlayerPrefs.SetInt("english", englishToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("dutch", dutchToggle.isOn ? 1 : 0);
            PlayerPrefs.Save();

            StartCoroutine(setLanguage());
        }
    }

    public void ChangeDifficultyValue()
    {
        if (canChange)
        {
            PlayerPrefs.SetInt("easy", easyDifficultyToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("normal", normalDifficultyToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("hard", hardDifficultyToggle.isOn ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public void ChangeControlsValue()
    {
        if (canChange)
        {
            PlayerPrefs.SetInt("normalcontrols", normalControlsToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("bcicontrols", bciControlsToggle.isOn ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public void ChangeAccessibilityValue()
    {
        if (canChange)
        {
            PlayerPrefs.SetInt("standing", standingToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("sitting", sittingToggle.isOn ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public void StartGame()
    {

    }    

    public void ShowCredits()
    {
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void ShowSettings()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        howToPlayControllerCanvas.SetActive(false);
        howToPlayBCICanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        howToPlayCanvas.SetActive(true);
    }
    
    public void ShowHowToPlayController()
    {
        howToPlayCanvas.SetActive(false);
        howToPlayControllerCanvas.SetActive(true);
    }

    public void ShowHowToPlayBCI()
    {
        howToPlayCanvas.SetActive(false);
        howToPlayBCICanvas.SetActive(true);
    }

    public void ShowMainMenu()
    {
        settingsCanvas.SetActive(false);
        howToPlayCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}