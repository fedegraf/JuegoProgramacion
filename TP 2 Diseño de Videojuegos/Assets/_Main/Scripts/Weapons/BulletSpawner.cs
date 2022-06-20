using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class BulletSpawner : MonoBehaviour, IFactory<BulletBase, AmmoTypeSO>
    {
        [Header("Spawner Values")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject grenadePrefab;
        [SerializeField] private Transform bulletSpawn;
        [SerializeField] private Transform bulletBox;

        public BulletBase Product => bulletPrefab.GetComponent<BulletBase>();

        public BulletBase CreateBullet(AmmoTypeSO stats)
        {
            if (stats == null || bulletPrefab == null || grenadePrefab == null) return null;

            GameObject newBullet = stats.IsThrowable ? grenadePrefab : bulletPrefab;

            if (bulletSpawn == null) bulletSpawn = transform;
            var parentRotation = bulletSpawn.parent.rotation;

            var b = Instantiate(newBullet, bulletSpawn.position, parentRotation, bulletBox).GetComponent<BulletBase>();
            b.SetData(stats);
            return b;
        }
    }
}
