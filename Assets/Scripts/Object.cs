using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Bloom = UnityEngine.Rendering.Universal.Bloom;

public class Object : MonoBehaviour
{
    public Volume volume;
    public bool isPlayerAlive = false;
    public GameObject volumeDeath;

    private void Start()
    {
        volume = GetComponent<Volume>();
        // if(volume.profile.TryGet<Bloom>(out Bloom bloom))
        // {
        //     bloom.intensity.value = 0;
        // }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
        }
    }

    private void OnEnable()
    {
        Achivement.TestFunction += UpdateSomething;
    }

    private void UpdateSomething()
    {
        Debug.Log("Updatings something");
    }

    public void PlayerDeath()
    {
        if (!isPlayerAlive)
        {
            isPlayerAlive = true;
            volumeDeath.SetActive(false);
            return;
        }

        isPlayerAlive = false;
        volumeDeath.SetActive(true);
    }
}