using UnityEngine;
using Localization;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
public class MenuLanguage : LocalizationText
{
    //[SerializeField]
    //TMP_Text _play,
    //                _options,
    //                _exit,
    //                _credits,
    //                _clearData,
    //                _sound,
    //                _quality,
    //                _language,
    //                _fullScreen,
    //                _creditBox;

    [SerializeField] List<TMP_Text> _texts;

    [SerializeField] List<string> _strings;

    private void Awake()
    {
        _texts = GameObject.FindObjectsOfType<TMP_Text>().ToList();

        foreach (var text in _texts)
        {
            _strings.Add(text.text);
        }
    }
    private void Start()
    {
        UpdateText();
    }


    protected override void UpdateText()
    {
        //    _play.text = LocalizationManager.Instance.GetLocalizedValue("begin");
        //_options.text = LocalizationManager.Instance.GetLocalizedValue("option");
        //_exit.text = LocalizationManager.Instance.GetLocalizedValue("leave");
        //_credits.text = LocalizationManager.Instance.GetLocalizedValue("credits");
        //_clearData.text = LocalizationManager.Instance.GetLocalizedValue("clearsave");
        //_sound.text = LocalizationManager.Instance.GetLocalizedValue("soundset");
        //_quality.text = LocalizationManager.Instance.GetLocalizedValue("imageset");
        //_language.text = LocalizationManager.Instance.GetLocalizedValue("language");
        //_fullScreen.text = LocalizationManager.Instance.GetLocalizedValue("fullscreen");
        //_creditBox.text = LocalizationManager.Instance.GetLocalizedValue("credittext");

        for (int i = 0; i < _texts.Count; i++)
        {
            _texts[i].text = _strings[i];
        }

        foreach (TMP_Text text in _texts)
        {
            string s = GetLocalizedText(text.text);
            text.text = s;
        }
    }
}
