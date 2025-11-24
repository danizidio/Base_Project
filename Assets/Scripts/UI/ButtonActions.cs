using UnityEngine;
using SaveLoadPlayerPrefs;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    [SerializeField] Slider _masterSound, _musicSound, _sfx;

    [SerializeField] TMPro.TMP_Dropdown _resolution;

    [SerializeField] TMPro.TMP_Dropdown _qualitySettings;

    [SerializeField] Toggle _fullScreen;

    [SerializeField] GameObject _exitBtn;

    [SerializeField] Color32 _fontColor;

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
    }
    
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

    public void SetQuality(int qualitySettings)
    {
        QualitySettings.SetQualityLevel(qualitySettings);
    }

    public void SetResolution(int res)
    {
        SaveLoad s = new SaveLoad();

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

        s.PlayerSaveInt(SaveStrings.MENU_RESOLUTION, res);
    }

    public void SetFullScreen(bool on)
    {
        SaveLoad s = new SaveLoad();

        Screen.fullScreen = on;

        NavigationData.Instance.FullScreen = on;

        s.PlayerSaveBool(SaveStrings.MENU_FULLSCREEN, on);
    }

    public void SaveBloom(bool v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveBool(SaveStrings.MENU_BLOOM, v);

        NavigationData.OnSetBloom?.Invoke(v);
    }

    public void SaveFilmGrain(bool v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveBool(SaveStrings.MENU_FILMGRAIN, v);

        NavigationData.OnSetFilmGrain?.Invoke(v);
    }

    public void SaveChromaticAberration(bool v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveBool(SaveStrings.MENU_CHROMATIC_ABERRATION, v);

        NavigationData.OnSetChromAberration?.Invoke(v);
    }
    public void SaveMasterSoundVolume(float v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveFloat(SaveStrings.MENU_MASTERSOUND, v);

        NavigationData.Instance.SetMasterSoundValue(v);

        NavigationData.OnSetMasterVolume?.Invoke();
    }
    public void SaveMusicVolume(float v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveFloat(SaveStrings.MENU_VOLUME, v);

        NavigationData.Instance.SetVolumeValue(v);

        NavigationData.OnSetVolume?.Invoke();
    }
    public void SaveSFX(float v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveFloat(SaveStrings.MENU_SFX, v);

        NavigationData.Instance.SetSfxValue(v);

        NavigationData.OnSetSFX?.Invoke();
    }

    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    void UpdateUIOnLoad()
    {
        SaveLoad s = new SaveLoad();

        _masterSound.value = s.PlayerLoadingFloat(SaveStrings.MENU_MASTERSOUND);
        _musicSound.value = s.PlayerLoadingFloat(SaveStrings.MENU_VOLUME);
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
