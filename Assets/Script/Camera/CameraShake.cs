using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            Shake();
        }
    }

    void Shake()
    {
        float shakeDuration = 1.25f; // ��鸲�� ����� �ð�

        transform.DOShakePosition(shakeDuration);
    }
}
