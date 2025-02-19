using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectTree : MonoBehaviour
{
    private IObjectPool<ObjectTree> treePool;

    public void SetPool(IObjectPool<ObjectTree> pool)
    {
        treePool = pool;
    }

    // private void OnDisable()
    public void DestroyTree()
    {
        PlayerDataManager.OnPlayerCollectTreeEvent();
        if (TreeSpawnerManager.GetInstance() == null) return;
        treePool.Release(this);
    }
}