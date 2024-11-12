
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    public int levelCount = 50;
    public Camera camera = null;
    public LevelGenerator levelGenerator = null;

    public int coinCount;
    public Text coinCountTxt;

    public bool canPlay = false;

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

    public void UpdateConinCount()
    {
        coinCount++;
        coinCountTxt.text = coinCount.ToString();
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
        canPlay = true;
    }
}
