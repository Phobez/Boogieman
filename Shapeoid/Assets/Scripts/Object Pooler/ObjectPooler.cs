using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObject
{
    public GameObject prefab;
    public int amount;
    public bool expands = true;

    public PoolObject(GameObject _prefab, int _amount, bool _expands = true)
    {
        prefab = _prefab;
        amount = Mathf.Max(_amount, 2);
        expands = _expands;
    }
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;

    public List<PoolObject> prefabs;

    private List<List<GameObject>> poolList;
    private List<int> positions;

    // cached variables and references
    private List<GameObject> pool;
    private PoolObject tempPoolObject;

    private void Awake()
    {
        instance = this;

        poolList = new List<List<GameObject>>();
        positions = new List<int>();

        for (int i = 0; i < prefabs.Count; i++)
        {
            PoolObject(i);
        }
    }

    public GameObject GetObject(int _index)
    {
        int _currSize = poolList[_index].Count;

        for (int i = positions[_index] + 1; i < positions[_index] + _currSize; i++)
        {
            if (!poolList[_index][i % _currSize].activeInHierarchy)
            {
                positions[_index] = i % _currSize;
                return poolList[_index][i % _currSize];
            }
        }

        if (prefabs[_index].expands)
        {
            GameObject _tempObj = (GameObject)Instantiate(prefabs[_index].prefab);
            _tempObj.SetActive(false);
            _tempObj.transform.parent = this.transform;
            poolList[_index].Add(_tempObj);
            return _tempObj;
        }

        return null;
    }

    private void PoolObject(int _index)
    {
        tempPoolObject = prefabs[_index];

        pool = new List<GameObject>();

        GameObject _tempObj;

        for (int i = 0; i < tempPoolObject.amount; i++)
        {
            _tempObj = (GameObject)Instantiate(tempPoolObject.prefab);
            _tempObj.SetActive(false);
            _tempObj.transform.parent = this.transform;
            pool.Add(_tempObj);
        }

        poolList.Add(pool);
        positions.Add(0);
    }
}
