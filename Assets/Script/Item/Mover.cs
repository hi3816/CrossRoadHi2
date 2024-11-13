using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveDirection = 0;
    public bool parentOnTrigger = true;
    public bool hitBoxOnTrigger = false;
    public GameObject moveObject = null;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (parentOnTrigger)
            {
                other.transform.parent = this.transform;
            }

            if (hitBoxOnTrigger)
            {
                other.GetComponent<IHit>().GetHit();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (parentOnTrigger)
            {
                other.transform.parent = null;
            }
        }
    }
}