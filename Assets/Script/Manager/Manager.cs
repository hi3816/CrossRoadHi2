
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    public int levelCount = 50;
    public Camera camera = null;
    public LevelGenerator levelGenerator = null;

    private int currentCoins = 0;
    private int currentDistance = 0;
    public bool canPlay = false;

    public event Action<int> coins;
    public event Action<int> distance;

    public List<ItemObject> pools;
    public Dictionary<int, Queue<GameObject>> poolDict;

    private static Manager s_Instance;
    public static Manager Instance
    {
        get 
        {
            if (s_Instance = null)
            {
                s_Instance = FindObjectOfType(typeof(Manager)) as Manager;
            }

            return s_Instance;
        }
    }

    private void Awake()
    {
        poolDict = new Dictionary<int, Queue<GameObject>>();
        foreach (var pool in pools)
        { 
            Queue<GameObject> poolObject = new Queue<GameObject>();
            for (int i = 0; i < 3; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                poolObject.Enqueue(obj);
            }
            poolDict.Add(pool.id, poolObject);
        }
    }

    public void UpdateConinCount()
    {
        currentCoins++;
        coins?.Invoke(currentCoins);
    }
    public GameObject SpawnFromPool(int id)
    {
        if (!poolDict.ContainsKey(id)) return null;

        //Awake에서 당겨왔던 poolDict를 사용하는 것
        if (poolDict[id].Count > 0)
        {
            GameObject obj = poolDict[id].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        //당겨왔던것들을 다 써서 새로 가져오는 것.
        else
        {
            var newObj = Instantiate(pools.Find(x => x.id == id).prefab);
            newObj.SetActive(false);
            poolDict[id].Enqueue(newObj);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj, int id)
    {
        if (!poolDict.ContainsKey(id)) return;

        obj.SetActive(false);
        poolDict[id].Enqueue(obj);
    }

    private void Start()
    {
        for (int i = 0; i < levelCount; i++)
        {
            levelGenerator.RandomGenerator();
        }
    }

    public bool CanPlay()
    {
        return canPlay;
    }

    public void StartPlay()
    {
        Debug.Log("시작");
        canPlay = true;
    }

    
}
