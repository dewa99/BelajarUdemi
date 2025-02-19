using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achivement : MonoBehaviour
{
    public static event Action TestFunction;
    public delegate void test();
    private void OnDisable()
    {
        TestFunction?.Invoke();
    }
}