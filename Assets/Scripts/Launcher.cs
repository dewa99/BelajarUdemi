using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Launcher : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;

    private IObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGet, OnRelease);
    }

    private void OnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = transform.position;
    }

    private void OnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.SetPool(bulletPool);
        return bullet;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bulletPool.Get();
        }
    }
}