using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "New Ammo", fileName = "Ammo Type", order = 1)]
    public class AmmoTypeSO : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float lifeTime;
        [SerializeField] private int maxAmmo;
        [SerializeField] private bool isThrowable;
        [SerializeField] private Mesh mesh;

        public string BulletName => name;
        public float Speed => speed;
        public float Damage => damage;
        public float LifeTime => lifeTime;
        public int MaxAmmo => maxAmmo;
        public bool IsThrowable => isThrowable;
        public Mesh Mesh => mesh;
    }
}
