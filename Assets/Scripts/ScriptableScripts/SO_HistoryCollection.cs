using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu()]
public class SO_HistoryCollection : ScriptableObject
{
    [SerializeField] List<HistoryObjParams> _historyList;

    public void ChangeInfo(int v)
    {
        foreach (var item in _historyList)
        {
            if(item.id == v)
            {
                item.alreadySaw = true;
            }
        }
    }

    public bool CanShow(int v)
    {

        foreach (var item in _historyList)
        {
            if (item.id == v)
            {
                return !item.alreadySaw;
            }
        }

        return true;
    }
}

[Serializable]
public class HistoryObjParams
{
    [SerializeField] int _id;
    public int id { get { return _id; } }

    [SerializeField] bool _alreadySaw;
    public bool alreadySaw { get { return _alreadySaw; }set { _alreadySaw = value; } }
    
    [SerializeField] GameObject _obj;
    public GameObject obj { get { return _obj; } }
}
