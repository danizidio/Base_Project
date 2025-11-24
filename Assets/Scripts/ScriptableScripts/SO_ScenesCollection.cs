using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu()]

public class SO_ScenesCollection : ScriptableObject
{
    [SerializeField] List<SceneParams> _territorylocations;
}

[Serializable]
public class SceneParams
{
    [SerializeField] string _nameLocation;

    [SerializeField] List<SceneAsset> _scenesCollection;
    public List<SceneAsset> scenesCollection { get { return _scenesCollection; } }
}
