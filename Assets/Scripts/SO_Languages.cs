using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "ScriptableObjects/LanguageDictionary")]
public class SO_Languages : ScriptableObject
{
    public List<string> keys;
    public List<Lang> languages;
}

[Serializable]
public class Lang
{
    public string language;
    public List<string> langValue;
}


