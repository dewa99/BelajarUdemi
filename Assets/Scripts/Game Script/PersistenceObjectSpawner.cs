using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceObjectSpawner : MonoBehaviour
{
    [Tooltip("The object that will spawned once and persist between scenes.")] [SerializeField]
    private GameObject persistenceObjectPrefab = null;

    private static bool hasSpawned = false;

    private void Awake()
    {
        if (hasSpawned) return;
        SpawnPersistenceObject();
        hasSpawned = true;
    }

    private void SpawnPersistenceObject()
    {
        GameObject persistenceObject = Instantiate(persistenceObjectPrefab);
        DontDestroyOnLoad(persistenceObject);
    }
}