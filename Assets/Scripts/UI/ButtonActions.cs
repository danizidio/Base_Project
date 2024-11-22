using UnityEngine;
using SaveLoadPlayerPrefs;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using Localization;
using TMPro;
public class ButtonActions : MonoBehaviour
{
    [SerializeField] Slider _sound, _sfx;

    [SerializeField] TMP_Dropdown _resolution;

    [SerializeField] TMP_Dropdown _qualitySettings;
    [SerializeField] TMP_Dropdown _languageSettings;

    [SerializeField] Toggle _fullScreen;

    [SerializeField] GameObject _exitBtn;

    [SerializeField] Color32 _fontColor;

    SaveLoad s = new SaveLoad();

    private void Start()
    {
#if UNITY_WEBGL  
        _exitBtn.SetActive(false);
        _resolution.gameObject.SetActive(false);
        _qualitySettings.gameObject.SetActive(false);
#endif

        GameObject[] _fonts = GameObject.FindGameObjectsWithTag("UI_Element");
        foreach(GameObject _font in _fonts)
        {
            _font.GetComponent<TMPro.TMP_Text>().color = _fontColor;
        }

        if (s.PlayerLoadingString<SaveStrings>(SaveStrings.LOCALIZATION) == "en")
        {
            _languageSettings.value = 0;
            LocalizationManager.OnChangeLocalization("en");
        }
        if (s.PlayerLoadingString<SaveStrings>(SaveStrings.LOCALIZATION) == "pt")
        {
            _languageSettings.value = 1;
            LocalizationManager.OnChangeLocalization("pt");
        }
    }

    #region -- Button Animations
    public void OnEnterOptions()
    {
        GetComponent<Animator>().SetTrigger("OPTIONS");
    }

    public void OnEnterCredits()
    {
        GetComponent<Animator>().SetTrigger("CREDITS");
    }

    public void OnMainMenu()
    {
        GetComponent<Animator>().SetTrigger("MAINMENU");
    }

    public void OnClearData()
    {
        GetComponent<Animator>().SetTrigger("CLEARSAVE");
    }
    #endregion

    #region -- Button Actions
    public void SetQuality(int qualitySettings)
    {
        QualitySettings.SetQualityLevel(qualitySettings);
    }

    public void SetResolution(int res)
    {
        switch (res)
        {
            case 0:
                {
                    Screen.SetResolution(1280, 720, NavigationData.Instance.FullScreen);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1920, 1080, NavigationData.Instance.FullScreen);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(2560, 1440, NavigationData.Instance.FullScreen);
                    break;
                }
            case 3:
                {
                    Screen.SetResolution(3840, 2160, NavigationData.Instance.FullScreen);
                    break;
                }
        }

        s.PlayerSaveInt<SaveStrings>(SaveStrings.MENU_RESOLUTION, res);
    }

    public void SetFullScreen(bool on)
    {
        Screen.fullScreen = on;

        NavigationData.Instance.FullScreen = on;

        s.PlayerSaveBool<SaveStrings>(SaveStrings.MENU_FULLSCREEN , on);
    }

    public void SaveBloom(bool v)
    {
        s.PlayerSaveBool<SaveStrings>(SaveStrings.MENU_BLOOM, v);

        NavigationData.OnSetBloom?.Invoke(v);
    }

    public void SaveFilmGrain(bool v)
    {
        s.PlayerSaveBool<SaveStrings>(SaveStrings.MENU_FILMGRAIN, v);

        NavigationData.OnSetFilmGrain?.Invoke(v);
    }

    public void SaveChromaticAberration(bool v)
    {
        s.PlayerSaveBool<SaveStrings>(SaveStrings.MENU_CHROMATIC_ABERRATION, v);

        NavigationData.OnSetChromAberration?.Invoke(v);
    }
    public void SaveMasterSoundVolume(float v)
    {
        s.PlayerSaveFloat<SaveStrings>(SaveStrings.MENU_MASTERSOUND, v);

        NavigationData.Instance.SetVolumeValue(v);

        NavigationData.OnSetVolume?.Invoke();
    }
    public void SaveMusicVolume(float v)
    {
        s.PlayerSaveFloat<SaveStrings>(SaveStrings.MENU_VOLUME, v);

        NavigationData.Instance.SetVolumeValue(v);

        NavigationData.OnSetVolume?.Invoke();
    }
    public void SaveSFX(float v)
    {
        s.PlayerSaveFloat<SaveStrings>(SaveStrings.MENU_SFX, v);

        NavigationData.Instance.SetSfxValue(v);

        NavigationData.OnSetSFX?.Invoke();
    }

    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ChangeLocalization(int i)
    {
        switch (i)
        {
            case 0:
                {
                    LocalizationManager.OnChangeLocalization("en");
                    break;
                }
            case 1:
                {
                    LocalizationManager.OnChangeLocalization("pt");
                    break;
                }
        }
        LocalizationManager.Instance.SaveLocalizedText();
    }
    #endregion

    void UpdateUIOnLoad()
    {
        _sound.value = s.PlayerLoadingFloat<SaveStrings>(SaveStrings.MENU_VOLUME);

        _sfx.value = s.PlayerLoadingFloat(SaveStrings.MENU_SFX);

        Toggle tChroma = GameObject.FindGameObjectWithTag("ChromaFX").GetComponent<Toggle>();

        tChroma.isOn = s.PlayerLoadingBool(SaveStrings.MENU_CHROMATIC_ABERRATION);

        Toggle tBlomm = GameObject.FindGameObjectWithTag("BloomFX").GetComponent<Toggle>();

        tBlomm.isOn = s.PlayerLoadingBool(SaveStrings.MENU_BLOOM);

        Toggle tGrain = GameObject.FindGameObjectWithTag("FilmGrainFX").GetComponent<Toggle>();

        tGrain.isOn = s.PlayerLoadingBool(SaveStrings.MENU_FILMGRAIN);

        _fullScreen.isOn = s.PlayerLoadingBool(SaveStrings.MENU_FULLSCREEN);

        _resolution.value = s.PlayerLoadingInt(SaveStrings.MENU_RESOLUTION);

        switch (_resolution.value)
        {
            case 0:
                {
                    Screen.SetResolution(1280, 720, _fullScreen.isOn);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1920, 1080, _fullScreen.isOn);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(2560, 1440, _fullScreen.isOn);
                    break;
                }
            case 3:
                {
                    Screen.SetResolution(3840, 2160, _fullScreen.isOn);
                    break;
                }
        }
    }

    private void OnEnable()
    {
        NavigationData.OnLoading += UpdateUIOnLoad;
    }
    private void OnDisable()
    {
        NavigationData.OnLoading -= UpdateUIOnLoad;
    }
}
