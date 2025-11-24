using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu()]
public class SO_Tutorials : ScriptableObject
{
    [SerializeField] List<TutorialsParams> _tutList;

    public void ChangeInfo(int v)
    {
        foreach (var item in _tutList)
        {
            if (item.id == v)
            {
                item.alreadyShow = true;
            }
        }
    }

    public bool CanShow(int v)
    {

        foreach (var item in _tutList)
        {
            if (item.id == v)
            {
                return !item.alreadyShow;
            }
        }

        return true;
    }
}

[Serializable]
public class TutorialsParams
{
    [SerializeField] int _id;
    public int id { get { return _id; } }

    [SerializeField] bool _alreadyShow;
    public bool alreadyShow { get { return _alreadyShow; } set { _alreadyShow = value; } }

    [SerializeField] GameObject _obj;
}
