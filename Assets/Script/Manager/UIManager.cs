using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text coin = null;
    public Text distance = null;
    public GameObject guiGameOver = null;

    private void Start()
    {
        Manager.Instance.coins += UpdateConinCount;
        Manager.Instance.distance += UpdateDistance;
    }

    private void UpdateConinCount(int value)
    { 
        coin.text = value.ToString();
    }

    private void UpdateDistance(int value)
    {
        distance.text = value.ToString();
    }


}
