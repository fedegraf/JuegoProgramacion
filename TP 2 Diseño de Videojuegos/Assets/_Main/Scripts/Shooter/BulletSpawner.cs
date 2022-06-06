using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour, IFactory<Bullet, BulletTypeSO>
{
    [Header("Spawner Values")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Transform bulletBox;

    public Bullet Product => bulletPrefab.GetComponent<Bullet>();

    public Bullet CreateBullet(BulletTypeSO stats)
    {
        if (stats == null || bulletPrefab == null) return null;

        if (bulletSpawn == null) bulletSpawn = transform;
        var parentRotation = bulletSpawn.parent.rotation;

        var b = Instantiate(bulletPrefab, bulletSpawn.position, parentRotation, bulletBox).GetComponent<Bullet>();
        b.SetData(stats);
        return b;
    }
}
