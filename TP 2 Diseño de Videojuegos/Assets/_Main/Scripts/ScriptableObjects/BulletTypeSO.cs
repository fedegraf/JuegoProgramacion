using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(menuName = "New Bullet", fileName = "Bullet Type", order = 1)]
    public class BulletTypeSO : ScriptableObject
    {
        [SerializeField] private string bulletName;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float lifeTime;

        public string BulletName => bulletName;
        public float Speed => speed;
        public float Damage => damage;
        public float LifeTime => lifeTime;
    }
}
