using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chopable"))
        {
            other.gameObject.GetComponent<ObjectTree>().DestroyTree();
        }
    }
}