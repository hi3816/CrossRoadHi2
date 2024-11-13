using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform startPos = null;

    public float dealayMin = 1.5f;
    public float dealayMax = 5;
    public float speedMin = 1;
    public float speedMax = 4;

    public bool useSpawnPlacement = false;
    public int spawnCount = 4;

    private float lastTime = 0;
    private float delayTime = 0;
    private float speed = 0;

    [HideInInspector] public ItemObject Item = null;
    [HideInInspector] public bool goLeft = false;
    [HideInInspector] public float spawnLeftPos = 0;
    [HideInInspector] public float spawnRightPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (useSpawnPlacement)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnItem();
            }
        }
        else 
        {
            speed = Random.Range(speedMin, speedMax);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useSpawnPlacement) return; 
        //한번만 스폰되는거라면
        if (Time.time > lastTime + delayTime)
        {
            lastTime = Time.time;
            delayTime = Random.Range(dealayMin, dealayMax);
        }
    }

    public void SpawnItem()
    {
        GameObject obj = Manager.Instance.SpawnFromPool(Item.id); //pools에서 가져온 obj
        obj.transform.position = GetSpawnPosition();

        float direction = 0;
        if (goLeft) direction = 180;

        if (!useSpawnPlacement) //스폰이 한번만 되는거라면
        {
            obj.GetComponent<Mover>().speed = speed;
            obj.transform.rotation = obj.transform.rotation * Quaternion.Euler(0, direction, 0);
        }
    }

    Vector3 GetSpawnPosition()
    {
        if (useSpawnPlacement) //한번에 한개 이상의 스폰을 사용한다면
        {
            return new Vector3(Random.Range(spawnLeftPos, spawnRightPos), startPos.position.y, startPos.position.z);
        }
        else 
        {
            return startPos.position;
        }
    }
}
