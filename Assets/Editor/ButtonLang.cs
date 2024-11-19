using UnityEngine;
using UnityEditor;
using System.IO;
using System.Drawing.Printing;
using static UnityEditor.PlayerSettings.Switch;
using System.Collections.Generic;
using System.Text;

public class GerarArquivoWindow : EditorWindow
{
    private string[] _jsonName;
    private string[] _jsonContent;

    [MenuItem("Languages/Generate Dictionary")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GerarArquivoWindow), false, "Gerar Arquivo");
    }

    private void OnGUI()
    {
        string[] guids = AssetDatabase.FindAssets("Language" + " t:ScriptableObject");

        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            SO_Languages so = AssetDatabase.LoadAssetAtPath<SO_Languages>(path);
            _jsonContent = new string[so.languages.Count];
            _jsonName = new string[so.languages.Count];

            foreach (Lang s in so.languages)
            {
                if (s.language == "pt")
                {
                    List<Lang> langList = so.languages.FindAll(lang => lang.language == "pt");
                    WriteJson(langList, so, "pt");
                    //SO_Languages filteredSO = CreateInstance<SO_Languages>();
                    //filteredSO.languages = langList;
                    //_jsonName[0] = "Language" + "_" + s.language + ".json";
                    //_jsonContent[0] = JsonUtility.ToJson(filteredSO, true);
                }
                else
                {
                    List<Lang> langList = so.languages.FindAll(lang => lang.language == "en");
                    WriteJson(langList, so, "en");
                    //SO_Languages filteredSO = CreateInstance<SO_Languages>();
                    //filteredSO.languages = langList;
                    //_jsonName[1] = "Language" + "_" + s.language + ".json";
                    //_jsonContent[1] = JsonUtility.ToJson(filteredSO, true);
                }
            }
        }
        else
        {
            Debug.LogWarning("ScriptableObject não encontrado.");
        }

        GUILayout.Label("Configurações do Arquivo", EditorStyles.boldLabel);

        if (GUILayout.Button("Gerar Arquivo"))
        {
            GerarArquivo();
        }
    }

    void WriteJson(List<Lang> list, SO_Languages so, string language)
    {
        list = so.languages.FindAll(lang => lang.language == language);

        if (list.Count > 0)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\n");
            //jsonBuilder.AppendFormat(" \"language\": \"{0}\",\n", language);
            //jsonBuilder.Append(" \"translations\": {\n");
            for (int i = 0; i < list.Count; i++)
            {
                Lang lang = list[i];
                for (int j = 0; j < lang.langKey.Count; j++)
                {
                    jsonBuilder.AppendFormat(" \"{0}\": \"{1}\"", lang.langKey[j], lang.langValue[j]);
                    if (i < list.Count - 1 || j < lang.langKey.Count - 1)
                    {
                        jsonBuilder.Append(",\n");
                    }
                }
            }

            jsonBuilder.Append("\n}");

            EditorGUILayout.TextArea(jsonBuilder.ToString(), GUILayout.Height(100));

            string caminho = Path.Combine(Application.dataPath + "/Localization", $"Language_{language}.json");
            File.WriteAllText(caminho, jsonBuilder.ToString());
        }
        else
        {
            Debug.LogWarning($"Nenhum dado encontrado para a linguagem: {language}");
        }
    }

    private void GerarArquivo()
    {
        EditorUtility.DisplayDialog("Arquivo Gerado", "Arquivo foi gerado com sucesso.", "OK");

        AssetDatabase.Refresh();
    }
}
