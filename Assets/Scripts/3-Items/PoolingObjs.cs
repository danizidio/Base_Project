using System.Collections.Generic;
using UnityEngine;
using System;
public class PoolingObjs : MonoBehaviour
{
    public Action<GameObject> OnReturnPoolObj;

    [SerializeField] List<PoolingObjsParam> _poolList;

    int _weaponEquiped = 1;
    public int weaponEquiped { set { _weaponEquiped = value; } }

    private void Awake()
    {
        StartPooling();
    }
    private void Start()
    {
        OnReturnPoolObj = DeactivateObj;
    }

    void StartPooling()
    {
        foreach (var item in _poolList)
        {
            for (int i = 0; i < item.maxQuantity; i++)
            {
                GameObject temp = Instantiate(item.obj, transform);
                temp.GetComponent<Projectile>().SetParentObj(this.gameObject);
                temp.SetActive(false);
                item.poolingObjects.Add(temp);
            }
        }
    }

    public GameObject GetPoolObj()
    {
        PoolingObjsParam[] a = _poolList.ToArray();

        for (int i = 0; i < a[_weaponEquiped].maxQuantity; i++)
        {
            if (!a[_weaponEquiped].poolingObjects[i].activeInHierarchy)
            {
                return a[_weaponEquiped].poolingObjects[i];
            }
        }

        return null;
    }

    public void DeactivateObj(GameObject obj)
    {
        obj.transform.parent = this.transform;
        obj.transform.position = Vector2.zero;
        obj.SetActive(false);
    }
}

[Serializable]
public class PoolingObjsParam
{
    [SerializeField] int _maxquantity;
    public int maxQuantity { get { return _maxquantity; } }

    [SerializeField] GameObject _obj;
    public GameObject obj { get { return _obj; } }

    [SerializeField] List<GameObject> _poolingObjects;
    public List<GameObject> poolingObjects { get { return _poolingObjects; } set { _poolingObjects = value; } }
}