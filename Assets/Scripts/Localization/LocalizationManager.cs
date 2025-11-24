using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using SaveLoadPlayerPrefs;
using System.IO;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        public delegate void _onChangeLocalization(string s);
        public static _onChangeLocalization OnChangeLocalization;
        public static LocalizationManager Instance;
        Dictionary<string, string> localizedText;
        [SerializeField] TextAsset[] _files;
        TextAsset _file;
        string _currentLanguage;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                if (_files.Length == 0)
                {
                    string[] languageFiles = Directory.GetFiles(Application.dataPath + "/Resources/Localization", $"Language*.json", SearchOption.TopDirectoryOnly);

                    _files = new TextAsset[languageFiles.Length];

                    for (int i = 0; i < languageFiles.Length; i++)
                    {
                        string file = Path.GetFileNameWithoutExtension(languageFiles[i]);
                        string relativePath = $"Localization/{file}";
                        TextAsset t = Resources.Load<TextAsset>(relativePath);
                        if (t != null)
                        {
                            _files[i] = t;
                        }
                        else
                        {
                            Debug.Log("Arquivo não encontrado no caminho: " + relativePath);
                        }
                    }
                }

                SaveLoad s = new SaveLoad();

                if (s.CheckKey(SaveStrings.LOCALIZATION))
                {
                    _currentLanguage = s.PlayerLoadingString(SaveStrings.LOCALIZATION);
                    LoadLocalizedText(_currentLanguage);
                }
                else
                {
                    LoadLocalizedText("en");
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadLocalizedText(string language)
        {
            _currentLanguage = language;

            localizedText = new Dictionary<string, string>();

            foreach (var item in _files)
            {
                if (item.name == "Language_" + language)
                {
                    _file = item;
                }
            }

            var data = JSON.Parse(_file.text);

            foreach (var item in data)
            {
                localizedText.Add(item.Key, item.Value);
            }

            LocalizationText.OnUpdateLocalization?.Invoke();
        }

        public void SaveLocalizedText()
        {
            if (string.IsNullOrEmpty(_currentLanguage)) return;

            SaveLoad s = new SaveLoad();
            s.PlayerSaveString(SaveStrings.LOCALIZATION, _currentLanguage);
        }

        public string GetLocalizedValue(string key)
        {
            if (localizedText.ContainsKey(key))
            {
                return localizedText[key];
            }
            return key;
        }
        private void OnEnable()
        {
            OnChangeLocalization += LoadLocalizedText;
        }
        private void OnDisable()
        {
            OnChangeLocalization -= LoadLocalizedText;
        }
    }
}