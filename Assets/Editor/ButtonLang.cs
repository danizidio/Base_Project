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
        EditorWindow.GetWindow(typeof(GerarArquivoWindow), false, "Gerar Arquivo(s)");
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
                    WriteJson(so.keys, langList, so, "pt");
                }
                else
                {
                    List<Lang> langList = so.languages.FindAll(lang => lang.language == "en");
                    WriteJson(so.keys, langList, so, "en");
                }
            }
        }
        else
        {
            Debug.LogWarning("ScriptableObject não encontrado.");
        }

        GUILayout.Label("Configurações do Arquivo", EditorStyles.boldLabel);

        if (GUILayout.Button("Gerar Arquivo(s)"))
        {
            GerarArquivo();
        }
    }

    void WriteJson(List<string> listKeys, List<Lang> listValues, SO_Languages so, string language)
    {
        listValues = so.languages.FindAll(lang => lang.language == language);
        
        if (listKeys.Count > 0)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\n");

            for (int i = 0; i < listValues.Count; i++)
            {
                Lang lang = listValues[i];

                for (int j = 0; j < listKeys.Count; j++)
                {
                    jsonBuilder.AppendFormat(" \"{0}\": \"{1}\"", listKeys[j], lang.langValue[j]);
                    if (i < listValues.Count - 1 || j < listKeys.Count - 1)
                    {
                        jsonBuilder.Append(",\n");
                    }
                }
            }

            jsonBuilder.Append("\n}");

            EditorGUILayout.TextArea(jsonBuilder.ToString(), GUILayout.Height(100));

            //string caminho = Path.Combine(Application.dataPath + "Resources/Localization", $"Language_{language}.json");
            string path = Application.dataPath + "/Resources/Localization";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string p = Path.Combine(path, $"Language_{language}.json");
            File.WriteAllText(p, jsonBuilder.ToString());
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
