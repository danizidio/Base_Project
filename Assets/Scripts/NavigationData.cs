using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadPlayerPrefs;
using UnityEngine.Audio;

public class NavigationData : MonoBehaviour
{
    public static NavigationData Instance;

    public delegate bool _onSetBloom(bool b);
    public static _onSetBloom OnSetBloom;

    public delegate bool _onSetFilmGrain(bool b);
    public static _onSetFilmGrain OnSetFilmGrain;

    public delegate bool _onSetChromAberration(bool b);
    public static _onSetChromAberration OnSetChromAberration;

    public delegate float _onSetVolume();
    public static _onSetVolume OnSetVolume;

    public delegate float _onSetSFX();
    public static _onSetSFX OnSetSFX;

    public delegate void _onLoading();
    public static _onLoading OnLoading;

    public delegate void _onLoadInventory();
    public static _onLoadInventory OnLoadInventory;

    GameObject[] Datas;

    [SerializeField] int _targetFrameRate;

    float _volume;
    public float Volume { get { return _volume; } }

    float _sfx;
    public float Sfx { get { return _sfx; } }

    float _masterSound;
    public float MasterSound { get { return _masterSound; } }

    [SerializeField] bool _bloom;
    public bool Bloom { get { return _bloom; } }

    [SerializeField] bool _filmGrain;
    public bool FilmGrain { get { return _filmGrain; } }

    [SerializeField] bool _chromaticAberration;
    public bool ChromaticAberration { get { return _chromaticAberration; } }

    [SerializeField] bool _fullScreen;
    public bool FullScreen { get { return _fullScreen; } set { _fullScreen = value; } }

    [SerializeField] AudioMixer _audioMix;

    SaveLoad _saveLoad = new SaveLoad();

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Datas = GameObject.FindGameObjectsWithTag("NavigationData");

        if (Datas.Length > 1)
        {
            Debug.LogFormat("Asset NAVDATA : Destroyed " + Datas[1].name);
            Destroy(Datas[1]);
        }

        Application.targetFrameRate = _targetFrameRate;
    }

    void LoadData()
    {
        //FIRST TIME LOADING SETTING INITIAL VALUES!!
        if (!PlayerPrefs.HasKey(SaveStrings.FIRSTUSE.ToString()))
        {
            _saveLoad.PlayerSaveFloat(SaveStrings.MENU_VOLUME, 1);
            _saveLoad.PlayerSaveFloat(SaveStrings.MENU_SFX, 1);
            _saveLoad.PlayerSaveBool(SaveStrings.MENU_BLOOM, _bloom);
            _saveLoad.PlayerSaveBool(SaveStrings.MENU_FILMGRAIN, _filmGrain);
            _saveLoad.PlayerSaveBool(SaveStrings.MENU_CHROMATIC_ABERRATION, _chromaticAberration);
            _saveLoad.PlayerSaveBool(SaveStrings.MENU_FULLSCREEN, _fullScreen);
            _saveLoad.PlayerSaveInt(SaveStrings.MENU_RESOLUTION, 0);

            _saveLoad.PlayerSaveInt(SaveStrings.FIRSTUSE, 1);
        }

        //IF HAS FIRST USE KEY THE VALUES CAME FROM PLAYERPREFS KEYS
        else
        {
            _volume = _saveLoad.PlayerLoadingFloat(SaveStrings.MENU_VOLUME);

            _sfx = _saveLoad.PlayerLoadingFloat(SaveStrings.MENU_SFX.ToString());

            _bloom = _saveLoad.PlayerLoadingBool(SaveStrings.MENU_BLOOM);

            _filmGrain = _saveLoad.PlayerLoadingBool(SaveStrings.MENU_FILMGRAIN);

            _chromaticAberration = _saveLoad.PlayerLoadingBool(SaveStrings.MENU_CHROMATIC_ABERRATION);

            _fullScreen = _saveLoad.PlayerLoadingBool(SaveStrings.MENU_FULLSCREEN);

            //RESOLUTION
            switch (_saveLoad.PlayerLoadingInt(SaveStrings.MENU_RESOLUTION))
            {
                case 0:
                    {
                        Screen.SetResolution(1280, 720, FullScreen);
                        break;
                    }
                case 1:
                    {
                        Screen.SetResolution(1920, 1080, FullScreen);
                        break;
                    }
                case 2:
                    {
                        Screen.SetResolution(2560, 1440, FullScreen);
                        break;
                    }
                case 3:
                    {
                        Screen.SetResolution(3840, 2160, FullScreen);
                        break;
                    }
            }
        }
    }
    
    public bool SetBloom(bool b)
    {
        Volume vol = GameObject.FindGameObjectWithTag("Global Volume").GetComponent<Volume>();

        Bloom bloomVolume;

        if(vol.profile.TryGet<Bloom>(out bloomVolume))
        {
            if(b)
            {
                bloomVolume.scatter.value = .6f;
            }
            else
            {
                bloomVolume.scatter.value = .3f;
            }
        }

        Toggle t = GameObject.FindGameObjectWithTag("BloomFX").GetComponent<Toggle>();

        t.isOn = b;

        return _bloom = b;
    }

    public bool SetFilmGrain(bool b)
    {
        Volume vol = GameObject.FindGameObjectWithTag("Global Volume").GetComponent<Volume>();

        FilmGrain filmGrainVolume;

        if (vol.profile.TryGet<FilmGrain>(out filmGrainVolume))
        {
            filmGrainVolume.active = b;
        }

        Toggle t = GameObject.FindGameObjectWithTag("FilmGrainFX").GetComponent<Toggle>();

        t.isOn = b;

        return _filmGrain = b;
    }

    public bool SetChromaticAberration(bool b)
    {
        Volume vol = GameObject.FindGameObjectWithTag("Global Volume").GetComponent<Volume>();

        ChromaticAberration chromaticAberrationVolume;

        if (vol.profile.TryGet<ChromaticAberration>(out chromaticAberrationVolume))
        {
            chromaticAberrationVolume.active = b;
        }

        Toggle t = GameObject.FindGameObjectWithTag("ChromaFX").GetComponent<Toggle>();

        t.isOn = b;

        return _chromaticAberration = b;
    }

    public float SetVolumeValue(float f)
    {
        return _volume = f;
    }
    public float SetSfxValue(float f)
    {
        return _sfx = f;
    }

    public float SetMasterSoundValue(float f)
    {
        return _masterSound = f;
    }

    public float SetSoundMaster()
    {
        _audioMix.SetFloat("MasterVol", _masterSound);

        return _masterSound;
    }

    public float SetSoundVolume()
    {
        _audioMix.SetFloat("MusicVol", _volume);

        return _volume;
    }

    public float SetSfxVolume()
    {
        _audioMix.SetFloat("SfxVol", _sfx);

        return _sfx;
    }

    private void OnEnable()
    {
        OnLoading += LoadData;
        OnSetBloom += SetBloom;
        OnSetVolume += SetSoundVolume;
        OnSetSFX += SetSfxVolume;
        OnSetFilmGrain += SetFilmGrain;
        OnSetChromAberration += SetChromaticAberration;
    }

    private void OnDisable()
    {
        OnLoading -= LoadData;
        OnSetBloom -= SetBloom;
        OnSetVolume -= SetSoundVolume;
        OnSetSFX -= SetSfxVolume;
        OnSetFilmGrain -= SetFilmGrain;
        OnSetChromAberration -= SetChromaticAberration;
    }
}

