using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class TreeSpawnerManager : MonoBehaviour
{
    private static TreeSpawnerManager instance;
    [SerializeField] private List<ObjectTree> treePrefabs;
    [SerializeField] private int treeToSpawnIndex;
    private IObjectPool<ObjectTree> _treePool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _treePool = new ObjectPool<ObjectTree>(CreateTree, OnGetTree, OnReleaseTree);
    }

    private void Start()
    {
        for (int i = 0; i < treeToSpawnIndex; i++)
        {
            _treePool.Get();
        }
    }

    private void SpawnTreeIn5Seconds() //u can make this more dynamic like example below
    {
        StartCoroutine(SpawnTreeAfter5Seconds());
    }
    //=============================================
    //make like this so you can change the delay anytime via inspector or maybe code 
    private void SpawnDelayedTree(int delay)
    {
        StartCoroutine(SpawnDelayedTreeEnumerator(delay));
    }

    IEnumerator SpawnDelayedTreeEnumerator(int delay)
    {
        _treePool.Get();
        yield return new WaitForSeconds(delay);
    }
    //============================================
    private IEnumerator SpawnTreeAfter5Seconds()
    {
        yield return new WaitForSeconds(5);
        _treePool.Get();
    }

    public static TreeSpawnerManager GetInstance()
    {
        return instance;
    }

    private ObjectTree CreateTree()
    {
        var treePrefab = treePrefabs[UnityEngine.Random.Range(0, treePrefabs.Count)];
        ObjectTree tree = Instantiate(treePrefab);
        tree.transform.position = new Vector3(UnityEngine.Random.Range(-17, 17), 0, UnityEngine.Random.Range(-17, 17));
        tree.SetPool(_treePool);
        return tree;
    }

    private void OnGetTree(ObjectTree tree)
    {
        tree.transform.position = new Vector3(UnityEngine.Random.Range(-17, 17), 0, UnityEngine.Random.Range(-17, 17));
        tree.gameObject.SetActive(true);
    }

    private void OnReleaseTree(ObjectTree tree)
    {
        tree.gameObject.SetActive(false);
        SpawnTreeIn5Seconds();
    }
}